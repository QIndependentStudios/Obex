using System;
using System.Collections.Generic;

namespace QIndependentStudios.Obex.Header
{
    public class GuidObexHeader : ObexHeader
    {
        public Guid Value { get; protected set; }

        public static GuidObexHeader Create(ObexHeaderId id, Guid value, ushort? headerLength = null)
        {
            return new GuidObexHeader { Id = id, ActualLength = headerLength, Value = value };
        }

        public override bool Equals(object obj)
        {
            return obj is GuidObexHeader header
                && base.Equals(obj)
                && Value.Equals(header.Value);
        }

        public override int GetHashCode()
        {
            var hashCode = -783812246;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(Value);
            return hashCode;
        }

        public override string ToString()
        {
            return $"{Id} - {Value}";
        }
    }
}
