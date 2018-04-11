using System.Collections.Generic;

namespace QIndependentStudios.Obex.Header
{
    /// <summary>
    /// Represents an Obex header with a text value that will be encoded as null-terminating Unicode.
    /// </summary>
    public class UnicodeTextObexHeader : ObexHeader
    {
        private UnicodeTextObexHeader()
        { }

        /// <summary>
        /// Gets the value of the header.
        /// </summary>
        public string Value { get; protected set; }

        /// <summary>
        /// Creates a new instance of the <see cref="UnicodeTextObexHeader"/> class with a text value.
        /// </summary>
        /// <param name="id">The id of the header.</param>
        /// <param name="value">The value of the header.</param>
        /// <returns>The created header.</returns>
        public static UnicodeTextObexHeader Create(ObexHeaderId id, string value)
        {
            return new UnicodeTextObexHeader { Id = id, Value = value };
        }

        /// <summary>
        /// Creates a new instance of the <see cref="UnicodeTextObexHeader"/> class with a text value.
        /// </summary>
        /// <param name="id">The id of the header.</param>
        /// <param name="value">The value of the header.</param>
        /// <param name="headerLength">The header length. provide this value when deserializing from binary data.</param>
        /// <returns>The created header.</returns>
        public static UnicodeTextObexHeader Create(ObexHeaderId id, ushort headerLength, string value)
        {
            return new UnicodeTextObexHeader { Id = id, ActualLength = headerLength, Value = value };
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is UnicodeTextObexHeader header
                && base.Equals(obj)
                && Value == header.Value;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hashCode = -783812246;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Value);
            return hashCode;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Id} - {Value}";
        }
    }
}
