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

        /// <summary>
        /// Converts binary data to an Obex header object.
        /// </summary>
        /// <param name="bytes">The binary data to deserialize.</param>
        /// <returns>The deserialized Obex header.</returns>
        public override ObexHeader FromBytes(byte[] bytes)
        {
            return TextObexHeader.Create((ObexHeaderId)bytes[0],
                GetHeaderSize(bytes),
                ObexBitConverter.ToString(ExtractValueBytes(bytes)).TrimEnd(char.MinValue));
        }

        /// <summary>
        /// Converts the value of the Obex header to binary data.
        /// </summary>
        /// <param name="header">The header whose value will be converted.</param>
        /// <returns>The binary data of the header's value.</returns>
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
