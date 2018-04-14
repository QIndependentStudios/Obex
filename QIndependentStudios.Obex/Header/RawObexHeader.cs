using QIndependentStudios.Obex.Comparison;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex.Header
{
    /// <summary>
    /// Represents an Obex header with a raw binary value.
    /// </summary>
    public class RawObexHeader : ObexHeader
    {
        private RawObexHeader()
        { }

        /// <summary>
        /// Gets the value of the header.
        /// </summary>
        public byte[] Value { get; protected set; }

        /// <summary>
        /// Creates a new instance of the <see cref="RawObexHeader"/> class with a value in binary format.
        /// </summary>
        /// <param name="id">The id of the header.</param>
        /// <param name="value">The value of the header.</param>
        /// <returns>The created header.</returns>
        public static RawObexHeader Create(ObexHeaderId id, params byte[] value)
        {
            return new RawObexHeader { Id = id, ActualLength = (ushort)(value.Length + 3), Value = value };
        }

        /// <summary>
        /// Creates a new instance of the <see cref="RawObexHeader"/> class with a value in binary format.
        /// </summary>
        /// <param name="id">The id of the header.</param>
        /// <param name="value">The value of the header.</param>
        /// <returns>The created header.</returns>
        public static RawObexHeader Create(ObexHeaderId id, IEnumerable<byte> value)
        {
            return Create(id, value?.ToArray() ?? new byte[0]);
        }

#pragma warning disable CS1591
        public override bool Equals(object obj)
        {
            return obj is RawObexHeader header
                && base.Equals(obj)
                && new SequenceEqualityComparer<byte>().Equals(Value, header.Value);
        }

        public override int GetHashCode()
        {
            var hashCode = -783812246;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + new SequenceEqualityComparer<byte>().GetHashCode(Value);
            return hashCode;
        }

        public override string ToString()
        {
            return $"{Id} - {Value}";
        }
#pragma warning restore CS1591
    }
}
