using System;
using System.Collections.Generic;

namespace QIndependentStudios.Obex.Converter.Request
{
    public class ObexConnectRequestConverter : ObexRequestConverter
    {
        protected override int FieldBytesLength => 7;

        protected override List<byte> GetFieldBytes(ObexRequestBase request)
        {
            if (request.OpCode != ObexOpCode.Connect || !(request is ObexConnectRequest connectRequest))
                throw new ArgumentException($"Request OpCode must be {ObexOpCode.Connect} and must be of type {nameof(ObexConnectRequest)}.");

            var bytes = new List<byte>
            {
                (byte)connectRequest.OpCode,
                0x00,
                0x00,
                connectRequest.ObexVersion,
                connectRequest.Flags
            };

            bytes.AddRange(ObexBitConverter.GetBytes(connectRequest.MaxPacketSize));

            return bytes;
        }

        protected override ObexRequestBase FromBytesCore(byte[] bytes)
        {
            var opCode = (ObexOpCode)bytes[0];
            if (opCode != ObexOpCode.Connect)
                throw new ArgumentException($"OpCode in byte data appears to be {opCode} but expected {ObexOpCode.Connect}.");

            var requestLength = GetRequestLength(bytes);
            var obexVersion = bytes[3];
            var flags = bytes[4];
            var maxPacketSize = ObexBitConverter.ToUInt16(new byte[] { bytes[5], bytes[6] });

            return ObexConnectRequest.Create(requestLength,
                obexVersion,
                flags,
                maxPacketSize,
                DeserializeHeaders(bytes, requestLength));
        }
    }
}
