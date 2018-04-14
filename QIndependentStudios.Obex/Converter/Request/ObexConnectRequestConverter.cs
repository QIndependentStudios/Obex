using System;
using System.Collections.Generic;

namespace QIndependentStudios.Obex.Converter.Request
{
    /// <summary>
    /// Converts Connect Obex requests to and from binary data.
    /// </summary>
    public class ObexConnectRequestConverter : ObexRequestConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObexConnectRequestConverter"/> class.
        /// </summary>
        protected ObexConnectRequestConverter()
        { }

        /// <summary>
        /// Gets the instance of the <see cref="ObexConnectRequestConverter"/> class.
        /// </summary>
        public new static ObexConnectRequestConverter Instance { get; } = new ObexConnectRequestConverter();

        /// <summary>
        /// The number of bytes that represents the Obex request's field values.
        /// </summary>
        protected override int FieldBytesLength => 7;

        /// <summary>
        /// Constructs the Obex request's fields in binary format.
        /// </summary>
        /// <param name="request">The Obex request to get field data.</param>
        /// <returns>Binary data representing the fields in the request.</returns>
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

        /// <summary>
        /// Core conversion logic to deserialize binary data to an appropriate Obex request object.
        /// </summary>
        /// <param name="bytes">The binary data.</param>
        /// <returns>An Obex request object.</returns>
        protected override ObexRequestBase FromBytesCore(byte[] bytes)
        {
            var opCode = (ObexOpCode)bytes[0];
            if (opCode != ObexOpCode.Connect)
                throw new ArgumentException($"OpCode in byte data appears to be {opCode} but expected {ObexOpCode.Connect}.");

            var requestLength = GetRequestLength(bytes);
            var obexVersion = bytes[3];
            var flags = bytes[4];
            var maxPacketSize = ObexBitConverter.ToUInt16(new[] { bytes[5], bytes[6] });

            return ObexConnectRequest.Create(requestLength,
                obexVersion,
                flags,
                maxPacketSize,
                DeserializeHeaders(bytes, requestLength));
        }
    }
}
