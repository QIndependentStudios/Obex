namespace QIndependentStudios.Obex.Header
{
    public enum ObexHeaderId : byte
    {
        None = 0x00,
        Name = 0x01,
        Type = 0x42,
        Length = 0xC3,
        Time = 0x44,
        Time4Byte = 0xC4,
        Description = 0x05,
        Target = 0x46,
        Http = 0x47,
        Body = 0x48,
        EndOfBody = 0x49,
        Who = 0x4A,
        ConnectionId = 0xCB,
        ApplicationParameter = 0x4C,
        AuthenticationChallenge = 0x4D,
        AuthenticationResponse = 0x4E,
        ObjectClass = 0x4F,
        WanUuid = 0x50,
        SessionParameter = 0x52,
        SessionSequenceNumber = 0x93,
        Action = 0x94,
        SingleResponseMode = 0x97,
        SingleResponseModeParameter = 0x98,
        Count = 0xC0,
        Creator = 0xCF,
        Permissions = 0xD6
    }
}
