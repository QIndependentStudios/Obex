using QIndependentStudios.Obex.Header;

namespace QIndependentStudios.Obex.Converter.Header
{
    /// <summary>
    /// Converts Guid value Obex headers to and from binary data.
    /// </summary>
    public class GuidObexHeaderConverter : RawObexHeaderConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GuidObexHeaderConverter"/> class.
        /// </summary>
        protected GuidObexHeaderConverter()
        { }

        /// <summary>
        /// Gets the instance of the <see cref="GuidObexHeaderConverter"/> class.
        /// </summary>
        public new static GuidObexHeaderConverter Instance { get; } = new GuidObexHeaderConverter();

        /// <inheritdoc/>
        public override ObexHeader FromBytes(byte[] bytes)
        {
            return GuidObexHeader.Create((ObexHeaderId)bytes[0],
                GetHeaderSize(bytes),
                ObexBitConverter.ToGuid(ExtractValueBytes(bytes)));
        }

        /// <inheritdoc/>
        protected override byte[] ValueToBytes(ObexHeader header)
        {
            if (header is GuidObexHeader guidheader)
                return ObexBitConverter.GetBytes(guidheader.Value);

            return new byte[0];
        }
    }
}
