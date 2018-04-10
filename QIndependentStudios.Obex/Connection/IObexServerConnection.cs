using System;
using System.Threading.Tasks;

namespace QIndependentStudios.Obex.Connection
{
    /// <summary>
    /// Enables an Obex server to start listening for a connection from a client and communicate over it when implemented on a platform.
    /// </summary>
    public interface IObexServerConnection : IDisposable
    {
        /// <summary>
        /// Creates a connection and starts listening for a client to connect over that connection.
        /// </summary>
        /// <returns></returns>
        Task WaitForClientAsync();

        /// <summary>
        /// Provides a way for the Obex server to receive data over the connection.
        /// </summary>
        /// <param name="length"></param>
        /// <returns>The bytes read from the connection.</returns>
        Task<byte[]> ReadAsync(uint length);

        /// <summary>
        /// Provides a way for the Obex server to send data over the connection.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task WriteAsync(byte[] data);
    }
}
