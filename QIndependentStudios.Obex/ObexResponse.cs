using QIndependentStudios.Obex.Header;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex
{
    /// <summary>
    /// Represents a generic Obex response.
    /// </summary>
    public class ObexResponse : ObexResponseBase
    {
        private ObexResponse(ObexResponseCode responseCode,
            ushort? responseLength,
            IEnumerable<ObexHeader> headers)
            : base(responseCode, responseLength, headers)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ObexResponse"/>.
        /// </summary>
        /// <param name="responseCode">The response code of the response.</param>
        /// <param name="headers">A collection of Obex headers.</param>
        /// <returns>The created response.</returns>
        public static ObexResponse Create(ObexResponseCode responseCode, params ObexHeader[] headers)
        {
            return Create(responseCode, headers.ToList());
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ObexResponse"/>.
        /// </summary>
        /// <param name="responseCode">The response code of the response.</param>
        /// <param name="responseLength">The response length. Provide this value when deserializing from binary data.</param>
        /// <param name="headers">A collection of Obex headers.</param>
        /// <returns>The created response.</returns>
        public static ObexResponse Create(ObexResponseCode responseCode,
            ushort responseLength,
            params ObexHeader[] headers)
        {
            return Create(responseCode, responseLength, headers.ToList());
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ObexResponse"/>.
        /// </summary>
        /// <param name="responseCode">The response code of the response.</param>
        /// <param name="headers">A collection of Obex headers.</param>
        /// <returns>The created response.</returns>
        public static ObexResponse Create(ObexResponseCode responseCode, IEnumerable<ObexHeader> headers)
        {
            return new ObexResponse(responseCode, null, headers.ToList());
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ObexResponse"/>.
        /// </summary>
        /// <param name="responseCode">The response code of the response.</param>
        /// <param name="responseLength">The response length. Provide this value when deserializing from binary data.</param>
        /// <param name="headers">A collection of Obex headers.</param>
        /// <returns>The created response.</returns>
        public static ObexResponse Create(ObexResponseCode responseCode,
            ushort responseLength,
            IEnumerable<ObexHeader> headers)
        {
            return new ObexResponse(responseCode, responseLength, headers.ToList());
        }
    }
}
