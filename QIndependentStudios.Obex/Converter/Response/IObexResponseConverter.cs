namespace QIndependentStudios.Obex.Converter.Response
{
    public interface IObexResponseConverter
    {
        byte[] ToBytes(ObexResponseBase response);
        ObexResponseBase FromBytes(byte[] bytes);
    }
}
