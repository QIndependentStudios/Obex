using QIndependentStudios.Obex.Comparison;
using System;

namespace QIndependentStudios.Obex.Header
{
    /// <summary>
    /// Represents an tag-value-length triplet that can be included in an Obex header.
    /// </summary>
    public class TlvTriplet
    {
        private TlvTriplet()
        { }

        /// <summary>
        /// Gets the tag component of the triplet.
        /// </summary>
        public byte Tag { get; protected set; }

        /// <summary>
        /// Gets the value component of the triplet.
        /// </summary>
        public byte[] Value { get; protected set; }

        /// <summary>
        /// Creates a new instance of the <see cref="TlvTriplet"/> class.
        /// </summary>
        /// <param name="tag">The tag determining what the value is for.</param>
        /// <param name="value">The value of the triplet.</param>
        /// <returns></returns>
        public static TlvTriplet Create(byte tag, params byte[] value)
            => new TlvTriplet { Tag = tag, Value = value };

#pragma warning disable CS1591
        public override bool Equals(object obj)
        {
            return obj is TlvTriplet parameter
                && Tag == parameter.Tag
                && new SequenceEqualityComparer<byte>().Equals(Value, parameter.Value);
        }

        public override int GetHashCode()
        {
            var hashCode = 221537429;
            hashCode = hashCode * -1521134295 + Tag.GetHashCode();
            hashCode = hashCode * -1521134295 + new SequenceEqualityComparer<byte>().GetHashCode(Value);
            return hashCode;
        }

        public override string ToString()
        {
            var tag = BitConverter.ToString(new[] { Tag });
            return $"{tag} {BitConverter.ToString(Value)}";
        }
#pragma warning restore CS1591
    }
}
