using System.Collections.Generic;

namespace QIndependentStudios.Obex.Header
{
    /// <summary>
    /// Represents an Obex header that with a single unsigned integer value and no length.
    /// </summary>
    public class UInt32ObexHeader : ObexHeader
    {
        private UInt32ObexHeader()
        { }

        /// <inheritdoc/>
        public override ushort? ActualLength => 5;

        /// <summary>
        /// Gets the value of the header.
        /// </summary>
        public uint Value { get; protected set; }

        /// <summary>
        /// Creates a new instance of the <see cref="UInt32ObexHeader"/> class with an unsigned integer value.
        /// </summary>
        /// <param name="id">The id of the header.</param>
        /// <param name="value">The value of the header.</param>
        /// <returns>The created header.</returns>
        public static UInt32ObexHeader Create(ObexHeaderId id, uint value)
        {
            return new UInt32ObexHeader { Id = id, Value = value };
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is UInt32ObexHeader header
                && base.Equals(obj)
                && EqualityComparer<ushort?>.Default.Equals(ActualLength, header.ActualLength)
                && Value == header.Value;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hashCode = -2059405511;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<ushort?>.Default.GetHashCode(ActualLength);
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            return hashCode;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Id} - {Value}";
        }
    }
}
