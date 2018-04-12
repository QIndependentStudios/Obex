using QIndependentStudios.Obex.Comparison;
using System;

namespace QIndependentStudios.Obex.Header
{
    /// <summary>
    /// Represents an Obex application parameter that can be included in an Obex application parameter header.
    /// </summary>
    public class ObexAppParameter
    {
        private ObexAppParameter()
        { }

        /// <summary>
        /// Gets the tag determining what kind of parameter it is.
        /// </summary>
        public byte Tag { get; protected set; }

        /// <summary>
        /// Gets the value of the parameter.
        /// </summary>
        public byte[] Value { get; protected set; }

        /// <summary>
        /// Creates a new instance of the <see cref="ObexAppParameter"/> class.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ObexAppParameter Create(byte tag, params byte[] value)
            => new ObexAppParameter { Tag = tag, Value = value };

#pragma warning disable CS1591
        public override bool Equals(object obj)
        {
            return obj is ObexAppParameter parameter
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
            var tag = BitConverter.ToString(new byte[] { Tag });
            return $"{tag} {BitConverter.ToString(Value)}";
        }
#pragma warning restore CS1591
    }
}
