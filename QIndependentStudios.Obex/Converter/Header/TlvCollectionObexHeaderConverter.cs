using QIndependentStudios.Obex.Header;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex.Converter.Header
{
    /// <summary>
    /// Converts Obex Application Parameter headers to and from binary data.
    /// </summary>
    public class TlvCollectionObexHeaderConverter : RawObexHeaderConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TlvCollectionObexHeaderConverter"/> class.
        /// </summary>
        protected TlvCollectionObexHeaderConverter()
        { }

        /// <summary>
        /// Gets the instance of the <see cref="TlvCollectionObexHeaderConverter"/> class.
        /// </summary>
        public new static TlvCollectionObexHeaderConverter Instance { get; } = new TlvCollectionObexHeaderConverter();

        /// <summary>
        /// Converts binary data to an Obex header object.
        /// </summary>
        /// <param name="bytes">The binary data to deserialize.</param>
        /// <returns>The deserialized Obex header.</returns>
        public override ObexHeader FromBytes(byte[] bytes)
        {
            var parametersData = ExtractValueBytes(bytes);
            var parameters = GetApplicationParameters(parametersData);

            return TlvCollectionObexHeader.Create((ObexHeaderId)bytes[0],
                GetHeaderSize(bytes), parameters);
        }

        /// <summary>
        /// Converts the value of the Obex header to binary data.
        /// </summary>
        /// <param name="header">The header whose value will be converted.</param>
        /// <returns>The binary data of the header's value.</returns>
        protected override byte[] ValueToBytes(ObexHeader header)
        {
            if (header is TlvCollectionObexHeader appParamHeader)
                return GetBytes(appParamHeader.Tlvs);

            return new byte[0];
        }

        private static IEnumerable<TlvTriplet> GetApplicationParameters(byte[] bytes)
        {
            var parameters = new List<TlvTriplet>();
            var offset = 0;

            while (offset < bytes.Length)
            {
                var tag = bytes[offset];
                offset++;
                var length = (ushort)bytes[offset];
                offset++;
                var value = new ArraySegment<byte>(bytes, offset, length);
                offset += length;

                parameters.Add(TlvTriplet.Create(tag, value.ToArray()));
            }

            return parameters;
        }

        private static byte[] GetBytes(IEnumerable<TlvTriplet> appParameters)
        {
            var bytes = new List<byte>();
            foreach (var param in appParameters)
            {
                bytes.Add(param.Tag);
                bytes.Add((byte)param.Value.Length);
                bytes.AddRange(param.Value);
            }

            return bytes.ToArray();
        }
    }
}
