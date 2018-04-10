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

        /// <inheritdoc/>
        protected override int FieldBytesLength => 7;

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        protected override ObexResponseBase FromBytesCore(byte[] bytes)
        {
            var responseCode = (ObexResponseCode)bytes[0];
            var responseLength = GetResponseLength(bytes);
            var obexVersion = bytes[3];
            var flags = bytes[4];
            var maxPacketSize = ObexBitConverter.ToUInt16(new byte[] { bytes[5], bytes[6] });

            return ObexConnectResponse.Create(responseCode,
                responseLength,
                obexVersion,
                flags,
                maxPacketSize,
                DeserializeHeaders(bytes, responseLength));
        }
    }
}
