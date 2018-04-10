using QIndependentStudios.Obex.Header;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex.Converter.Header
{
    /// <summary>
    /// Serializes and deserializes <see cref="ObexHeader"/> objects in binary format.
    /// </summary>
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

        /// <summary>
        /// Serializes the specified header into binary data.
        /// </summary>
        /// <param name="header">The header to serialize.</param>
        /// <returns>Binary data that represents the serialized header.</returns>
        public static byte[] Serialize(ObexHeader header)
        {
            if (!(header is RawObexHeader) && !_converters.ContainsKey(header.Id))
                throw new System.NotSupportedException($"No converters found for Obex Headers with id {header.Id}.");

            var converter = header is RawObexHeader
                ? RawObexHeaderConverter.Instance
                : _converters[header.Id];
            return converter.ToBytes(header);
        }


        /// <summary>
        /// Deserializes the specified bytes.
        /// </summary>
        /// <param name="bytes">The bytes to deserialize.</param>
        /// <returns>A deserialized Obex header object.</returns>
        public static ObexHeader Deserialize(byte[] bytes)
        {
            var headerId = (ObexHeaderId)bytes[0];
            var converter = _converters.ContainsKey(headerId)
                ? _converters[headerId]
                : RawObexHeaderConverter.Instance;
            return converter.FromBytes(bytes);
        }

        /// <summary>
        /// Deserializes the specified bytes into a collection of Obex headers.
        /// </summary>
        /// <param name="bytes">The bytes to deserialize.</param>
        /// <returns>A collection of deserialized Obex headers.</returns>
        public static IEnumerable<ObexHeader> DeserializeAll(byte[] bytes)
        {
            var headers = new List<ObexHeader>();
            var currentPosition = 0;
            while (bytes.Length - currentPosition > 0)
            {
                var header = Deserialize(bytes.Skip(currentPosition).ToArray());
                headers.Add(header);
                currentPosition += (int)header.ActualLength;
            }

            return headers;
        }
    }
}
