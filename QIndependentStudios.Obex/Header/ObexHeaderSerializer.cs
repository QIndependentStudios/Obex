using QIndependentStudios.Obex.Converter.Header;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex.Header
{
    /// <summary>
    /// Serializes and deserializes <see cref="ObexHeader"/> objects to and from binary format.
    /// </summary>
    public class ObexHeaderSerializer
    {
        private static readonly Dictionary<ObexHeaderId, IObexHeaderConverter> ConvertersById =
            new Dictionary<ObexHeaderId, IObexHeaderConverter>
            {
                { ObexHeaderId.Count, UInt32ObexHeaderConverter.Instance },
                { ObexHeaderId.Name, UnicodeTextObexHeaderConverter.Instance },
                { ObexHeaderId.Length, UInt32ObexHeaderConverter.Instance },
                //TODO: handle time header
                { ObexHeaderId.Description, UnicodeTextObexHeaderConverter.Instance },
                { ObexHeaderId.ConnectionId, UInt32ObexHeaderConverter.Instance },
                { ObexHeaderId.ApplicationParameter, TlvCollectionObexHeaderConverter.Instance },
                { ObexHeaderId.AuthenticationChallenge, TlvCollectionObexHeaderConverter.Instance },
                { ObexHeaderId.AuthenticationResponse, TlvCollectionObexHeaderConverter.Instance },
                { ObexHeaderId.Target, GuidObexHeaderConverter.Instance },
                { ObexHeaderId.Who, GuidObexHeaderConverter.Instance },
                { ObexHeaderId.SingleResponseMode, ByteObexHeaderConverter.Instance },
                { ObexHeaderId.SingleResponseModeParameter, ByteObexHeaderConverter.Instance}
            };

        private static readonly Dictionary<ObexHeaderEncoding, IObexHeaderConverter> ConvertersByEncoding =
            new Dictionary<ObexHeaderEncoding, IObexHeaderConverter>
            {
                { ObexHeaderEncoding.NullTermUnicodeWithLength, UnicodeTextObexHeaderConverter.Instance },
                { ObexHeaderEncoding.ByteSequenceWithLength, ByteSequenceObexHeaderConverter.Instance },
                { ObexHeaderEncoding.SingleByte, ByteObexHeaderConverter.Instance },
                { ObexHeaderEncoding.FourBytes, UInt32ObexHeaderConverter.Instance }
            };

        private static readonly Dictionary<Type, IObexHeaderConverter> ConvertersByType =
            new Dictionary<Type, IObexHeaderConverter>
            {
                { typeof(ByteObexHeader), ByteObexHeaderConverter.Instance },
                { typeof(GuidObexHeader), GuidObexHeaderConverter.Instance },
                { typeof(ByteSequenceObexHeader), ByteSequenceObexHeaderConverter.Instance },
                { typeof(TlvCollectionObexHeader), TlvCollectionObexHeaderConverter.Instance },
                { typeof(UInt32ObexHeader), UInt32ObexHeaderConverter.Instance },
                { typeof(UnicodeTextObexHeader), UnicodeTextObexHeaderConverter.Instance },
            };

        /// <summary>
        /// Serializes the specified header into binary data.
        /// </summary>
        /// <param name="header">The header to serialize.</param>
        /// <returns>Binary data that represents the serialized header.</returns>
        public static byte[] Serialize(ObexHeader header)
        {
            if (!ConvertersByType.ContainsKey(header.GetType()))
                throw new NotSupportedException($"No converter found for type {header.GetType().FullName}.");

            var converter = ConvertersByType[header.GetType()];
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
            var converter = ConvertersById.ContainsKey(headerId)
                ? ConvertersById[headerId]
                : ConvertersByEncoding[ObexHeaderUtil.GetHeaderEncoding(headerId)];
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
                currentPosition += header.ActualLength ?? 0;
            }

            return headers;
        }
    }
}
