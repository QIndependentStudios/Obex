using QIndependentStudios.Obex.Comparison;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QIndependentStudios.Obex.Header
{
    /// <summary>
    /// Represents an Obex header with a sequence of bytes as its value.
    /// </summary>
    public class ByteSequenceObexHeader : ObexHeader
    {
        private ByteSequenceObexHeader()
        { }

        /// <summary>
        /// Gets the value of the header.
        /// </summary>
        public byte[] Value { get; protected set; }

        /// <summary>
        /// Creates a new instance of the <see cref="ByteSequenceObexHeader"/> class with a value in binary format.
        /// </summary>
        /// <param name="id">The id of the header.</param>
        /// <param name="value">The value of the header.</param>
        /// <returns>The created header.</returns>
        public static ByteSequenceObexHeader Create(ObexHeaderId id, params byte[] value)
        {
            return new ByteSequenceObexHeader { Id = id, ActualLength = (ushort)(value.Length + 3), Value = value };
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ByteSequenceObexHeader"/> class with a value in binary format.
        /// </summary>
        /// <param name="id">The id of the header.</param>
        /// <param name="value">The value of the header.</param>
        /// <returns>The created header.</returns>
        public static ByteSequenceObexHeader Create(ObexHeaderId id, IEnumerable<byte> value)
        {
            return Create(id, value?.ToArray() ?? new byte[0]);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ByteSequenceObexHeader"/> class with a text value.
        /// </summary>
        /// <param name="id">The id of the header.</param>
        /// <param name="value">The value of the header.</param>
        /// <returns>The created header.</returns>
        public static ByteSequenceObexHeader Create(ObexHeaderId id, string value, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.ASCII;

            if (encoding.Equals(Encoding.Unicode) || encoding.Equals(Encoding.BigEndianUnicode))
                throw new ArgumentException($"Use {nameof(UnicodeTextObexHeader)} for headers with unicode values.");

            if (!string.IsNullOrEmpty(value) && value.Last() != char.MinValue)
                value += char.MinValue.ToString();
            return Create(id, ObexBitConverter.GetBytes(value, encoding));
        }

        /// <summary>
        /// Converts the byte sequence value of the header into a string using the specified encoding.
        /// </summary>
        /// <param name="encoding">The encoding used to convert the byte sequence into a string.</param>
        /// <returns>The converted string.</returns>
        public string GetValueAsString(Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.ASCII;

            return ObexBitConverter.ToString(Value, encoding);
        }

#pragma warning disable CS1591
        public override bool Equals(object obj)
        {
            return obj is ByteSequenceObexHeader header
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
