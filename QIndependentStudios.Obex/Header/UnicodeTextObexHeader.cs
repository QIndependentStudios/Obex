using System.Collections.Generic;

namespace QIndependentStudios.Obex.Header
{
    public class UnicodeTextObexHeader : ObexHeader
    {
        public string Value { get; protected set; }

        public static UnicodeTextObexHeader Create(ObexHeaderId id, string value)
        {
            return new UnicodeTextObexHeader { Id = id, Value = value };
        }

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
