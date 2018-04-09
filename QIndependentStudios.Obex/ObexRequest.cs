using QIndependentStudios.Obex.Header;
using System;
using System.Collections.Generic;

namespace QIndependentStudios.Obex
{
    public class ObexRequest : ObexRequestBase
    {
        protected ObexRequest(ObexOpCode opCode,
            ushort? requestLength,
            IEnumerable<ObexHeader> headers)
            : base(opCode, requestLength, headers)
        {
            if (opCode == ObexOpCode.Connect)
                throw new ArgumentException($"Use {nameof(ObexConnectRequest)}." +
                    $"{nameof(ObexConnectRequest.Create)}() to create an Obex Connect request.");

            if (opCode == ObexOpCode.SetPath)
                throw new ArgumentException($"Use {nameof(ObexSetPathRequest)}." +
                    $"{nameof(ObexSetPathRequest.Create)}() to create an Obex SetPath request.");
        }

        public static ObexRequest Create(ObexOpCode opCode, params ObexHeader[] headers)
        {
            return new ObexRequest(opCode, null, headers);
        }

        public static ObexRequest Create(ObexOpCode opCode, IEnumerable<ObexHeader> headers)
        {
            return new ObexRequest(opCode, null, headers);
        }

        public static ObexRequest Create(ObexOpCode opCode, ushort requestLength, params ObexHeader[] headers)
        {
            return new ObexRequest(opCode, requestLength, headers);
        }

        public static ObexRequest Create(ObexOpCode opCode, ushort requestLength, IEnumerable<ObexHeader> headers)
        {
            return new ObexRequest(opCode, requestLength, headers);
        }
    }
}
