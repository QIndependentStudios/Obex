using System.Collections.Generic;

namespace QIndependentStudios.Obex.Header
{
    public class UInt32ObexHeader : ObexHeader
    {
        public override ushort? ActualLength => 5;
        public uint Value { get; protected set; }

        public static UInt32ObexHeader Create(ObexHeaderId id, uint value)
        {
            return new UInt32ObexHeader { Id = id, Value = value };
        }

        public override bool Equals(object obj)
        {
            return obj is UInt32ObexHeader header
                && base.Equals(obj)
                && EqualityComparer<ushort?>.Default.Equals(ActualLength, header.ActualLength)
                && Value == header.Value;
        }

        public override int GetHashCode()
        {
            var hashCode = -2059405511;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<ushort?>.Default.GetHashCode(ActualLength);
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"{Id} - {Value}";
        }
    }
}
