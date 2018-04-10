namespace QIndependentStudios.Obex.Converter.Response
{
    /// <summary>
    /// Converts Obex response to and from binary data.
    /// </summary>
    public interface IObexResponseConverter
    {
        /// <summary>
        /// Converts an Obex response to binary data.
        /// </summary>
        /// <param name="response">The response to convert.</param>
        /// <returns>Binary data representing the Obex response.</returns>
        byte[] ToBytes(ObexResponseBase response);

        /// <summary>
        /// Converts binary data to an Obex response object.
        /// </summary>
        /// <param name="bytes">The binary data to convert.</param>
        /// <returns>The deserialized Obex response.</returns>
        ObexResponseBase FromBytes(byte[] bytes);
    }
}
