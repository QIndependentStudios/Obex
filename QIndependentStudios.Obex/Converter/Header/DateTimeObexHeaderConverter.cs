using QIndependentStudios.Obex.Header;
using System;

namespace QIndependentStudios.Obex.Converter.Header
{
    /// <summary>
    /// Converts Guid value Obex headers to and from binary data.
    /// </summary>
    public class DateTimeObexHeaderConverter : ObexHeaderConverterBase
    {
        private static readonly DateTime Time4ByteReferenceDateTime = new DateTime(1970, 1, 1);

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeObexHeaderConverter"/> class.
        /// </summary>
        protected DateTimeObexHeaderConverter()
        { }

        /// <summary>
        /// Gets the instance of the <see cref="DateTimeObexHeaderConverter"/> class.
        /// </summary>
        public static DateTimeObexHeaderConverter Instance { get; } = new DateTimeObexHeaderConverter();

        /// <summary>
        /// Converts binary data to an Obex header object.
        /// </summary>
        /// <param name="bytes">The binary data to deserialize.</param>
        /// <returns>The deserialized Obex header.</returns>
        public override ObexHeader FromBytes(byte[] bytes)
        {
            var id = (ObexHeaderId)bytes[0];
            return DateTimeObexHeader.Create(id,
                GetHeaderSize(bytes),
                ConvertToDateTime(id, ExtractValueBytes(bytes)));
        }

        /// <summary>
        /// Converts the value of the Obex header to binary data.
        /// </summary>
        /// <param name="header">The header whose value will be converted.</param>
        /// <returns>The binary data of the header's value.</returns>
        protected override byte[] ValueToBytes(ObexHeader header)
        {
            if (!(header is DateTimeObexHeader dateTimeHeader))
                return new byte[0];

            if (dateTimeHeader.Id == ObexHeaderId.Time4Byte)
                return ObexBitConverter.GetBytes((UInt32)(dateTimeHeader.Value - Time4ByteReferenceDateTime).TotalSeconds);

            return ObexBitConverter.GetBytes(dateTimeHeader.Value);

        }

        private static DateTime ConvertToDateTime(ObexHeaderId id, byte[] bytes)
        {
            if (id == ObexHeaderId.Time)
                return ObexBitConverter.ToDateTime(bytes);
            if (id == ObexHeaderId.Time4Byte)
            {
                if (bytes.Length != 4)
                    throw new ArgumentException($"Headers with id {ObexHeaderId.Time4Byte} must have a value that's exactly 4 bytes.");

                var secondsOffset = ObexBitConverter.ToUInt32(bytes);
                return Time4ByteReferenceDateTime.AddSeconds(secondsOffset);
            }

            throw new ArgumentException($"{nameof(DateTimeObexHeaderConverter)} only supports " +
                $"{ObexHeaderId.Time} and {ObexHeaderId.Time4Byte} Obex header ids.");
        }
    }
}
