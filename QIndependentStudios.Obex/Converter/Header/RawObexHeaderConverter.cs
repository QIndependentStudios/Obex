using QIndependentStudios.Obex.Header;

namespace QIndependentStudios.Obex.Converter.Header
{
    public class RawObexHeaderConverter : ObexHeaderConverterBase
    {
        protected RawObexHeaderConverter()
        { }

        public static RawObexHeaderConverter Instance { get; } = new RawObexHeaderConverter();

        public override ObexHeader FromBytes(byte[] bytes)
        {
            return RawObexHeader.Create((ObexHeaderId)bytes[0], ExtractValueBytes(bytes));
        }

        protected override byte[] ValueToBytes(ObexHeader header)
        {
            if (header is RawObexHeader rawheader)
                return rawheader.Value;

            return new byte[0];
        }
    }
}
