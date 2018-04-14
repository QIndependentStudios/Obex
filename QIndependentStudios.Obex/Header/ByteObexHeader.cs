using System.Collections.Generic;

namespace QIndependentStudios.Obex.Header
{
    /// <summary>
    /// Represents an Obex header with a value of a single byte and no length.
    /// </summary>
    public class ByteObexHeader : ObexHeader
    {
        private ByteObexHeader()
        {
            ActualLength = 2;
        }

        /// <summary>
        /// Gets the value of the header.
        /// </summary>
        public byte Value { get; protected set; }

        /// <summary>
        /// Creates a new instance of the <see cref="ByteObexHeader"/> class with a single byte value.
        /// </summary>
        /// <param name="id">The id of the header.</param>
        /// <param name="value">The value of the header.</param>
        /// <returns>The created header.</returns>
        public static ByteObexHeader Create(ObexHeaderId id, byte value)
        {
            return new ByteObexHeader { Id = id, Value = value };
        }

#pragma warning disable CS1591
        public override bool Equals(object obj)
        {
            return obj is ByteObexHeader header
                && base.Equals(obj)
                && EqualityComparer<ushort?>.Default.Equals(ActualLength, header.ActualLength)
                && Value == header.Value;
        }

        public override int GetHashCode()
        {
            var hashCode = -2059405511;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<ushort?>.Default.GetHashCode(ActualLength);
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
