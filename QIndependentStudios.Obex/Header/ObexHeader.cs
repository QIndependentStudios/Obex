using System.Collections.Generic;

namespace QIndependentStudios.Obex.Header
{
    /// <summary>
    /// Represents an Obex header.
    /// </summary>
    public class ObexHeader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObexHeader"/> class.
        /// </summary>
        protected ObexHeader()
        { }

        /// <summary>
        /// Gets the Id determining what kind of header it is.
        /// </summary>
        public ObexHeaderId Id { get; protected set; }

        /// <summary>
        /// If available, gets the total length of the binary data of the the header.
        /// Usually set if the header was created by deserialization or if the header has a fixed length.
        /// </summary>
        public ushort? ActualLength { get; protected set; }

#pragma warning disable CS1591
        public override bool Equals(object obj)
        {
            return obj is ObexHeader header
                && Id == header.Id
                && EqualityComparer<ushort?>.Default.Equals(ActualLength, header.ActualLength);
        }

        public override int GetHashCode()
        {
            var hashCode = 831783735;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<ushort?>.Default.GetHashCode(ActualLength);
            return hashCode;
        }
#pragma warning restore CS1591
    }
}
