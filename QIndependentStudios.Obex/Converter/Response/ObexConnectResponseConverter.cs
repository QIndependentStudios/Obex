using System.Collections.Generic;

namespace QIndependentStudios.Obex.Converter.Response
{
    /// <summary>
    /// Converts Connect Obex response to and from binary data.
    /// </summary>
    public class ObexConnectResponseConverter : ObexResponseConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObexConnectResponseConverter"/> class.
        /// </summary>
        protected ObexConnectResponseConverter()
        { }

        /// <summary>
        /// Gets the instance of the <see cref="ObexResponseConverter"/> class.
        /// </summary>
        public new static ObexConnectResponseConverter Instance { get; } = new ObexConnectResponseConverter();

        /// <summary>
        /// The number of bytes that represents the Obex response's field values.
        /// </summary>
        protected override int FieldBytesLength => 7;

        /// <summary>
        /// Constructs the Obex response's fields in binary format.
        /// </summary>
        /// <param name="response">The Obex response to get field data.</param>
        /// <returns>Binary data representing the fields in the response.</returns>
        protected override List<byte> GetFieldBytes(ObexResponseBase response)
        {
            var connectResponse = (ObexConnectResponse)response;
            var bytes = new List<byte>
            {
                (byte)connectResponse.ResponseCode,
                0x00,
                0x00,
                connectResponse.ObexVersion,
                connectResponse.Flags
            };

            bytes.AddRange(ObexBitConverter.GetBytes(connectResponse.MaxPacketSize));

            return bytes;
        }

        /// <summary>
        /// Core conversion logic to deserialize binary data to an appropriate Obex response object.
        /// </summary>
        /// <param name="bytes">The binary data.</param>
        /// <returns>An Obex response object.</returns>
        protected override ObexResponseBase FromBytesCore(byte[] bytes)
        {
            var responseCode = (ObexResponseCode)bytes[0];
            var responseLength = GetResponseLength(bytes);
            var obexVersion = bytes[3];
            var flags = bytes[4];
            var maxPacketSize = ObexBitConverter.ToUInt16(new[] { bytes[5], bytes[6] });

            return ObexConnectResponse.Create(responseCode,
                responseLength,
                obexVersion,
                flags,
                maxPacketSize,
                DeserializeHeaders(bytes, responseLength));
        }
    }
}
