using System;
using System.Linq;
using System.Text;

namespace QIndependentStudios.Obex.Header
{
    /// <summary>
    /// Utility class to help work with Obex headers.
    /// </summary>
    public class ObexHeaderUtil
    {
        /// <summary>
        /// Gets an Obex header id's encoding defined by the id's two most significant bits.
        /// </summary>
        /// <param name="id">The Obex header id to retrieve the encoding from.</param>
        /// <returns>The encoding embedded in the header id's bytes.</returns>
        public static ObexHeaderEncoding GetHeaderEncoding(ObexHeaderId id)
        {
            if (IsEncoding(id, ObexHeaderEncoding.NullTermUnicodeWithLength))
                return ObexHeaderEncoding.NullTermUnicodeWithLength;
            if (IsEncoding(id, ObexHeaderEncoding.ByteSequenceWithLength))
                return ObexHeaderEncoding.ByteSequenceWithLength;
            if (IsEncoding(id, ObexHeaderEncoding.SingleByte))
                return ObexHeaderEncoding.SingleByte;
            if (IsEncoding(id, ObexHeaderEncoding.FourBytes))
                return ObexHeaderEncoding.FourBytes;

            throw new ArgumentOutOfRangeException();
        }

        /// <summary>
        /// Gets a response's Body or EndOfBody header as a string.
        /// </summary>
        /// <param name="response">The response that contains a Body or EndOfBody header.</param>
        /// <param name="encoding">The encoding to convert the Body or EndOfBody's value into a string.</param>
        /// <returns>The value of the Body or EndOfBody as a string.</returns>
        public static string GetBodyContent(ObexResponseBase response, Encoding encoding)
        {
            if (response.GetHeadersForId(ObexHeaderId.Body).FirstOrDefault() is ByteSequenceObexHeader body)
                return body.GetValueAsString(encoding);

            if (response.GetHeadersForId(ObexHeaderId.EndOfBody).FirstOrDefault() is ByteSequenceObexHeader endOfBody)
                return endOfBody.GetValueAsString(encoding);

            return null;
        }

        private static bool IsEncoding(ObexHeaderId id, ObexHeaderEncoding encoding)
        {
            var encodingMask = (byte)encoding | 0b0011_1111;
            return ((byte)id & encodingMask) == (byte)id;
        }
    }
}
