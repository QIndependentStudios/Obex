using QIndependentStudios.Obex.Header;

namespace QIndependentStudios.Obex.Converter.Header
{
    public class ByteObexHeaderConverter : ObexHeaderConverterBase
    {
        protected ByteObexHeaderConverter()
        { }

        public static ByteObexHeaderConverter Instance { get; } = new ByteObexHeaderConverter();

        public override int? HeaderLengthOverride => 2;

        public override ObexHeader FromBytes(byte[] bytes)
        {
            return ByteObexHeader.Create((ObexHeaderId)bytes[0], ExtractValueBytes(bytes)[0]);
        }

        protected override byte[] ValueToBytes(ObexHeader header)
        {
            if (header is ByteObexHeader byteHeader)
                return new byte[] { byteHeader.Value };

            return new byte[0];
        }
    }
}
