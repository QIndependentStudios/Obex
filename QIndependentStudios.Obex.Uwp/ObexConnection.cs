using QIndependentStudios.Obex.Connection;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace QIndependentStudios.Obex.Uwp
{
    public abstract class ObexConnection : IObexConnection
    {
        protected StreamSocket _socket;

        public StreamSocket Socket => _socket;

        public static ObexConnection Create(RfcommDeviceService rfcommService)
        {
            return new RfcommServiceObexConnection(rfcommService);
        }

        public abstract Task EnsureInitAsync();

        public abstract void Dispose();

        public async Task<byte[]> ReadAsync(uint length)
        {
            var bytes = new byte[length];
            await _socket.InputStream.ReadAsync(bytes.AsBuffer(), length, InputStreamOptions.None);
            return bytes;
        }

        public async Task WriteAsync(byte[] data)
        {
            await _socket.OutputStream.WriteAsync(data.AsBuffer());
        }
    }

    internal class RfcommServiceObexConnection : ObexConnection
    {
        private readonly RfcommDeviceService _rfcommService;
        private bool _isInit;

        public RfcommServiceObexConnection(RfcommDeviceService deviceService)
        {
            _socket = new StreamSocket();
            _rfcommService = deviceService;
        }

        public override async Task EnsureInitAsync()
        {
            if (_isInit)
                return;

            await _socket.ConnectAsync(_rfcommService.ConnectionHostName, _rfcommService.ConnectionServiceName);
            _isInit = true;
        }

        public override void Dispose()
        {
            _socket.Dispose();
            _isInit = false;
        }
    }
}
