using QIndependentStudios.Obex.Header;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex
{
    public class ObexResponse : ObexResponseBase
    {
        protected ObexResponse(ObexResponseCode responseCode,
            ushort? responseLength,
            IEnumerable<ObexHeader> headers)
            : base(responseCode, responseLength, headers)
        { }

        public static ObexResponse Create(ObexResponseCode responseCode, params ObexHeader[] headers)
        {
            return Create(responseCode, headers.ToList());
        }

        public static ObexResponse Create(ObexResponseCode responseCode,
            ushort responseLength,
            params ObexHeader[] headers)
        {
            return Create(responseCode, responseLength, headers.ToList());
        }

        public static ObexResponse Create(ObexResponseCode responseCode, IEnumerable<ObexHeader> headers)
        {
            return new ObexResponse(responseCode, null, headers.ToList());
        }

        public static ObexResponse Create(ObexResponseCode responseCode,
            ushort responseLength,
            IEnumerable<ObexHeader> headers)
        {
            return new ObexResponse(responseCode, responseLength, headers.ToList());
        }
    }
}
