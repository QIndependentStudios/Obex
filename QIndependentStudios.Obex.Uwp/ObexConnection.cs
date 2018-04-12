using QIndependentStudios.Obex.Connection;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace QIndependentStudios.Obex.Uwp
{
    /// <summary>
    /// Provides implementation of an Obex connection.
    /// </summary>
    public abstract class ObexConnection : IObexConnection
    {
        /// <summary>
        /// The underlying <see cref="StreamSocket"/> connection.
        /// </summary>
        protected StreamSocket _socket;

        /// <summary>
        /// Gets the underlying <see cref="StreamSocket"/> connection.
        /// </summary>
        public StreamSocket Socket => _socket;

        /// <summary>
        /// Creates an new instance of an <see cref="ObexConnection"/> to an <see cref="RfcommDeviceService"/>.
        /// </summary>
        /// <param name="rfcommService">The rfcomm service to connect to.</param>
        /// <returns>Returns an Obex connection for the specified rfcomm service.</returns>
        public static ObexConnection Create(RfcommDeviceService rfcommService)
        {
            return new RfcommServiceObexConnection(rfcommService);
        }

        /// <summary>
        /// Creates and initializes a connection if not already done so. This is called before any read and write.
        /// </summary>
        /// <returns></returns>
        public abstract Task EnsureInitAsync();

        /// <summary>
        /// Frees up and releases resources.
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// Provides a way for the Obex client to receive data over the connection.
        /// </summary>
        /// <param name="length">The number of bytes to read.</param>
        /// <returns>The bytes read from the connection.</returns>
        public async Task<byte[]> ReadAsync(uint length)
        {
            var bytes = new byte[length];
            await _socket.InputStream.ReadAsync(bytes.AsBuffer(), length, InputStreamOptions.None);
            return bytes;
        }

        /// <summary>
        /// Provides a way for the Obex client to send data over the connection.
        /// </summary>
        /// <param name="data">The data the client wants to send.</param>
        /// <returns></returns>
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
