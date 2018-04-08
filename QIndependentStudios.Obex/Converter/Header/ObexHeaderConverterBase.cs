using QIndependentStudios.Obex.Header;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex.Converter.Header
{
    public abstract class ObexHeaderConverterBase : IObexHeaderConverter
    {
        protected const int HeaderLengthBytesSize = 2;
        public virtual int? HeaderLengthOverride => null;

        protected byte[] ExtractValueBytes(byte[] bytes)
        {
            var offset = 1;
            var length = 0;

            if (HeaderLengthOverride != null)
                length = HeaderLengthOverride.Value - offset;
            else
            {
                length = ObexBitConverter.ToUInt16(new ArraySegment<byte>(bytes, offset, HeaderLengthBytesSize).ToArray());
                offset += HeaderLengthBytesSize;
                length -= offset;
            }

            return new ArraySegment<byte>(bytes, offset, length).ToArray();
        }

        protected abstract byte[] ValueToBytes(ObexHeader header);

        protected void SetHeaderSize(List<byte> bytes)
        {
            bytes.InsertRange(1, new byte[] { 0x00, 0x00 });
            var sizeBytes = ObexBitConverter.GetBytes((ushort)bytes.Count);
            bytes[1] = sizeBytes[0];
            bytes[2] = sizeBytes[1];
        }

        protected ushort GetHeaderSize(byte[] bytes)
        {
            return ObexBitConverter.ToUInt16(new byte[] { bytes[1], bytes[2] });
        }

        public abstract ObexHeader FromBytes(byte[] bytes);

        public byte[] ToBytes(ObexHeader header)
        {
            var bytes = new List<byte> { (byte)header.Id };

            bytes.AddRange(ValueToBytes(header));

            if (HeaderLengthOverride == null)
                SetHeaderSize(bytes);

            return bytes.ToArray();
        }
    }
}
