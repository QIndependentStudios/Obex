using QIndependentStudios.Obex.Header;
using System.Linq;

namespace QIndependentStudios.Obex.Converter.Header
{
    public class TextObexHeaderConverter : RawObexHeaderConverter
    {
        public override ObexHeader FromBytes(byte[] bytes)
        {
            return TextObexHeader.Create((ObexHeaderId)bytes[0],
                GetHeaderSize(bytes),
                ObexBitConverter.ToString(ExtractValueBytes(bytes)).TrimEnd(char.MinValue));
        }

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
