using QIndependentStudios.Obex.Converter.Header;
using QIndependentStudios.Obex.Header;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex.Converter.Response
{
    public class ObexResponseConverter : IObexResponseConverter
    {
        protected virtual int FieldBytesLength => 3;

        public byte[] ToBytes(ObexResponseBase response)
        {
            var bytes = GetFieldBytes(response);

            foreach (var header in response.Headers)
            {
                bytes.AddRange(ObexHeaderSerializer.Serialize(header));
            }

            var sizeBytes = ObexBitConverter.GetBytes((ushort)bytes.Count);
            bytes[1] = sizeBytes[0];
            bytes[2] = sizeBytes[1];

            return bytes.ToArray();
        }

        protected virtual List<byte> GetFieldBytes(ObexResponseBase response)
        {
            return new List<byte> { (byte)response.ResponseCode, 0x00, 0x00 };
        }

        public ObexResponseBase FromBytes(byte[] bytes)
        {
            return FromBytesCore(bytes);
        }

        protected virtual ObexResponseBase FromBytesCore(byte[] bytes)
        {
            var responseCode = (ObexResponseCode)bytes[0];
            var responseLength = GetResponseLength(bytes);
            return ObexResponse.Create(responseCode,
                responseLength,
                DeserializeHeaders(bytes, responseLength));
        }

        protected virtual ushort GetResponseLength(byte[] bytes)
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
