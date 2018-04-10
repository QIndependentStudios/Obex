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
        /// Gets the header's size that should be used instead of calculating it.
        /// Override this a head has a fixed length and does not have header length bytes.
        /// </summary>
        public virtual int? HeaderLengthOverride => null;

        /// <inheritdoc/>
        public abstract ObexHeader FromBytes(byte[] bytes);

        /// <inheritdoc/>
        public byte[] ToBytes(ObexHeader header)
        {
            var bytes = new List<byte> { (byte)header.Id };

            bytes.AddRange(ValueToBytes(header));

            if (HeaderLengthOverride == null)
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
            return ObexBitConverter.ToUInt16(new byte[] { bytes[1], bytes[2] });
        }
    }
}
