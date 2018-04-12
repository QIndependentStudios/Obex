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

        /// <summary>
        /// Converts binary data to an Obex header object.
        /// </summary>
        /// <param name="bytes">The binary data to deserialize.</param>
        /// <returns>The deserialized Obex header.</returns>
        public override ObexHeader FromBytes(byte[] bytes)
        {
            return GuidObexHeader.Create((ObexHeaderId)bytes[0],
                GetHeaderSize(bytes),
                ObexBitConverter.ToGuid(ExtractValueBytes(bytes)));
        }

        /// <summary>
        /// Converts the value of the Obex header to binary data.
        /// </summary>
        /// <param name="header">The header whose value will be converted.</param>
        /// <returns>The binary data of the header's value.</returns>
        protected override byte[] ValueToBytes(ObexHeader header)
        {
            if (header is GuidObexHeader guidheader)
                return ObexBitConverter.GetBytes(guidheader.Value);

            return new byte[0];
        }
    }
}
