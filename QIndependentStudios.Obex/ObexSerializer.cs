using QIndependentStudios.Obex.Converter.Request;
using QIndependentStudios.Obex.Converter.Response;
using System.Collections.Generic;

namespace QIndependentStudios.Obex
{
    /// <summary>
    /// Serializes and deserializes Obex request and response objects to and from binary format.
    /// </summary>
    public class ObexSerializer
    {
        private static readonly Dictionary<ObexOpCode, IObexRequestConverter> RequestConverters = new Dictionary<ObexOpCode, IObexRequestConverter>
        {
            { ObexOpCode.Connect, ObexConnectRequestConverter.Instance },
            { ObexOpCode.SetPath, ObexSetPathRequestConverter.Instance }
        };

        /// <summary>
        /// Serializes the specified request into binary data.
        /// </summary>
        /// <param name="request">The request to serialize.</param>
        /// <returns>Binary data that represents the serialized request.</returns>
        public static byte[] SerializeRequest(ObexRequestBase request)
        {
            var converter = GetRequestConverter(request.OpCode);
            return converter.ToBytes(request);
        }

        /// <summary>
        /// Deserializes the specified bytes.
        /// </summary>
        /// <param name="bytes">The bytes to deserialize.</param>
        /// <returns>A deserialized Obex request object.</returns>
        public static ObexRequestBase DeserializeRequest(byte[] bytes)
        {
            var opCode = (ObexOpCode)bytes[0];

            var converter = GetRequestConverter(opCode);
            return converter.FromBytes(bytes);
        }

        /// <summary>
        /// Serializes the specified response into binary data.
        /// </summary>
        /// <param name="response">The response to serialize.</param>
        /// <returns>Binary data that represents the serialized response.</returns>
        public static byte[] SerializeResponse(ObexResponseBase response)
        {
            var converter = response is ObexConnectResponse
                ? ObexConnectResponseConverter.Instance
                : ObexResponseConverter.Instance;

            return converter.ToBytes(response);
        }

        /// <summary>
        /// Deserializes the specified bytes.
        /// </summary>
        /// <param name="bytes">The bytes to deserialize.</param>
        /// <param name="isConnectResponse">Whether the data should be deserialized as an Obex connect response.</param>
        /// <returns>A deserialized Obex response object.</returns>
        public static ObexResponseBase DeserializeResponse(byte[] bytes, bool isConnectResponse)
        {
            var converter = isConnectResponse
                ? ObexConnectResponseConverter.Instance
                : ObexResponseConverter.Instance;

            return converter.FromBytes(bytes);
        }

        private static IObexRequestConverter GetRequestConverter(ObexOpCode opCode)
        {
            if (RequestConverters.ContainsKey(opCode))
                return RequestConverters[opCode];

            return ObexRequestConverter.Instance;
        }
    }
}
