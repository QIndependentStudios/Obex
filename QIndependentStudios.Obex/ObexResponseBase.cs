using QIndependentStudios.Obex.Comparison;
using QIndependentStudios.Obex.Header;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex
{
    public class ObexResponseBase
    {
        private readonly List<ObexHeader> _headers;

        protected ObexResponseBase(ObexResponseCode responseCode,
            ushort? responseLength,
            IEnumerable<ObexHeader> headers)
        {
            ResponseCode = responseCode;
            ActualLength = responseLength;
            _headers = headers?.ToList() ?? new List<ObexHeader>();
        }

        public ObexResponseCode ResponseCode { get; }
        public ushort? ActualLength { get; }
        public IReadOnlyCollection<ObexHeader> Headers => _headers.AsReadOnly();

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
            return obj is ObexResponseBase response
                && new SequenceEqualityComparer<ObexHeader>().Equals(_headers, response._headers)
                && ResponseCode == response.ResponseCode
                && EqualityComparer<ushort?>.Default.Equals(ActualLength, response.ActualLength);
        }

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
