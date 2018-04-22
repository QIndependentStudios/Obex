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
        /// Gets an Obex header's id.
        /// </summary>
        /// <param name="id">The Obex header id to retrieve the encoding from.</param>
        /// <returns></returns>
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
