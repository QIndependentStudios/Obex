namespace QIndependentStudios.Obex
{
    /// <summary>
    /// Specifies the Obex request type.
    /// </summary>
    public enum ObexOpCode : byte
    {
        /// <summary>
        /// Connect request OpCode.
        /// </summary>
        Connect = 0x80,
        /// <summary>
        /// Disconnect request OpCode.
        /// </summary>
        Disconnect = 0x81,
        /// <summary>
        /// Put request OpCode with final bit.
        /// </summary>
        Put = 0x82,
        /// <summary>
        /// Put request OpCode.
        /// </summary>
        PutContinue = 0x02,
        /// <summary>
        /// Get request OpCode with final bit.
        /// </summary>
        Get = 0x83,
        /// <summary>
        /// Get request OpCode.
        /// </summary>
        GetContinue = 0x03,
        /// <summary>
        /// Set Path request OpCode.
        /// </summary>
        SetPath = 0x85,
        /// <summary>
        /// Abort request OpCode.
        /// </summary>
        Abort = 0xFF
    }
}
