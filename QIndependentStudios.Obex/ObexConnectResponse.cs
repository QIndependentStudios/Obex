using QIndependentStudios.Obex.Header;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex
{
    public class ObexConnectResponse : ObexResponseBase
    {
        protected ObexConnectResponse(ObexResponseCode responseCode,
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

        public byte ObexVersion { get; }
        public byte Flags { get; }
        public ushort MaxPacketSize { get; }

        public static ObexConnectResponse Create(ObexResponseCode responseCode,
            byte obexVersionNumber,
            byte flags,
            ushort maxPacketSize,
            params ObexHeader[] headers)
        {
            return Create(responseCode,
                obexVersionNumber,
                flags,
                maxPacketSize,
                headers.ToList());
        }

        public static ObexConnectResponse Create(ObexResponseCode responseCode,
            ushort responseLength,
            byte obexVersionNumber,
            byte flags,
            ushort maxPacketSize,
            params ObexHeader[] headers)
        {
            return Create(responseCode,
                responseLength,
                obexVersionNumber,
                flags,
                maxPacketSize,
                headers.ToList());
        }

        public static ObexConnectResponse Create(ObexResponseCode responseCode,
            byte obexVersionNumber,
            byte flags,
            ushort maxPacketSize,
            IEnumerable<ObexHeader> headers)
        {
            return new ObexConnectResponse(responseCode,
                null,
                obexVersionNumber,
                flags,
                maxPacketSize,
                headers.ToList());
        }

        public static ObexConnectResponse Create(ObexResponseCode responseCode,
            ushort responseLength,
            byte obexVersionNumber,
            byte flags,
            ushort maxPacketSize,
            IEnumerable<ObexHeader> headers)
        {
            return new ObexConnectResponse(responseCode,
                responseLength,
                obexVersionNumber,
                flags,
                maxPacketSize,
                headers.ToList());
        }

        public override bool Equals(object obj)
        {
            return obj is ObexConnectResponse response
                && base.Equals(obj)
                && ObexVersion == response.ObexVersion
                && Flags == response.Flags
                && MaxPacketSize == response.MaxPacketSize;
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
    }
}
