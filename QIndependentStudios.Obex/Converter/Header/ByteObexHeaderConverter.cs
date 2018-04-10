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

        /// <inheritdoc/>
        public override int? HeaderLengthOverride => 2;

        /// <inheritdoc/>
        public override ObexHeader FromBytes(byte[] bytes)
        {
            return ByteObexHeader.Create((ObexHeaderId)bytes[0], ExtractValueBytes(bytes)[0]);
        }

        /// <inheritdoc/>
        protected override byte[] ValueToBytes(ObexHeader header)
        {
            if (header is ByteObexHeader byteHeader)
                return new byte[] { byteHeader.Value };

            return new byte[0];
        }
    }
}
