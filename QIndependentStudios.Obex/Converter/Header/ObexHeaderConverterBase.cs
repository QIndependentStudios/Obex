using QIndependentStudios.Obex.Header;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex.Converter.Header
{
    /// <summary>
    /// A base converter class to convert Obex headers to and from binary data.
    /// </summary>
    public abstract class ObexHeaderConverterBase : IObexHeaderConverter
    {
        /// <summary>
        /// The number of bytes that defines the length of a header.
        /// </summary>
        protected const int HeaderLengthBytesSize = 2;

        /// <summary>
        /// Converts binary data to an Obex header object.
        /// </summary>
        /// <param name="bytes">The binary data to deserialize.</param>
        /// <returns>The deserialized Obex header.</returns>
        public abstract ObexHeader FromBytes(byte[] bytes);

        /// <summary>
        /// Converts an Obex header to binary data.
        /// </summary>
        /// <param name="header">The header to convert.</param>
        /// <returns>Binary data representing the Obex header.</returns>
        public byte[] ToBytes(ObexHeader header)
        {
            var bytes = new List<byte> { (byte)header.Id };

            bytes.AddRange(ValueToBytes(header));

            if (IsValuePrefixedWithLength(header.Id))
                SetHeaderSize(bytes);

            return bytes.ToArray();
        }

        /// <summary>
        /// Extracts only the bytes that rerpresents the value of a header and stripes away the id and length bytes.
        /// </summary>
        /// <param name="bytes">The entire binary data of an Obex header.</param>
        /// <returns>The binary data of the header's value without the header's id or length bytes.</returns>
        protected byte[] ExtractValueBytes(byte[] bytes)
        {
            var offset = 1;
            int valueLength;

            switch (ObexHeaderUtil.GetHeaderEncoding((ObexHeaderId)bytes[0]))
            {
                case ObexHeaderEncoding.SingleByte:
                    valueLength = 1;
                    break;
                case ObexHeaderEncoding.FourBytes:
                    valueLength = 4;
                    break;
                default:
                    valueLength = ObexBitConverter.ToUInt16(new ArraySegment<byte>(bytes,
                        offset,
                        HeaderLengthBytesSize).ToArray());
                    offset += HeaderLengthBytesSize;
                    valueLength -= offset;
                    break;
            }

            return new ArraySegment<byte>(bytes, offset, valueLength).ToArray();
        }

        /// <summary>
        /// Converts the value of the Obex header to binary data.
        /// </summary>
        /// <param name="header">The header whose value will be converted.</param>
        /// <returns>The binary data of the header's value.</returns>
        protected abstract byte[] ValueToBytes(ObexHeader header);

        /// <summary>
        /// Adds the header length bytes and sets the value to be the number of overall bytes.
        /// </summary>
        /// <param name="bytes">The bytes representing a header.</param>
        protected void SetHeaderSize(List<byte> bytes)
        {
            bytes.InsertRange(1, new byte[] { 0x00, 0x00 });
            var sizeBytes = ObexBitConverter.GetBytes((ushort)bytes.Count);
            bytes[1] = sizeBytes[0];
            bytes[2] = sizeBytes[1];
        }

        /// <summary>
        /// Extracts the overall length of the header.
        /// </summary>
        /// <param name="bytes">The binary data of an Obex header.</param>
        /// <returns></returns>
        protected ushort GetHeaderSize(byte[] bytes)
        {
            return ObexBitConverter.ToUInt16(new[] { bytes[1], bytes[2] });
        }

        private static bool IsValuePrefixedWithLength(ObexHeaderId id)
        {
            var encoding = ObexHeaderUtil.GetHeaderEncoding(id);
            return encoding == ObexHeaderEncoding.NullTermUnicodeWithLength
                || encoding == ObexHeaderEncoding.ByteSequenceWithLength;
        }
    }
}
