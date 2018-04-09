using System;
using System.Threading.Tasks;

namespace QIndependentStudios.Obex.Connection
{
    public interface IObexConnection : IDisposable
    {
        Task EnsureInitAsync();
        Task<byte[]> ReadAsync(uint length);
        Task WriteAsync(byte[] data);
    }
}
