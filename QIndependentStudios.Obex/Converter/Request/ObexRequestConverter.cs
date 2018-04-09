using QDev.Obex.Header;
using QIndependentStudios.Obex.Header;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex.Converter.Request
{
    public class ObexRequestConverter : IObexRequestConverter
    {
        protected virtual int FieldBytesLength => 3;

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

        public ObexRequestBase FromBytes(byte[] bytes)
        {
            return FromBytesCore(bytes);
        }

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

        protected virtual ushort GetRequestLength(byte[] bytes)
        {
            return ObexBitConverter.ToUInt16(new byte[] { bytes[1], bytes[2] });
        }

        protected virtual IEnumerable<ObexHeader> DeserializeHeaders(byte[] bytes, ushort responseLength)
        {
            return ObexHeaderSerializer.DeserializeAll(
                new ArraySegment<byte>(bytes, FieldBytesLength, responseLength - FieldBytesLength).ToArray());
        }
    }
}
