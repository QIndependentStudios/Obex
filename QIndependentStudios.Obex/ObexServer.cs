using QIndependentStudios.Obex.Connection;
using System;
using System.Collections.Generic;

namespace QIndependentStudios.Obex
{
    public class ObexServer
    {
        private readonly IObexServerConnection _connection;

        public ObexServer(IObexServerConnection connection)
        {
            _connection = connection;
        }

        public async void Start(Func<ObexRequestBase, ObexResponseBase> requestHandler)
        {
            await _connection.WaitForClientAsync();
            ListenForRequests(requestHandler);
        }

        private async void ListenForRequests(Func<ObexRequestBase, ObexResponseBase> requestHandler)
        {
            while (true)
            {
                var bytes = new List<byte>();
                bytes.AddRange(await _connection.ReadAsync(1));

                var packetLengthBytes = await _connection.ReadAsync(2);
                bytes.AddRange(packetLengthBytes);

                int bytesRemaining = (ObexBitConverter.ToUInt16(packetLengthBytes) - bytes.Count);
                if (bytesRemaining > 0)
                    bytes.AddRange(await _connection.ReadAsync((uint)bytesRemaining));

                var request = ObexSerializer.DeserializeRequest(bytes.ToArray());

                var response = requestHandler(request);
                await _connection.WriteAsync(ObexSerializer.SerializeResponse(response));
            }
        }
    }
}
