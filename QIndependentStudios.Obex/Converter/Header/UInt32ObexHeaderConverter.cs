using QIndependentStudios.Obex.Header;

namespace QIndependentStudios.Obex.Converter.Header
{
    /// <summary>
    /// Converts unsigned integer value Obex headers with fixed length to and from binary data.
    /// </summary>
    public class UInt32ObexHeaderConverter : ObexHeaderConverterBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UInt32ObexHeaderConverter"/> class.
        /// </summary>
        protected UInt32ObexHeaderConverter()
        { }

        /// <summary>
        /// Gets the instance of the <see cref="UInt32ObexHeaderConverter"/> class.
        /// </summary>
        public static UInt32ObexHeaderConverter Instance { get; } = new UInt32ObexHeaderConverter();

        /// <summary>
        /// Converts binary data to an Obex header object.
        /// </summary>
        /// <param name="bytes">The binary data to deserialize.</param>
        /// <returns>The deserialized Obex header.</returns>
        public override ObexHeader FromBytes(byte[] bytes)
        {
            return UInt32ObexHeader.Create((ObexHeaderId)bytes[0], ObexBitConverter.ToUInt32(ExtractValueBytes(bytes)));
        }

        /// <summary>
        /// Converts the value of the Obex header to binary data.
        /// </summary>
        /// <param name="header">The header whose value will be converted.</param>
        /// <returns>The binary data of the header's value.</returns>
        protected override byte[] ValueToBytes(ObexHeader header)
        {
            if (header is UInt32ObexHeader uint32Header)
                return ObexBitConverter.GetBytes(uint32Header.Value);

            return new byte[0];
        }
    }
}
