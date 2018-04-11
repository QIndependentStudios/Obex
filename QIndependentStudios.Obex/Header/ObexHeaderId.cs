namespace QIndependentStudios.Obex.Header
{
    /// <summary>
    /// Specifies the type of value an Obex header has.
    /// </summary>
    public enum ObexHeaderId : byte
    {
        /// <summary>
        /// Unknown or undefined Obex header id.
        /// </summary>
        None = 0x00,
        /// <summary>
        /// Name Obex header id.
        /// </summary>
        Name = 0x01,
        /// <summary>
        /// Type Obex header id.
        /// </summary>
        Type = 0x42,
        /// <summary>
        /// Length Obex header id.
        /// </summary>
        Length = 0xC3,
        /// <summary>
        /// Time Obex header id.
        /// </summary>
        Time = 0x44,
        /// <summary>
        /// Legacy time Obex header id.
        /// </summary>
        Time4Byte = 0xC4,
        /// <summary>
        /// Description Obex header id.
        /// </summary>
        Description = 0x05,
        /// <summary>
        /// Target Obex header id.
        /// </summary>
        Target = 0x46,
        /// <summary>
        /// Http Obex header id.
        /// </summary>
        Http = 0x47,
        /// <summary>
        /// Body Obex header id.
        /// </summary>
        Body = 0x48,
        /// <summary>
        /// End of Body Obex header id.
        /// </summary>
        EndOfBody = 0x49,
        /// <summary>
        /// Who Obex header id.
        /// </summary>
        Who = 0x4A,
        /// <summary>
        /// Connection Id Obex header id.
        /// </summary>
        ConnectionId = 0xCB,
        /// <summary>
        /// Application Parameter Obex header id.
        /// </summary>
        ApplicationParameter = 0x4C,
        /// <summary>
        /// Authentication Challenge Obex header id.
        /// </summary>
        AuthenticationChallenge = 0x4D,
        /// <summary>
        /// Authentication Response Obex header id.
        /// </summary>
        AuthenticationResponse = 0x4E,
        /// <summary>
        /// Object Class Obex header id.
        /// </summary>
        ObjectClass = 0x4F,
        /// <summary>
        /// Wan Uuid Obex header id.
        /// </summary>
        WanUuid = 0x50,
        /// <summary>
        /// Session Parameter Obex header id.
        /// </summary>
        SessionParameter = 0x52,
        /// <summary>
        /// Session Sequence Number Obex header id.
        /// </summary>
        SessionSequenceNumber = 0x93,
        /// <summary>
        /// Action Obex header id.
        /// </summary>
        Action = 0x94,
        /// <summary>
        /// Single Response Mode Obex header id.
        /// </summary>
        SingleResponseMode = 0x97,
        /// <summary>
        /// Single Response Mode Parameter Obex header id.
        /// </summary>
        SingleResponseModeParameter = 0x98,
        /// <summary>
        /// Count Obex header id.
        /// </summary>
        Count = 0xC0,
        /// <summary>
        /// Creator Obex header id.
        /// </summary>
        Creator = 0xCF,
        /// <summary>
        /// Permissions Obex header id.
        /// </summary>
        Permissions = 0xD6
    }
}
