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
    public abstract class ObexServerConnection : IObexServerConnection
    {
        protected StreamSocket _socket;

        public StreamSocket Socket => _socket;

        public static ObexServerConnection Create(RfcommServiceId serviceId)
        {
            return new RfcommServiceObexServerConnection(serviceId, null);
        }

        public static ObexServerConnection Create(RfcommServiceId serviceId,
            IDictionary<byte, byte[]> attributes)
        {
            return new RfcommServiceObexServerConnection(serviceId, attributes);
        }

        public abstract Task WaitForClientAsync();

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
