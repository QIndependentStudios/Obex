namespace QIndependentStudios.Obex.Converter.Request
{
    /// <summary>
    /// Converts Obex requests to and from binary data.
    /// </summary>
    public interface IObexRequestConverter
    {
        /// <summary>
        /// Converts an Obex request to binary data.
        /// </summary>
        /// <param name="request">The request to convert.</param>
        /// <returns>Binary data representing the Obex request.</returns>
        byte[] ToBytes(ObexRequestBase request);

        /// <summary>
        /// Converts binary data to an Obex request object.
        /// </summary>
        /// <param name="bytes">The binary data to convert.</param>
        /// <returns>The deserialized Obex request.</returns>
        ObexRequestBase FromBytes(byte[] bytes);
    }
}
