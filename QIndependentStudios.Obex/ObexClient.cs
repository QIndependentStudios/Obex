using QIndependentStudios.Obex.Connection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QIndependentStudios.Obex
{
    /// <summary>
    /// Provides a class for sending Obex request and receiving Obex responses.
    /// </summary>
    public class ObexClient : IDisposable
    {
        private readonly IObexConnection _connection;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        /// <summary>
        /// Initializes a new instance of the <see cref="ObexClient"/> class.
        /// </summary>
        /// <param name="connection">The connection the client will communicate over.</param>
        public ObexClient(IObexConnection connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Gets or sets the timeout duration in milliseconds when making a request.
        /// </summary>
        public int Timeout { get; set; } = 10000;

        /// <summary>
        /// Makes a request and waits for a response.
        /// </summary>
        /// <param name="request">The request to send.</param>
        /// <returns>The response received from the server.</returns>
        public async Task<ObexResponseBase> RequestAsync(ObexRequestBase request)
        {
            await _semaphore.WaitAsync();
            try
            {
                await _connection.EnsureInitAsync();
                await _connection.WriteAsync(ObexSerializer.SerializeRequest(request)).WithTimeout(Timeout);

                var responseData = await ReadPacketDataAsync().WithTimeout(Timeout);
                return ObexSerializer.DeserializeResponse(responseData, request.OpCode == ObexOpCode.Connect);
            }
            finally { _semaphore.Release(); }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _connection.Dispose();
            _semaphore.Dispose();
        }

        private async Task<byte[]> ReadPacketDataAsync()
        {
            var responseByte = await _connection.ReadAsync(1);
            var packetSizeBytes = await _connection.ReadAsync(2);

            var packetData = responseByte.Concat(packetSizeBytes).ToList();
            var bytesRemaining = (uint)(ObexBitConverter.ToUInt16(packetSizeBytes) - packetData.Count);

            if (bytesRemaining > 0)
                packetData.AddRange(await _connection.ReadAsync(bytesRemaining));

            return packetData.ToArray();
        }
    }
}
