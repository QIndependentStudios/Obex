using System;

namespace QIndependentStudios.Obex.Header
{
    /// <summary>
    /// Represents an Obex header with a DateTime value.
    /// </summary>
    public class DateTimeObexHeader : ObexHeader
    {
        private DateTimeObexHeader(ObexHeaderId id, ushort? headerLength, DateTime value)
        {
            if (id != ObexHeaderId.Time && id != ObexHeaderId.Time4Byte)
                throw new ArgumentException($"Only {ObexHeaderId.Time} and {ObexHeaderId.Time4Byte} Obex header ids are supported.");

            Id = id;
            ActualLength = headerLength;
            Value = value;
        }

        /// <summary>
        /// Gets the value of the header.
        /// </summary>
        public DateTime Value { get; protected set; }

        /// <summary>
        /// Creates a new instance of the <see cref="DateTimeObexHeader"/> class with a DateTime value.
        /// </summary>
        /// <param name="id">The id of the header.</param>
        /// <param name="value">The value of the header.</param>
        /// <returns>The created header.</returns>
        public static DateTimeObexHeader Create(ObexHeaderId id, DateTime value)
        {
            return new DateTimeObexHeader(id, null, value);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DateTimeObexHeader"/> class with a DateTime value and header length.
        /// </summary>
        /// <param name="id">The id of the header.</param>
        /// <param name="headerLength">The header length. Provide this value when deserializing from binary data.</param>
        /// <param name="value">The value of the header.</param>
        /// <returns>The created header.</returns>
        public static DateTimeObexHeader Create(ObexHeaderId id, ushort headerLength, DateTime value)
        {
            return new DateTimeObexHeader(id, headerLength, value);
        }

#pragma warning disable CS1591
        public override bool Equals(object obj)
        {
            return obj is DateTimeObexHeader header
                && base.Equals(obj)
                && Value == header.Value;
        }

        public override int GetHashCode()
        {
            var hashCode = -783812246;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"{Id} - {Value}";
        }
#pragma warning restore CS1591
    }
}
