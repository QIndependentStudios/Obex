namespace QIndependentStudios.Obex.Converter.Request
{
    public interface IObexRequestConverter
    {
        byte[] ToBytes(ObexRequestBase request);
        ObexRequestBase FromBytes(byte[] bytes);
    }
}
