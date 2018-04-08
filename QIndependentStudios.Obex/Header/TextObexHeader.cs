using System.Collections.Generic;

namespace QIndependentStudios.Obex.Header
{
    public class TextObexHeader : ObexHeader
    {
        public string Value { get; protected set; }

        public static TextObexHeader Create(ObexHeaderId id, string value)
        {
            return new TextObexHeader { Id = id, Value = value };
        }

        public static TextObexHeader Create(ObexHeaderId id, ushort headerLength, string value)
        {
            return new TextObexHeader { Id = id, ActualLength = headerLength, Value = value };
        }

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
    }
}
