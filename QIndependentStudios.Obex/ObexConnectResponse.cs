using QIndependentStudios.Obex.Header;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex
{
    /// <summary>
    /// Represents an Obex response used when responding to a connect request.
    /// </summary>
    public class ObexConnectResponse : ObexResponseBase
    {
        private ObexConnectResponse(ObexResponseCode responseCode,
            ushort? responseLength,
            byte obexVersionNumber,
            byte flags,
            ushort maxPacketSize,
            IEnumerable<ObexHeader> headers)
            : base(responseCode, responseLength, headers)
        {
            ObexVersion = obexVersionNumber;
            Flags = flags;
            MaxPacketSize = maxPacketSize;
        }

        /// <summary>
        /// Gets the Obex protocol version.
        /// </summary>
        public byte ObexVersion { get; }

        /// <summary>
        /// Gets the connect response flags.
        /// </summary>
        public byte Flags { get; }

        /// <summary>
        /// Gets the maximum packet size the connecting client can support from the request.
        /// </summary>
        public ushort MaxPacketSize { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="ObexConnectResponse"/>.
        /// </summary>
        /// <param name="responseCode">The response code.</param>
        /// <param name="obexVersion">The Obex protocol version. Currently, the only value is 0x10.</param>
        /// <param name="flags">The connect flags from the request. Currently not used.</param>
        /// <param name="maxPacketSize">The maximum packet size that the connecting client can support from the request.</param>
        /// <param name="headers">A collection of Obex headers.</param>
        /// <returns>The created response.</returns>
        public static ObexConnectResponse Create(ObexResponseCode responseCode,
            byte obexVersion,
            byte flags,
            ushort maxPacketSize,
            params ObexHeader[] headers)
        {
            return Create(responseCode,
                obexVersion,
                flags,
                maxPacketSize,
                headers.ToList());
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ObexConnectResponse"/>.
        /// </summary>
        /// <param name="responseCode">The response code.</param>
        /// <param name="responseLength">The response length. Provide this value when deserializing from binary data.</param>
        /// <param name="obexVersion">The Obex protocol version. Currently, the only value is 0x10.</param>
        /// <param name="flags">The connect flags from the request. Currently not used.</param>
        /// <param name="maxPacketSize">The maximum packet size that the connecting client can support from the request.</param>
        /// <param name="headers">A collection of Obex headers.</param>
        /// <returns>The created response.</returns>
        public static ObexConnectResponse Create(ObexResponseCode responseCode,
            ushort responseLength,
            byte obexVersion,
            byte flags,
            ushort maxPacketSize,
            params ObexHeader[] headers)
        {
            return Create(responseCode,
                responseLength,
                obexVersion,
                flags,
                maxPacketSize,
                headers.ToList());
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ObexConnectResponse"/>.
        /// </summary>
        /// <param name="responseCode">The response code.</param>
        /// <param name="obexVersion">The Obex protocol version. Currently, the only value is 0x10.</param>
        /// <param name="flags">The connect flags from the request. Currently not used.</param>
        /// <param name="maxPacketSize">The maximum packet size that the connecting client can support from the request.</param>
        /// <param name="headers">A collection of Obex headers.</param>
        /// <returns>The created response.</returns>
        public static ObexConnectResponse Create(ObexResponseCode responseCode,
            byte obexVersion,
            byte flags,
            ushort maxPacketSize,
            IEnumerable<ObexHeader> headers)
        {
            return new ObexConnectResponse(responseCode,
                null,
                obexVersion,
                flags,
                maxPacketSize,
                headers.ToList());
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ObexConnectResponse"/>.
        /// </summary>
        /// <param name="responseCode">The response code.</param>
        /// <param name="responseLength">The response length. Provide this value when deserializing from binary data.</param>
        /// <param name="obexVersion">The Obex protocol version. Currently, the only value is 0x10.</param>
        /// <param name="flags">The connect flags from the request. Currently not used.</param>
        /// <param name="maxPacketSize">The maximum packet size that the connecting client can support from the request.</param>
        /// <param name="headers">A collection of Obex headers.</param>
        /// <returns>The created response.</returns>
        public static ObexConnectResponse Create(ObexResponseCode responseCode,
            ushort responseLength,
            byte obexVersion,
            byte flags,
            ushort maxPacketSize,
            IEnumerable<ObexHeader> headers)
        {
            return new ObexConnectResponse(responseCode,
                responseLength,
                obexVersion,
                flags,
                maxPacketSize,
                headers.ToList());
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is ObexConnectResponse response
                && base.Equals(obj)
                && ObexVersion == response.ObexVersion
                && Flags == response.Flags
                && MaxPacketSize == response.MaxPacketSize;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hashCode = -1647789284;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + ObexVersion.GetHashCode();
            hashCode = hashCode * -1521134295 + Flags.GetHashCode();
            hashCode = hashCode * -1521134295 + MaxPacketSize.GetHashCode();
            return hashCode;
        }
    }
}
