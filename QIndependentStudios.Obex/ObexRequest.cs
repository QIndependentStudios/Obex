using QIndependentStudios.Obex.Header;
using System;
using System.Collections.Generic;

namespace QIndependentStudios.Obex
{
    /// <summary>
    /// Represents a generic Obex request.
    /// </summary>
    public class ObexRequest : ObexRequestBase
    {
        private ObexRequest(ObexOpCode opCode,
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

        /// <summary>
        /// Creates a new instance of the <see cref="ObexRequest"/>.
        /// </summary>
        /// <param name="opCode">The OpCode of the request.</param>
        /// <param name="headers">A collection of Obex headers.</param>
        /// <returns>The created request.</returns>
        public static ObexRequest Create(ObexOpCode opCode, params ObexHeader[] headers)
        {
            return new ObexRequest(opCode, null, headers);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ObexRequest"/>.
        /// </summary>
        /// <param name="opCode">The OpCode of the request.</param>
        /// <param name="headers">A collection of Obex headers.</param>
        /// <returns>The created request.</returns>
        public static ObexRequest Create(ObexOpCode opCode, IEnumerable<ObexHeader> headers)
        {
            return new ObexRequest(opCode, null, headers);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ObexRequest"/>.
        /// </summary>
        /// <param name="opCode">The OpCode of the request.</param>
        /// <param name="requestLength">The request length. Provide this value when deserializing from binary data.</param>
        /// <param name="headers">A collection of Obex headers.</param>
        /// <returns>The created request.</returns>
        public static ObexRequest Create(ObexOpCode opCode, ushort requestLength, params ObexHeader[] headers)
        {
            return new ObexRequest(opCode, requestLength, headers);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ObexRequest"/>.
        /// </summary>
        /// <param name="opCode">The OpCode of the request.</param>
        /// <param name="requestLength">The request length. Provide this value when deserializing from binary data.</param>
        /// <param name="headers">A collection of Obex headers.</param>
        /// <returns>The created request.</returns>
        public static ObexRequest Create(ObexOpCode opCode, ushort requestLength, IEnumerable<ObexHeader> headers)
        {
            return new ObexRequest(opCode, requestLength, headers);
        }
    }
}
