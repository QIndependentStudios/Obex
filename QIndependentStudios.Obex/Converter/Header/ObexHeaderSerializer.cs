using QIndependentStudios.Obex.Converter.Header;
using QIndependentStudios.Obex.Header;
using System.Collections.Generic;
using System.Linq;

namespace QDev.Obex.Header
{
    public class ObexHeaderSerializer
    {
        private static readonly Dictionary<ObexHeaderId, IObexHeaderConverter> _converters = new Dictionary<ObexHeaderId, IObexHeaderConverter>
        {
            { ObexHeaderId.Length, UInt32ObexHeaderConverter.Instance },
            { ObexHeaderId.ConnectionId, UInt32ObexHeaderConverter.Instance },
            { ObexHeaderId.Target, GuidObexHeaderConverter.Instance },
            { ObexHeaderId.Who, GuidObexHeaderConverter.Instance },
            { ObexHeaderId.Type, TextObexHeaderConverter.Instance },
            { ObexHeaderId.Body, TextObexHeaderConverter.Instance },
            { ObexHeaderId.EndOfBody, TextObexHeaderConverter.Instance },
            { ObexHeaderId.Name, UnicodeTextObexHeaderConverter.Instance },
            { ObexHeaderId.Description, UnicodeTextObexHeaderConverter.Instance },
            { ObexHeaderId.ApplicationParameter, AppParamObexHeaderConverter.Instance },
            { ObexHeaderId.SingleResponseMode, ByteObexHeaderConverter.Instance },
            { ObexHeaderId.SingleResponseModeParameter, ByteObexHeaderConverter.Instance }
        };

        public static byte[] Serialize(ObexHeader header)
        {
            if (!(header is RawObexHeader) && !_converters.ContainsKey(header.Id))
                throw new System.NotSupportedException($"No converters found for Obex Headers with id {header.Id}.");

            var converter = header is RawObexHeader
                ? RawObexHeaderConverter.Instance
                : _converters[header.Id];
            return converter.ToBytes(header);
        }

        public static ObexHeader Deserialize(byte[] bytes)
        {
            var headerId = (ObexHeaderId)bytes[0];
            var converter = _converters.ContainsKey(headerId)
                ? _converters[headerId]
                : RawObexHeaderConverter.Instance;
            return converter.FromBytes(bytes);
        }

        public static IEnumerable<ObexHeader> DeserializeAll(byte[] data)
        {
            var headers = new List<ObexHeader>();
            var currentPosition = 0;
            while (data.Length - currentPosition > 0)
            {
                var header = Deserialize(data.Skip(currentPosition).ToArray());
                headers.Add(header);
                currentPosition += (int)header.ActualLength;
            }

            return headers;
        }
    }
}
