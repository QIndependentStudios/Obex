using QIndependentStudios.Obex.Comparison;
using QIndependentStudios.Obex.Header;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex
{
    /// <summary>
    /// Provides a base class for an Obex response.
    /// </summary>
    public class ObexResponseBase
    {
        private readonly List<ObexHeader> _headers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObexRequestBase"/> class.
        /// </summary>
        /// <param name="responseCode">The response code determining the type of the response.</param>
        /// <param name="responseLength">The response length. Provide this value when deserializing from binary data.</param>
        /// <param name="headers">A collection of Obex headers.</param>
        protected ObexResponseBase(ObexResponseCode responseCode,
            ushort? responseLength,
            IEnumerable<ObexHeader> headers)
        {
            ResponseCode = responseCode;
            ActualLength = responseLength;
            _headers = headers?.ToList() ?? new List<ObexHeader>();
        }

        /// <summary>
        /// Gets the response's response code.
        /// </summary>
        public ObexResponseCode ResponseCode { get; }

        /// <summary>
        /// If available, gets the total length of the binary data of the the response.
        /// Usually set if the response was created by deserialization.
        /// </summary>
        public ushort? ActualLength { get; }

        /// <summary>
        /// Gets the collection Obex headers.
        /// </summary>
        public IReadOnlyCollection<ObexHeader> Headers => _headers.AsReadOnly();

        /// <summary>
        /// Gets a collection headers that has the specified id.
        /// </summary>
        /// <param name="id">The header id to match.</param>
        /// <returns>A collection of Obex headers.</returns>
        public IEnumerable<ObexHeader> GetHeadersForId(ObexHeaderId id)
        {
            foreach (var item in _headers)
            {
                if (item.Id == id)
                    yield return item;
            }
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is ObexResponseBase response
                && new SequenceEqualityComparer<ObexHeader>().Equals(_headers, response._headers)
                && ResponseCode == response.ResponseCode
                && EqualityComparer<ushort?>.Default.Equals(ActualLength, response.ActualLength);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hashCode = 79378678;
            hashCode = hashCode * -1521134295 + new SequenceEqualityComparer<ObexHeader>().GetHashCode(_headers);
            hashCode = hashCode * -1521134295 + ResponseCode.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<ushort?>.Default.GetHashCode(ActualLength);
            return hashCode;
        }
    }
}
