using QIndependentStudios.Obex.Header;

namespace QIndependentStudios.Obex.Converter.Header
{
    /// <summary>
    /// Converts raw binary value Obex headers to and from binary data.
    /// </summary>
    public class RawObexHeaderConverter : ObexHeaderConverterBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RawObexHeaderConverter"/> class.
        /// </summary>
        protected RawObexHeaderConverter()
        { }

        /// <summary>
        /// Gets the instance of the <see cref="RawObexHeaderConverter"/> class.
        /// </summary>
        public static RawObexHeaderConverter Instance { get; } = new RawObexHeaderConverter();

        /// <inheritdoc/>
        public override ObexHeader FromBytes(byte[] bytes)
        {
            return RawObexHeader.Create((ObexHeaderId)bytes[0], ExtractValueBytes(bytes));
        }

        /// <inheritdoc/>
        protected override byte[] ValueToBytes(ObexHeader header)
        {
            if (header is RawObexHeader rawheader)
                return rawheader.Value;

            return new byte[0];
        }
    }
}
