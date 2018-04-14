using QIndependentStudios.Obex.Header;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex
{
    /// <summary>
    /// Represents an Obex request used specifically when making a connection.
    /// </summary>
    public class ObexConnectRequest : ObexRequestBase
    {
        /// <summary>
        /// The minimum Max Packet Size.
        /// </summary>
        public const ushort MaxPacketSizeLowerBound = 255;

        /// <summary>
        /// The maximum Max Packet Size.
        /// </summary>
        public const ushort MaxPacketSizeUpperBound = 65534;

        private ObexConnectRequest(ushort? requestLength,
            ushort maxPacketSize,
            IEnumerable<ObexHeader> headers)
            : this(requestLength, 0x10, 0x00, maxPacketSize, headers)
        {
            // Currently the only Obex version is 0x10.
            // Flags are currently not used, using 0x00 as default.
        }

        private ObexConnectRequest(ushort? requestLength,
            byte obexVersion,
            byte flags,
            ushort maxPacketSize,
            IEnumerable<ObexHeader> headers)
            : base(ObexOpCode.Connect, requestLength, headers)
        {
            if (maxPacketSize < MaxPacketSizeLowerBound)
                throw new ArgumentException($"MaxPacketSize cannot be less than {MaxPacketSizeLowerBound}");
            if (maxPacketSize > MaxPacketSizeUpperBound)
                throw new ArgumentException($"MaxPacketSize cannot be greater than {MaxPacketSizeUpperBound}");

            ObexVersion = obexVersion;
            Flags = flags;
            MaxPacketSize = maxPacketSize;
        }

        /// <summary>
        /// Gets the Obex protocol version.
        /// </summary>
        public byte ObexVersion { get; }

        /// <summary>
        /// Gets the connect request flags.
        /// </summary>
        public byte Flags { get; }

        /// <summary>
        /// Gets the maximum packet size the connecting client can support.
        /// </summary>
        public ushort MaxPacketSize { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="ObexConnectRequest"/>.
        /// </summary>
        /// <param name="maxPacketSize">The maximum packet size that the connecting client can support.</param>
        /// <param name="headers">A collection of Obex headers.</param>
        /// <returns>The created request.</returns>
        public static ObexConnectRequest Create(ushort maxPacketSize, params ObexHeader[] headers)
        {
            return new ObexConnectRequest(null, maxPacketSize, headers);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ObexConnectRequest"/>.
        /// </summary>
        /// <param name="requestLength">The request length. Provide this value when deserializing from binary data.</param>
        /// <param name="maxPacketSize">The maximum packet size that the connecting client can support.</param>
        /// <param name="headers">A collection of Obex headers.</param>
        /// <returns>The created request.</returns>
        public static ObexConnectRequest Create(ushort requestLength,
            ushort maxPacketSize,
            params ObexHeader[] headers)
        {
            return Create(requestLength, maxPacketSize, headers.ToList());
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ObexConnectRequest"/> with explicit version number and flags.
        /// </summary>
        /// <param name="obexVersion">The Obex protocol version. Currently, the only value is 0x10.</param>
        /// <param name="flags">The connect request flags. Currently not used.</param>
        /// <param name="maxPacketSize">The maximum packet size that the connecting client can support.</param>
        /// <param name="headers">A collection of Obex headers.</param>
        /// <returns>The created request.</returns>
        public static ObexConnectRequest Create(byte obexVersion,
            byte flags,
            ushort maxPacketSize,
            params ObexHeader[] headers)
        {
            return new ObexConnectRequest(null, obexVersion, flags, maxPacketSize, headers);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ObexConnectRequest"/> with explicit version number and flags.
        /// </summary>
        /// <param name="requestLength">The request length. Provide this value when deserializing from binary data.</param>
        /// <param name="obexVersion">The Obex protocol version. Currently, the only value is 0x10.</param>
        /// <param name="flags">The connect request flags. Currently not used.</param>
        /// <param name="maxPacketSize">The maximum packet size that the connecting client can support.</param>
        /// <param name="headers">A collection of Obex headers.</param>
        /// <returns>The created request.</returns>
        public static ObexConnectRequest Create(ushort requestLength,
            byte obexVersion,
            byte flags,
            ushort maxPacketSize,
            params ObexHeader[] headers)
        {
            return new ObexConnectRequest(requestLength, obexVersion, flags, maxPacketSize, headers);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ObexConnectRequest"/>.
        /// </summary>
        /// <param name="maxPacketSize">The maximum packet size that the connecting client can support.</param>
        /// <param name="headers">A collection of Obex headers.</param>
        /// <returns>The created request.</returns>
        public static ObexConnectRequest Create(ushort maxPacketSize, IEnumerable<ObexHeader> headers)
        {
            return new ObexConnectRequest(null, maxPacketSize, headers);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ObexConnectRequest"/>.
        /// </summary>
        /// <param name="requestLength">The request length. Provide this value when deserializing from binary data.</param>
        /// <param name="maxPacketSize">The maximum packet size that the connecting client can support.</param>
        /// <param name="headers">A collection of Obex headers.</param>
        /// <returns>The created request.</returns>
        public static ObexConnectRequest Create(ushort requestLength,
            ushort maxPacketSize,
            IEnumerable<ObexHeader> headers)
        {
            return new ObexConnectRequest(requestLength, 0x10, 0x00, maxPacketSize, headers);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ObexConnectRequest"/> with explicit version number and flags.
        /// </summary>
        /// <param name="obexVersion">The Obex protocol version. Currently, the only value is 0x10.</param>
        /// <param name="flags">The connect request flags. Currently not used.</param>
        /// <param name="maxPacketSize">The maximum packet size that the connecting client can support.</param>
        /// <param name="headers">A collection of Obex headers.</param>
        /// <returns>The created request.</returns>
        public static ObexConnectRequest Create(byte obexVersion,
            byte flags,
            ushort maxPacketSize,
            IEnumerable<ObexHeader> headers)
        {
            return new ObexConnectRequest(null, obexVersion, flags, maxPacketSize, headers);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ObexConnectRequest"/> with explicit version number and flags.
        /// </summary>
        /// <param name="requestLength">The request length. Provide this value when deserializing from binary data.</param>
        /// <param name="obexVersion">The Obex protocol version. Currently, the only value is 0x10.</param>
        /// <param name="flags">The connect request flags. Currently not used.</param>
        /// <param name="maxPacketSize">The maximum packet size that the connecting client can support.</param>
        /// <param name="headers">A collection of Obex headers.</param>
        /// <returns>The created request.</returns>
        public static ObexConnectRequest Create(ushort requestLength,
            byte obexVersion,
            byte flags,
            ushort maxPacketSize,
            IEnumerable<ObexHeader> headers)
        {
            return new ObexConnectRequest(requestLength, obexVersion, flags, maxPacketSize, headers);
        }

#pragma warning disable CS1591
        public override bool Equals(object obj)
        {
            return obj is ObexConnectRequest request
                && base.Equals(obj)
                && ObexVersion == request.ObexVersion
                && Flags == request.Flags
                && MaxPacketSize == request.MaxPacketSize;
        }

        public override int GetHashCode()
        {
            var hashCode = -1647789284;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + ObexVersion.GetHashCode();
            hashCode = hashCode * -1521134295 + Flags.GetHashCode();
            hashCode = hashCode * -1521134295 + MaxPacketSize.GetHashCode();
            return hashCode;
        }
#pragma warning restore CS1591
    }
}
