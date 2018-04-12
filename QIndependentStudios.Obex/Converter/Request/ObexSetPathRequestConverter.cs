using QIndependentStudios.Obex.Path;
using System;
using System.Collections.Generic;

namespace QIndependentStudios.Obex.Converter.Request
{
    /// <summary>
    /// Converts Set Path Obex requests to and from binary data.
    /// </summary>
    public class ObexSetPathRequestConverter : ObexRequestConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObexSetPathRequestConverter"/> class.
        /// </summary>
        protected ObexSetPathRequestConverter()
        { }

        /// <summary>
        /// Gets the instance of the <see cref="ObexSetPathRequestConverter"/> class.
        /// </summary>
        public new static ObexSetPathRequestConverter Instance { get; } = new ObexSetPathRequestConverter();

        /// <summary>
        /// The number of bytes that represents the Obex request's field values.
        /// </summary>
        protected override int FieldBytesLength => 5;


        /// <summary>
        /// Constructs the Obex request's fields in binary format.
        /// </summary>
        /// <param name="request">The Obex request to get field data.</param>
        /// <returns>Binary data representing the fields in the request.</returns>
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

        /// <summary>
        /// Core conversion logic to deserialize binary data to an appropriate Obex request object.
        /// </summary>
        /// <param name="bytes">The binary data.</param>
        /// <returns>An Obex request object.</returns>
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
