using QIndependentStudios.Obex.Header;

namespace QIndependentStudios.Obex.Converter.Header
{
    public class UInt32ObexHeaderConverter : ObexHeaderConverterBase
    {
        protected UInt32ObexHeaderConverter()
        { }

        public static UInt32ObexHeaderConverter Instance { get; } = new UInt32ObexHeaderConverter();

        public override int? HeaderLengthOverride => 5;

        public override ObexHeader FromBytes(byte[] bytes)
        {
            return UInt32ObexHeader.Create((ObexHeaderId)bytes[0], ObexBitConverter.ToUInt32(ExtractValueBytes(bytes)));
        }

        protected override byte[] ValueToBytes(ObexHeader header)
        {
            if (header is UInt32ObexHeader uint32Header)
                return ObexBitConverter.GetBytes(uint32Header.Value);

            return new byte[0];
        }
    }
}
