using QIndependentStudios.Obex.Header;
using System.Linq;
using System.Text;

namespace QIndependentStudios.Obex.Converter.Header
{
    /// <summary>
    /// Converts unicode text value Obex headers to and from binary data.
    /// </summary>
    public class UnicodeTextObexHeaderConverter : ObexHeaderConverterBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnicodeTextObexHeaderConverter"/> class.
        /// </summary>
        protected UnicodeTextObexHeaderConverter()
        { }

        /// <summary>
        /// Gets the instance of the <see cref="UnicodeTextObexHeaderConverter"/> class.
        /// </summary>
        public static UnicodeTextObexHeaderConverter Instance { get; } = new UnicodeTextObexHeaderConverter();

        /// <summary>
        /// Converts binary data to an Obex header object.
        /// </summary>
        /// <param name="bytes">The binary data to deserialize.</param>
        /// <returns>The deserialized Obex header.</returns>
        public override ObexHeader FromBytes(byte[] bytes)
        {
            return UnicodeTextObexHeader.Create((ObexHeaderId)bytes[0],
                GetHeaderSize(bytes),
                ObexBitConverter.ToUnicodeString(ExtractValueBytes(bytes)).TrimEnd(char.MinValue));
        }

        /// <summary>
        /// Converts the value of the Obex header to binary data.
        /// </summary>
        /// <param name="header">The header whose value will be converted.</param>
        /// <returns>The binary data of the header's value.</returns>
        protected override byte[] ValueToBytes(ObexHeader header)
        {
            if (!(header is UnicodeTextObexHeader textheader))
                return new byte[0];

            var value = textheader.Value;

            if (!string.IsNullOrEmpty(value) && value.Last() != char.MinValue)
                value += char.MinValue.ToString();

            return ObexBitConverter.GetBytes(value, Encoding.BigEndianUnicode);
        }
    }
}
