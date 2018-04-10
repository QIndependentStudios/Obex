using System.Collections.Generic;

namespace QIndependentStudios.Obex.Header
{
    public class ObexHeader
    {
        public ObexHeaderId Id { get; protected set; }
        public virtual ushort? ActualLength { get; protected set; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is ObexHeader header
                && Id == header.Id
                && EqualityComparer<ushort?>.Default.Equals(ActualLength, header.ActualLength);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hashCode = 831783735;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<ushort?>.Default.GetHashCode(ActualLength);
            return hashCode;
        }
    }
}
