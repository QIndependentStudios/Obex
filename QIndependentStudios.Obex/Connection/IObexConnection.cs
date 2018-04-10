using System;
using System.Threading.Tasks;

namespace QIndependentStudios.Obex.Connection
{
    /// <summary>
    /// Enables an Obex client to initate a connection and communicate over it when implemented on a platform.
    /// </summary>
    public interface IObexConnection : IDisposable
    {
        /// <summary>
        /// Creates and initializes a connection if not already done so. This is called before any read and write.
        /// </summary>
        /// <returns></returns>
        Task EnsureInitAsync();

        /// <summary>
        /// Provides a way for the Obex client to receive data over the connection.
        /// </summary>
        /// <param name="length">The number of bytes to read.</param>
        /// <returns>The bytes read from the connection.</returns>
        Task<byte[]> ReadAsync(uint length);

        /// <summary>
        /// Provides a way for the Obex client to send data over the connection.
        /// </summary>
        /// <param name="data">The data the client wants to send.</param>
        /// <returns></returns>
        Task WriteAsync(byte[] data);
    }
}
