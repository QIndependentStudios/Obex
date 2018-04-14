using QIndependentStudios.Obex.Connection;
using QIndependentStudios.Obex.Header;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QIndependentStudios.Obex
{
    /// <summary>
    /// Provides a class for listening to Obex requests and sending Obex responses.
    /// </summary>
    public class ObexServer
    {
        private readonly IObexServerConnection _connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObexServer"/> class.
        /// </summary>
        /// <param name="connection">The connection the server will communicate over.</param>
        public ObexServer(IObexServerConnection connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Gets or sets the timeout duration in milliseconds when waiting the rest of a request's data.
        /// </summary>
        public int Timeout { get; set; } = 10000;

        /// <summary>
        /// Starts the server.
        /// </summary>
        /// <param name="requestHandler"></param>
        public async void Start(Func<ObexRequestBase, ObexResponseBase> requestHandler)
        {
            await _connection.WaitForClientAsync();
            ListenForRequests(requestHandler);
        }

        private async void ListenForRequests(Func<ObexRequestBase, ObexResponseBase> requestHandler)
        {
            while (true)
            {
                var request = await AwaitTaskWithTimeout(ReadNextRequest());

                var response = requestHandler(request);
                await AwaitTaskWithTimeout(_connection.WriteAsync(ObexSerializer.SerializeResponse(response)));
            }
        }

        private async Task AwaitTaskWithTimeout(Task task)
        {
            if (await Task.WhenAny(task, Task.Delay(Timeout)) != task)
                throw new TimeoutException();
        }

        private async Task<T> AwaitTaskWithTimeout<T>(Task<T> task)
        {
            if (await Task.WhenAny(task, Task.Delay(Timeout)) == task)
                return task.Result;

            throw new TimeoutException();
        }

        private async Task<ObexRequestBase> ReadNextRequest()
        {
            var id = (ObexHeaderId)(await _connection.ReadAsync(1))[1];
            var bytes = await ReadRemainingRequestData(id);

            return ObexSerializer.DeserializeRequest(bytes.ToArray());
        }

        private async Task<IEnumerable<byte>> ReadRemainingRequestData(ObexHeaderId id)
        {
            var bytes = new List<byte> { (byte)id };
            var bytesRemaining = 0;

            if (ObexHeaderUtil.GetHeaderEncoding(id) == ObexHeaderEncoding.SingleByte)
                bytesRemaining = 1;
            else if (ObexHeaderUtil.GetHeaderEncoding(id) == ObexHeaderEncoding.FourBytes)
                bytesRemaining = 4;
            else
            {
                var packetLengthBytes = await _connection.ReadAsync(2);
                bytes.AddRange(packetLengthBytes);

                bytesRemaining = ObexBitConverter.ToUInt16(packetLengthBytes) - bytes.Count;
            }

            if (bytesRemaining > 0)
                bytes.AddRange(await _connection.ReadAsync((uint)bytesRemaining));
            return bytes;
        }
    }
}
