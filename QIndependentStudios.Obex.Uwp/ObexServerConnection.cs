using QIndependentStudios.Obex.Connection;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace QIndependentStudios.Obex.Uwp
{
    /// <summary>
    /// Provides implementation of an Obex server connection.
    /// </summary>
    public abstract class ObexServerConnection : IObexServerConnection
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
        /// Creates an new instance of an <see cref="ObexServerConnection"/> for a discoverable <see cref="RfcommServiceId"/>.
        /// </summary>
        /// <param name="serviceId">The service that will be advertised for a connection over rfcomm.</param>
        /// <returns>Returns an Obex server connection for the specified rfcomm service id.</returns>
        public static ObexServerConnection Create(RfcommServiceId serviceId)
        {
            return new RfcommServiceObexServerConnection(serviceId, null);
        }

        /// <summary>
        /// Creates an new instance of an <see cref="ObexServerConnection"/> for a discoverable <see cref="RfcommServiceId"/>.
        /// </summary>
        /// <param name="serviceId">The service that will be advertised for a connection over rfcomm.</param>
        /// <param name="attributes">The rfcomm attributes that can be queried by potential clients.</param>
        /// <returns>Returns an Obex server connection for the specified rfcomm service id.</returns>
        public static ObexServerConnection Create(RfcommServiceId serviceId,
            IDictionary<byte, byte[]> attributes)
        {
            return new RfcommServiceObexServerConnection(serviceId, attributes);
        }

        /// <summary>
        /// Creates a connection and starts listening for a client to connect over that connection.
        /// </summary>
        /// <returns></returns>
        public abstract Task WaitForClientAsync();

        /// <summary>
        /// Frees up and releases resources.
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// Provides a way for the Obex server to receive data over the connection.
        /// </summary>
        /// <param name="length"></param>
        /// <returns>The bytes read from the connection.</returns>
        public async Task<byte[]> ReadAsync(uint length)
        {
            var bytes = new byte[length];
            await _socket.InputStream.ReadAsync(bytes.AsBuffer(), length, InputStreamOptions.None);
            return bytes;
        }

        /// <summary>
        /// Provides a way for the Obex server to send data over the connection.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task WriteAsync(byte[] data)
        {
            await _socket.OutputStream.WriteAsync(data.AsBuffer());
        }
    }

    internal class RfcommServiceObexServerConnection : ObexServerConnection
    {
        private readonly RfcommServiceId _serviceId;
        private readonly IDictionary<byte, byte[]> _attributes;

        private RfcommServiceProvider _provider;
        private TaskCompletionSource<bool> _tcs = new TaskCompletionSource<bool>();

        public RfcommServiceObexServerConnection(RfcommServiceId serviceId,
            IDictionary<byte, byte[]> attributes)
        {
            _serviceId = serviceId;
            _attributes = attributes;
        }

        public async override Task WaitForClientAsync()
        {
            _provider = await RfcommServiceProvider.CreateAsync(_serviceId);
            var listener = new StreamSocketListener();
            listener.ConnectionReceived += OnConnectionReceived;
            await listener.BindServiceNameAsync(_provider.ServiceId.AsString(),
                SocketProtectionLevel.BluetoothEncryptionAllowNullAuthentication);

            InitializeServiceSdpAttributes(_provider);
            _provider.StartAdvertising(listener);
            await _tcs.Task;
        }

        public override void Dispose()
        {
            _socket.Dispose();
        }

        private void OnConnectionReceived(StreamSocketListener listener,
            StreamSocketListenerConnectionReceivedEventArgs args)
        {
            _provider.StopAdvertising();
            listener.Dispose();
            _socket = args.Socket;
            _tcs.SetResult(true);
        }

        private void InitializeServiceSdpAttributes(RfcommServiceProvider provider)
        {
            if (_attributes == null)
                return;

            foreach (var attr in _attributes)
            {
                provider.SdpRawAttributes.Add(attr.Key, attr.Value.AsBuffer());
            }
        }
    }
}
