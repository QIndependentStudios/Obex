using QIndependentStudios.Obex.Header;
using System.Linq;
using System.Text;

namespace QIndependentStudios.Obex.Converter.Header
{
    /// <summary>
    /// Converts unicode text value Obex headers to and from binary data.
    /// </summary>
    public class UnicodeTextObexHeaderConverter : RawObexHeaderConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnicodeTextObexHeaderConverter"/> class.
        /// </summary>
        protected UnicodeTextObexHeaderConverter()
        { }

        /// <summary>
        /// Gets the instance of the <see cref="UnicodeTextObexHeaderConverter"/> class.
        /// </summary>
        public new static UnicodeTextObexHeaderConverter Instance { get; } = new UnicodeTextObexHeaderConverter();

        /// <inheritdoc/>
        public override ObexHeader FromBytes(byte[] bytes)
        {
            return UnicodeTextObexHeader.Create((ObexHeaderId)bytes[0],
                GetHeaderSize(bytes),
                ObexBitConverter.ToUnicodeString(ExtractValueBytes(bytes)).TrimEnd(char.MinValue));
        }

        /// <inheritdoc/>
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
