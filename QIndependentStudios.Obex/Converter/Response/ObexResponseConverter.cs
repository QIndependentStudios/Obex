using QIndependentStudios.Obex.Converter.Header;
using QIndependentStudios.Obex.Header;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex.Converter.Response
{
    /// <summary>
    /// Converts generic Obex response to and from binary data.
    /// </summary>
    public class ObexResponseConverter : IObexResponseConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObexResponseConverter"/> class.
        /// </summary>
        protected ObexResponseConverter()
        { }

        /// <summary>
        /// Gets the instance of the <see cref="ObexResponseConverter"/> class.
        /// </summary>
        public static ObexResponseConverter Instance { get; } = new ObexResponseConverter();

        /// <summary>
        /// The number of bytes that represents the Obex response's field values.
        /// </summary>
        protected virtual int FieldBytesLength => 3;

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public ObexResponseBase FromBytes(byte[] bytes)
        {
            return FromBytesCore(bytes);
        }

        /// <summary>
        /// Constructs the Obex response's fields in binary format.
        /// </summary>
        /// <param name="response">The Obex response to get field data.</param>
        /// <returns>Binary data representing the fields in the response.</returns>
        protected virtual List<byte> GetFieldBytes(ObexResponseBase response)
        {
            return new List<byte> { (byte)response.ResponseCode, 0x00, 0x00 };
        }

        /// <summary>
        /// Core conversion logic to deserialize binary data to an appropriate Obex response object.
        /// </summary>
        /// <param name="bytes">The binary data.</param>
        /// <returns>An Obex response object.</returns>
        protected virtual ObexResponseBase FromBytesCore(byte[] bytes)
        {
            var responseCode = (ObexResponseCode)bytes[0];
            var responseLength = GetResponseLength(bytes);
            return ObexResponse.Create(responseCode,
                responseLength,
                DeserializeHeaders(bytes, responseLength));
        }

        /// <summary>
        /// Gets the response binary data length from the Obex response binary data.
        /// </summary>
        /// <param name="bytes">The binary data.</param>
        /// <returns>The defined length of the Obex response data.</returns>
        protected virtual ushort GetResponseLength(byte[] bytes)
        {
            return ObexBitConverter.ToUInt16(new byte[] { bytes[1], bytes[2] });
        }

        /// <summary>
        /// Deserializes the headers in an Obex response binary data.
        /// </summary>
        /// <param name="bytes">The binary data.</param>
        /// <param name="responseLength">The defined length of the Obex response data.</param>
        /// <returns>A collection of Obex headers.</returns>
        protected virtual IEnumerable<ObexHeader> DeserializeHeaders(byte[] bytes, ushort responseLength)
        {
            return ObexHeaderSerializer.DeserializeAll(
                new ArraySegment<byte>(bytes, FieldBytesLength, responseLength - FieldBytesLength).ToArray());
        }
    }
}
