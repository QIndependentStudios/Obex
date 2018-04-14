using QIndependentStudios.Obex.Header;

namespace QIndependentStudios.Obex.Converter.Header
{
    /// <summary>
    /// Converts single byte value Obex headers with fixed length to and from binary data.
    /// </summary>
    public class ByteObexHeaderConverter : ObexHeaderConverterBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ByteObexHeaderConverter"/> class.
        /// </summary>
        protected ByteObexHeaderConverter()
        { }

        /// <summary>
        /// Gets the instance of the <see cref="ByteObexHeaderConverter"/> class.
        /// </summary>
        public static ByteObexHeaderConverter Instance { get; } = new ByteObexHeaderConverter();

        /// <summary>
        /// Converts binary data to an Obex header object.
        /// </summary>
        /// <param name="bytes">The binary data to deserialize.</param>
        /// <returns>The deserialized Obex header.</returns>
        public override ObexHeader FromBytes(byte[] bytes)
        {
            return ByteObexHeader.Create((ObexHeaderId)bytes[0], ExtractValueBytes(bytes)[0]);
        }

        /// <summary>
        /// Converts the value of the Obex header to binary data.
        /// </summary>
        /// <param name="header">The header whose value will be converted.</param>
        /// <returns>The binary data of the header's value.</returns>
        protected override byte[] ValueToBytes(ObexHeader header)
        {
            if (header is ByteObexHeader byteHeader)
                return new[] { byteHeader.Value };

            return new byte[0];
        }
    }
}
