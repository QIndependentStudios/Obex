using System.Collections.Generic;

namespace QIndependentStudios.Obex.Header
{
    /// <summary>
    /// Represents an Obex header with a text value that will be encoded as null-terminating Utf-8.
    /// </summary>
    public class TextObexHeader : ObexHeader
    {
        private TextObexHeader()
        { }

        /// <summary>
        /// Gets the value of the header.
        /// </summary>
        public string Value { get; protected set; }

        /// <summary>
        /// Creates a new instance of the <see cref="TextObexHeader"/> class with a text value.
        /// </summary>
        /// <param name="id">The id of the header.</param>
        /// <param name="value">The value of the header.</param>
        /// <returns>The created header.</returns>
        public static TextObexHeader Create(ObexHeaderId id, string value)
        {
            return new TextObexHeader { Id = id, Value = value };
        }

        /// <summary>
        /// Creates a new instance of the <see cref="TextObexHeader"/> class with a text value.
        /// </summary>
        /// <param name="id">The id of the header.</param>
        /// <param name="headerLength">The header length. Provide this value when deserializing from binary data.</param>
        /// <param name="value">The value of the header.</param>
        /// <returns>The created header.</returns>
        public static TextObexHeader Create(ObexHeaderId id, ushort headerLength, string value)
        {
            return new TextObexHeader { Id = id, ActualLength = headerLength, Value = value };
        }

#pragma warning disable CS1591
        public override bool Equals(object obj)
        {
            return obj is TextObexHeader header
                && base.Equals(obj)
                && Value == header.Value;
        }

        public override int GetHashCode()
        {
            var hashCode = -783812246;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Value);
            return hashCode;
        }

        public override string ToString()
        {
            return $"{Id} - {Value}";
        }
#pragma warning restore CS1591
    }
}
