using System;

namespace QIndependentStudios.Obex.Header
{
    /// <summary>
    /// Specfies the encoding of the header data using the 7th and 8th bits of the header id.
    /// </summary>
    [Flags]
    public enum ObexHeaderEncoding
    {
        /// <summary>
        /// Header is encoded with a single byte id, two bytes for the length of the entire header,
        /// and finally the value as unicode text ending with 0x00 0x00 when the value is not empty.
        /// </summary>
        NullTermUnicodeWithLength = 0x00,

        /// <summary>
        /// Header is encoded with a single byte id, two bytes for the length of the entire header,
        /// and finally a sequence of bytes for the header value.
        /// </summary>
        ByteSequenceWithLength = 0x40,

        /// <summary>
        /// Header is encoded with a single byte id and a single byte value.
        /// </summary>
        SingleByte = 0x80,

        /// <summary>
        /// Header is encoded with a single byte id and a four byte value.
        /// </summary>
        FourBytes = 0xC0
    }
}
