using QIndependentStudios.Obex.Comparison;
using QIndependentStudios.Obex.Header;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex
{
    /// <summary>
    /// Provides a base class for an Obex request.
    /// </summary>
    public class ObexRequestBase
    {
        private readonly List<ObexHeader> _headers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObexRequestBase"/> class.
        /// </summary>
        /// <param name="opCode">The opCode determining the type of the request.</param>
        /// <param name="requestLength">The request length. Provide this value when deserializing from binary data.</param>
        /// <param name="headers">A collection of Obex headers.</param>
        protected ObexRequestBase(ObexOpCode opCode,
            ushort? requestLength,
            IEnumerable<ObexHeader> headers)
        {
            OpCode = opCode;
            ActualLength = requestLength;
            _headers = headers?.ToList() ?? new List<ObexHeader>();
        }

        /// <summary>
        /// Gets the request's OpCode.
        /// </summary>
        public ObexOpCode OpCode { get; }

        /// <summary>
        /// If available, gets the total length of the binary data of the the request.
        /// Usually set if the request was created by deserialization.
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

#pragma warning disable CS1591
        public override bool Equals(object obj)
        {
            return obj is ObexRequestBase request
                && OpCode == request.OpCode
                && EqualityComparer<ushort?>.Default.Equals(ActualLength, request.ActualLength)
                && new SequenceEqualityComparer<ObexHeader>().Equals(_headers, request._headers);
        }

        public override int GetHashCode()
        {
            var hashCode = -1935681702;
            hashCode = hashCode * -1521134295 + new SequenceEqualityComparer<ObexHeader>().GetHashCode(_headers);
            hashCode = hashCode * -1521134295 + OpCode.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<ushort?>.Default.GetHashCode(ActualLength);
            return hashCode;
        }
#pragma warning restore CS1591
    }
}
