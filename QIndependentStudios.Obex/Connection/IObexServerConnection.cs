using System;
using System.Threading.Tasks;

namespace QIndependentStudios.Obex.Connection
{
    public interface IObexServerConnection : IDisposable
    {
        Task WaitForClientAsync();
        Task<byte[]> ReadAsync(uint length);
        Task WriteAsync(byte[] data);
    }
}
