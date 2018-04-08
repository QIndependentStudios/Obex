using QIndependentStudios.Obex.Header;

namespace QIndependentStudios.Obex.Converter.Header
{
    public class GuidObexHeaderConverter : RawObexHeaderConverter
    {
        public override ObexHeader FromBytes(byte[] bytes)
        {
            return GuidObexHeader.Create((ObexHeaderId)bytes[0],
                ObexBitConverter.ToGuid(ExtractValueBytes(bytes)),
                GetHeaderSize(bytes));
        }

        protected override byte[] ValueToBytes(ObexHeader header)
        {
            if (header is GuidObexHeader guidheader)
                return ObexBitConverter.GetBytes(guidheader.Value);

            return new byte[0];
        }
    }
}
