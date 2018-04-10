using QIndependentStudios.Obex.Header;
using System.Linq;
using System.Text;

namespace QIndependentStudios.Obex.Converter.Header
{
    public class UnicodeTextObexHeaderConverter : RawObexHeaderConverter
    {
        protected UnicodeTextObexHeaderConverter()
        { }

        public new static UnicodeTextObexHeaderConverter Instance { get; } = new UnicodeTextObexHeaderConverter();

        public override ObexHeader FromBytes(byte[] bytes)
        {
            return UnicodeTextObexHeader.Create((ObexHeaderId)bytes[0],
                GetHeaderSize(bytes),
                ObexBitConverter.ToUnicodeString(ExtractValueBytes(bytes)).TrimEnd(char.MinValue));
        }

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
