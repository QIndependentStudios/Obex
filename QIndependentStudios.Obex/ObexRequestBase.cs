using QIndependentStudios.Obex.Comparison;
using QIndependentStudios.Obex.Header;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex
{
    public class ObexRequestBase
    {
        private List<ObexHeader> _headers = new List<ObexHeader>();

        protected ObexRequestBase(ObexOpCode opCode,
            ushort? requestLength,
            IEnumerable<ObexHeader> headers)
        {
            OpCode = opCode;
            ActualLength = requestLength;
            _headers = headers?.ToList() ?? new List<ObexHeader>();
        }

        public ObexOpCode OpCode { get; }
        public ushort? ActualLength { get; }
        public IReadOnlyList<ObexHeader> Headers => _headers.AsReadOnly();

        public IEnumerable<ObexHeader> GetHeadersForId(ObexHeaderId id)
        {
            foreach (var item in _headers)
            {
                if (item.Id == id)
                    yield return item;
            }
        }

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
    }
}
