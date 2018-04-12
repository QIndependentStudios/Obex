using System;
using System.Collections.Generic;

namespace QIndependentStudios.Obex.Header
{
    /// <summary>
    /// Represents an Obex header with a Guid or Uuid value.
    /// </summary>
    public class GuidObexHeader : ObexHeader
    {
        private GuidObexHeader()
        { }

        /// <summary>
        /// Gets the value of the header.
        /// </summary>
        public Guid Value { get; protected set; }

        /// <summary>
        /// Creates a new instance of the <see cref="GuidObexHeader"/> class with a Guid value.
        /// </summary>
        /// <param name="id">The id of the header.</param>
        /// <param name="value">The value of the header.</param>
        /// <returns>The created header.</returns>
        public static GuidObexHeader Create(ObexHeaderId id, Guid value)
        {
            return new GuidObexHeader { Id = id, Value = value };
        }

        /// <summary>
        /// Creates a new instance of the <see cref="GuidObexHeader"/> class with a Guid and header length.
        /// </summary>
        /// <param name="id">The id of the header.</param>
        /// <param name="headerLength">The header length. Provide this value when deserializing from binary data.</param>
        /// <param name="value">The value of the header.</param>
        /// <returns>The created header.</returns>
        public static GuidObexHeader Create(ObexHeaderId id, ushort headerLength, Guid value)
        {
            return new GuidObexHeader { Id = id, ActualLength = headerLength, Value = value };
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is GuidObexHeader header
                && base.Equals(obj)
                && Value.Equals(header.Value);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hashCode = -783812246;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(Value);
            return hashCode;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Id} - {Value}";
        }
    }
}
