using QIndependentStudios.Obex.Connection;
using System;
using System.Collections.Generic;
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
                var opCodeByte = (await _connection.ReadAsync(1))[0];
                var request = await ReadRequest(opCodeByte).WithTimeout(Timeout);

                var response = requestHandler(request);
                await _connection.WriteAsync(ObexSerializer.SerializeResponse(response)).WithTimeout(Timeout);
            }
        }

        private async Task<ObexRequestBase> ReadRequest(byte opCodeByte)
        {
            var bytes = new List<byte> { opCodeByte };

            var packetLengthBytes = await _connection.ReadAsync(2);
            bytes.AddRange(packetLengthBytes);

            var bytesRemaining = ObexBitConverter.ToUInt16(packetLengthBytes) - bytes.Count;

            if (bytesRemaining > 0)
                bytes.AddRange(await _connection.ReadAsync((uint)bytesRemaining));

            return ObexSerializer.DeserializeRequest(bytes.ToArray());
        }
    }
}
