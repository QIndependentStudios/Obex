using QIndependentStudios.Obex.Comparison;
using System;

namespace QIndependentStudios.Obex.Header
{
    public class ObexAppParameter
    {
        protected ObexAppParameter(byte tag, byte[] value)
        {
            Tag = tag;
            Value = value;
        }

        public byte Tag { get; }
        public byte[] Value { get; }

        public static ObexAppParameter Create(byte tag, params byte[] value)
            => new ObexAppParameter(tag, value);

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is ObexAppParameter parameter
                && Tag == parameter.Tag
                && new SequenceEqualityComparer<byte>().Equals(Value, parameter.Value);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hashCode = 221537429;
            hashCode = hashCode * -1521134295 + Tag.GetHashCode();
            hashCode = hashCode * -1521134295 + new SequenceEqualityComparer<byte>().GetHashCode(Value);
            return hashCode;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            var tag = BitConverter.ToString(new byte[] { Tag });
            return $"{tag} {BitConverter.ToString(Value)}";
        }
    }
}
