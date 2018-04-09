using QIndependentStudios.Obex.Path;
using System;
using System.Collections.Generic;

namespace QIndependentStudios.Obex.Converter.Request
{
    public class ObexSetPathRequestConverter : ObexRequestConverter
    {
        protected override int FieldBytesLength => 5;

        protected override List<byte> GetFieldBytes(ObexRequestBase request)
        {
            if (request.OpCode != ObexOpCode.SetPath || !(request is ObexSetPathRequest setPathRequest))
                throw new ArgumentException($"Request OpCode must be {ObexOpCode.SetPath} and must be of type {nameof(ObexSetPathRequest)}.");

            var bytes = new List<byte>
            {
                (byte)setPathRequest.OpCode,
                0x00,
                0x00,
                (byte)setPathRequest.Flags,
                setPathRequest.Constant
            };

            return bytes;
        }

        protected override ObexRequestBase FromBytesCore(byte[] bytes)
        {
            var opCode = (ObexOpCode)bytes[0];
            if (opCode != ObexOpCode.SetPath)
                throw new ArgumentException($"OpCode in byte data appears to be {opCode} but expected {ObexOpCode.SetPath}.");

            var requestLength = GetRequestLength(bytes);
            ObexSetPathFlag setPathFlag = (ObexSetPathFlag)bytes[3];
            var constant = bytes[4];

            return ObexSetPathRequest.Create(requestLength,
                setPathFlag,
                constant,
                DeserializeHeaders(bytes, requestLength));
        }
    }
}
