using QIndependentStudios.Obex.Header;

namespace QIndependentStudios.Obex.Converter.Header
{
    /// <summary>
    /// Converts byte sequence Obex headers to and from binary data.
    /// </summary>
    public class ByteSequenceObexHeaderConverter : ObexHeaderConverterBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ByteSequenceObexHeaderConverter"/> class.
        /// </summary>
        protected ByteSequenceObexHeaderConverter()
        { }

        /// <summary>
        /// Gets the instance of the <see cref="ByteSequenceObexHeaderConverter"/> class.
        /// </summary>
        public static ByteSequenceObexHeaderConverter Instance { get; } = new ByteSequenceObexHeaderConverter();

        /// <summary>
        /// Converts binary data to an Obex header object.
        /// </summary>
        /// <param name="bytes">The binary data to deserialize.</param>
        /// <returns>The deserialized Obex header.</returns>
        public override ObexHeader FromBytes(byte[] bytes)
        {
            return ByteSequenceObexHeader.Create((ObexHeaderId)bytes[0], ExtractValueBytes(bytes));
        }

        /// <summary>
        /// Converts the value of the Obex header to binary data.
        /// </summary>
        /// <param name="header">The header whose value will be converted.</param>
        /// <returns>The binary data of the header's value.</returns>
        protected override byte[] ValueToBytes(ObexHeader header)
        {
            if (header is ByteSequenceObexHeader byteSequenceheader)
                return byteSequenceheader.Value;

            return new byte[0];
        }
    }
}
