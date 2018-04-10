using QIndependentStudios.Obex.Header;

namespace QIndependentStudios.Obex.Converter.Header
{
    /// <summary>
    /// Converts Obex headers to and from binary data.
    /// </summary>
    public interface IObexHeaderConverter
    {
        /// <summary>
        /// Converts an Obex header to binary data.
        /// </summary>
        /// <param name="header">The header to convert.</param>
        /// <returns>Binary data representing the Obex header.</returns>
        byte[] ToBytes(ObexHeader header);

        /// <summary>
        /// Converts binary data to an Obex header object.
        /// </summary>
        /// <param name="bytes">The binary data to deserialize.</param>
        /// <returns>The deserialized Obex header.</returns>
        ObexHeader FromBytes(byte[] bytes);
    }
}
