using QIndependentStudios.Obex.Converter.Request;
using QIndependentStudios.Obex.Converter.Response;
using System.Collections.Generic;

namespace QIndependentStudios.Obex
{
    public class ObexSerializer
    {
        private static Dictionary<ObexOpCode, IObexRequestConverter> _requestConverters = new Dictionary<ObexOpCode, IObexRequestConverter>
        {
            { ObexOpCode.Connect, ObexConnectRequestConverter.Instance },
            { ObexOpCode.SetPath, ObexSetPathRequestConverter.Instance }
        };

        public static byte[] SerializeRequest(ObexRequestBase request)
        {
            var converter = GetRequestConverter(request.OpCode);
            return converter.ToBytes(request);
        }

        public static ObexRequestBase DeserializeRequest(byte[] bytes)
        {
            var opCode = (ObexOpCode)bytes[0];

            var converter = GetRequestConverter(opCode);
            return converter.FromBytes(bytes);
        }

        public static byte[] SerializeResponse(ObexResponseBase response)
        {
            var converter = response is ObexConnectResponse
                ? ObexConnectResponseConverter.Instance
                : ObexResponseConverter.Instance;

            return converter.ToBytes(response);
        }

        public static ObexResponseBase DeserializeResponse(byte[] bytes, bool isConnectResponse)
        {
            var converter = isConnectResponse
                ? ObexConnectResponseConverter.Instance
                : ObexResponseConverter.Instance;

            return converter.FromBytes(bytes);
        }

        private static IObexRequestConverter GetRequestConverter(ObexOpCode opCode)
        {
            if (_requestConverters.ContainsKey(opCode))
                return _requestConverters[opCode];

            return ObexRequestConverter.Instance;
        }
    }
}
