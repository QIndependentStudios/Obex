using QIndependentStudios.Obex.Comparison;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex.Header
{
    public class RawObexHeader : ObexHeader
    {
        public byte[] Value { get; protected set; }

        public static RawObexHeader Create(ObexHeaderId id, params byte[] value)
        {
            return new RawObexHeader { Id = id, ActualLength = (ushort)(value.Length + 3), Value = value };
        }

        public static RawObexHeader Create(ObexHeaderId id, IEnumerable<byte> value)
        {
            return Create(id, value?.ToArray() ?? new byte[0]);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is RawObexHeader header
                && base.Equals(obj)
                && new SequenceEqualityComparer<byte>().Equals(Value, header.Value);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hashCode = -783812246;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + new SequenceEqualityComparer<byte>().GetHashCode(Value);
            return hashCode;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Id} - {Value}";
        }
    }
}
