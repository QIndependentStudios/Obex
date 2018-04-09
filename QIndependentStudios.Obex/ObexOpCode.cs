namespace QIndependentStudios.Obex
{
    public enum ObexOpCode : byte
    {
        Connect = 0x80,
        Disconnect = 0x81,
        Put = 0x82,
        PutContinue = 0x02,
        Get = 0x83,
        GetContinue = 0x03,
        SetPath = 0x85,
        Abort = 0xFF
    }
}
