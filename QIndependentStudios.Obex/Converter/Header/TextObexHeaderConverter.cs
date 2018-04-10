using QIndependentStudios.Obex.Header;
using System.Linq;

namespace QIndependentStudios.Obex.Converter.Header
{
    /// <summary>
    /// Converts text value Obex headers to and from binary data.
    /// </summary>
    public class TextObexHeaderConverter : RawObexHeaderConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextObexHeaderConverter"/> class.
        /// </summary>
        protected TextObexHeaderConverter()
        { }

        /// <summary>
        /// Gets the instance of the <see cref="TextObexHeaderConverter"/> class.
        /// </summary>
        public new static TextObexHeaderConverter Instance { get; } = new TextObexHeaderConverter();

        /// <inheritdoc/>
        public override ObexHeader FromBytes(byte[] bytes)
        {
            return TextObexHeader.Create((ObexHeaderId)bytes[0],
                GetHeaderSize(bytes),
                ObexBitConverter.ToString(ExtractValueBytes(bytes)).TrimEnd(char.MinValue));
        }

        /// <inheritdoc/>
        protected override byte[] ValueToBytes(ObexHeader header)
        {
            if (!(header is TextObexHeader textheader))
                return new byte[0];

            var value = textheader.Value;

            if (!string.IsNullOrEmpty(value) && value.Last() != char.MinValue)
                value += char.MinValue.ToString();

            return ObexBitConverter.GetBytes(value);
        }
    }
}
