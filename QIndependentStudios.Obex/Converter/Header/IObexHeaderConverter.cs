using QIndependentStudios.Obex.Header;

namespace QIndependentStudios.Obex.Converter.Header
{
    public interface IObexHeaderConverter
    {
        byte[] ToBytes(ObexHeader header);
        ObexHeader FromBytes(byte[] bytes);
    }
}
