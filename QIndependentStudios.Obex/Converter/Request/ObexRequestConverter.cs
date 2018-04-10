using QIndependentStudios.Obex.Converter.Header;
using QIndependentStudios.Obex.Header;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex.Converter.Request
{
    /// <summary>
    /// Converts generic Obex requests to and from binary data.
    /// </summary>
    public class ObexRequestConverter : IObexRequestConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObexRequestConverter"/> class.
        /// </summary>
        protected ObexRequestConverter()
        { }

        /// <summary>
        /// Gets the instance of the <see cref="ObexRequestConverter"/> class.
        /// </summary>
        public static ObexRequestConverter Instance { get; } = new ObexRequestConverter();

        /// <summary>
        /// The number of bytes that represents the Obex request's field values.
        /// </summary>
        protected virtual int FieldBytesLength => 3;

        /// <inheritdoc/>
        public byte[] ToBytes(ObexRequestBase request)
        {
            var bytes = GetFieldBytes(request);

            foreach (var header in request.Headers)
            {
                bytes.AddRange(ObexHeaderSerializer.Serialize(header));
            }

            var sizeBytes = ObexBitConverter.GetBytes((ushort)bytes.Count);
            bytes[1] = sizeBytes[0];
            bytes[2] = sizeBytes[1];

            return bytes.ToArray();
        }

        /// <inheritdoc/>
        public ObexRequestBase FromBytes(byte[] bytes)
        {
            return FromBytesCore(bytes);
        }

        /// <summary>
        /// Constructs the Obex request's fields in binary format.
        /// </summary>
        /// <param name="request">The Obex request to get field data.</param>
        /// <returns>Binary data representing the fields in the request.</returns>
        protected virtual List<byte> GetFieldBytes(ObexRequestBase request)
        {
            if (request.OpCode == ObexOpCode.Connect || request is ObexConnectRequest)
                throw new ArgumentException($"Requests with OpCode {ObexOpCode.Connect} or of type {nameof(ObexConnectRequest)} " +
                    $"is not supported. Use {nameof(ObexConnectRequestConverter)}.{nameof(ObexConnectRequestConverter.ToBytes)}().");

            if (request.OpCode == ObexOpCode.SetPath || request is ObexSetPathRequest)
                throw new ArgumentException($"Requests with OpCode {ObexOpCode.SetPath} or of type {nameof(ObexSetPathRequest)} " +
                    $"is not supported. Use {nameof(ObexSetPathRequestConverter)}.{nameof(ObexSetPathRequestConverter.ToBytes)}().");

            return new List<byte> { (byte)request.OpCode, 0x00, 0x00 };
        }

        /// <summary>
        /// Core conversion logic to deserialize binary data to an appropriate Obex request object.
        /// </summary>
        /// <param name="bytes">The binary data.</param>
        /// <returns>An Obex request object.</returns>
        protected virtual ObexRequestBase FromBytesCore(byte[] bytes)
        {
            var opCode = (ObexOpCode)bytes[0];
            if (opCode == ObexOpCode.Connect)
                throw new ArgumentException($"OpCode in byte data appears to be {opCode}. Use " +
                    $"{nameof(ObexConnectRequestConverter)}.{nameof(ObexConnectRequestConverter.FromBytes)}() " +
                    "to convert from bytes.");

            if (opCode == ObexOpCode.SetPath)
                throw new ArgumentException($"OpCode in byte data appears to be {opCode}. Use " +
                    $"{nameof(ObexSetPathRequestConverter)}.{nameof(ObexSetPathRequestConverter.FromBytes)}() " +
                    $"to convert from bytes.");

            var requestLength = GetRequestLength(bytes);

            return ObexRequest.Create(opCode,
                requestLength,
                DeserializeHeaders(bytes, requestLength));
        }

        /// <summary>
        /// Gets the request binary data length from the Obex request binary data.
        /// </summary>
        /// <param name="bytes">The binary data.</param>
        /// <returns>The defined length of the Obex request data.</returns>
        protected virtual ushort GetRequestLength(byte[] bytes)
        {
            return ObexBitConverter.ToUInt16(new byte[] { bytes[1], bytes[2] });
        }

        /// <summary>
        /// Deserializes the headers in an Obex request binary data.
        /// </summary>
        /// <param name="bytes">The binary data.</param>
        /// <param name="requestLength">The defined length of the Obex request data.</param>
        /// <returns>A collection of Obex headers.</returns>
        protected virtual IEnumerable<ObexHeader> DeserializeHeaders(byte[] bytes, ushort requestLength)
        {
            return ObexHeaderSerializer.DeserializeAll(
                new ArraySegment<byte>(bytes, FieldBytesLength, requestLength - FieldBytesLength).ToArray());
        }
    }
}
