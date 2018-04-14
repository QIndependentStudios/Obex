using QIndependentStudios.Obex.Comparison;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex.Header
{
    /// <summary>
    /// Represents an Obex header that has a collection of tag-value-length triplets.
    /// </summary>
    public class TlvCollectionObexHeader : ObexHeader
    {
        private readonly IEnumerable<TlvTriplet> _values;

        private TlvCollectionObexHeader(ObexHeaderId id,
            ushort? headerLength,
            IEnumerable<TlvTriplet> values)
        {
            Id = id;
            ActualLength = headerLength;
            _values = values ?? new List<TlvTriplet>();
        }

        /// <summary>
        /// Gets the tag-length-value triplets in the header.
        /// </summary>
        public IEnumerable<TlvTriplet> Tlvs => _values.ToList().AsReadOnly();

        /// <summary>
        /// Creates a new instance of the <see cref="TlvCollectionObexHeader"/> class with tag-length-value triplets.
        /// </summary>
        /// <param name="id">The id of the header.</param>
        /// <param name="values">The tag-length-value triplets for the header.</param>
        /// <returns>The created header.</returns>
        public static TlvCollectionObexHeader Create(ObexHeaderId id, params TlvTriplet[] values)
            => new TlvCollectionObexHeader(id, null, values);

        /// <summary>
        /// Creates a new instance of the <see cref="TlvCollectionObexHeader"/> class with tag-length-value triplets.
        /// </summary>
        /// <param name="id">The id of the header.</param>
        /// <param name="values">The tag-length-value triplets for the header.</param>
        /// <returns>The created header.</returns>
        public static TlvCollectionObexHeader Create(ObexHeaderId id, IEnumerable<TlvTriplet> values)
            => new TlvCollectionObexHeader(id, null, values);

        /// <summary>
        /// Creates a new instance of the <see cref="TlvCollectionObexHeader"/> class with tag-length-value triplets and header length.
        /// </summary>
        /// <param name="id">The id of the header.</param>
        /// <param name="headerLength">The header length. Provide this value when deserializing from binary data.</param>
        /// <param name="values">The tag-length-value triplets for the header.</param>
        /// <returns>The created header.</returns>
        public static TlvCollectionObexHeader Create(ObexHeaderId id, ushort headerLength, params TlvTriplet[] values)
            => new TlvCollectionObexHeader(id, headerLength, values);


        /// <summary>
        /// Creates a new instance of the <see cref="TlvCollectionObexHeader"/> class with tag-length-value triplets and header length.
        /// </summary>
        /// <param name="id">The id of the header.</param>
        /// <param name="headerLength">The header length. Provide this value when deserializing from binary data.</param>
        /// <param name="values">The tag-length-value triplets for the header.</param>
        /// <returns>The created header.</returns>
        public static TlvCollectionObexHeader Create(ObexHeaderId id, ushort headerLength, IEnumerable<TlvTriplet> values)
            => new TlvCollectionObexHeader(id, headerLength, values);

#pragma warning disable CS1591
        public override bool Equals(object obj)
        {
            return obj is TlvCollectionObexHeader header
                && base.Equals(obj)
                && new SequenceEqualityComparer<TlvTriplet>().Equals(_values, header._values);
        }

        public override int GetHashCode()
        {
            var hashCode = -1494980905;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + new SequenceEqualityComparer<TlvTriplet>().GetHashCode(_values);
            return hashCode;
        }

        public override string ToString()
        {
            return $"{Id} - {_values.Count()} TLV triplet(s)";
        }
#pragma warning restore CS1591
    }
}
