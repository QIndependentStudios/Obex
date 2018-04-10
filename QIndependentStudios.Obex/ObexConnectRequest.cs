using QIndependentStudios.Obex.Header;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex
{
    public class ObexConnectRequest : ObexRequestBase
    {
        public const ushort MaxPacketSizeLowerBound = 255;
        public const ushort MaxPacketSizeUpperBound = 65534;

        protected ObexConnectRequest(ushort? requestLength,
            ushort maxPacketSize,
            IEnumerable<ObexHeader> headers)
            : this(requestLength, 0x10, 0x00, maxPacketSize, headers)
        {
            // Currently the only Obex version is 0x10.
            // Flags are currently not used, using 0x00 as default.
        }

        protected ObexConnectRequest(ushort? requestLength,
            byte obexVersion,
            byte flag,
            ushort maxPacketSize,
            IEnumerable<ObexHeader> headers)
            : base(ObexOpCode.Connect, requestLength, headers)
        {
            if (maxPacketSize < MaxPacketSizeLowerBound)
                throw new ArgumentException($"MaxPacketSize cannot be less than {MaxPacketSizeLowerBound}");
            if (maxPacketSize > MaxPacketSizeUpperBound)
                throw new ArgumentException($"MaxPacketSize cannot be greater than {MaxPacketSizeUpperBound}");

            ObexVersion = obexVersion;
            Flags = flag;
            MaxPacketSize = maxPacketSize;
        }

        public byte ObexVersion { get; }
        public byte Flags { get; }
        public ushort MaxPacketSize { get; }

        public static ObexConnectRequest Create(ushort maxPacketSize, params ObexHeader[] headers)
        {
            return new ObexConnectRequest(null, maxPacketSize, headers);
        }

        public static ObexConnectRequest Create(ushort requestLength,
            ushort maxPacketSize,
            params ObexHeader[] headers)
        {
            return Create(requestLength, maxPacketSize, headers.ToList());
        }

        public static ObexConnectRequest Create(byte obexVersion,
            byte flag,
            ushort maxPacketSize,
            params ObexHeader[] headers)
        {
            return new ObexConnectRequest(null, obexVersion, flag, maxPacketSize, headers);
        }

        public static ObexConnectRequest Create(ushort requestLength,
            byte obexVersion,
            byte flag,
            ushort maxPacketSize,
            params ObexHeader[] headers)
        {
            return new ObexConnectRequest(requestLength, obexVersion, flag, maxPacketSize, headers);
        }

        public static ObexConnectRequest Create(ushort maxPacketSize, IEnumerable<ObexHeader> headers)
        {
            return new ObexConnectRequest(null, maxPacketSize, headers);
        }

        public static ObexConnectRequest Create(ushort requestLength,
            ushort maxPacketSize,
            IEnumerable<ObexHeader> headers)
        {
            return new ObexConnectRequest(requestLength, 0x10, 0x00, maxPacketSize, headers);
        }

        public static ObexConnectRequest Create(byte obexVersion,
            byte flag,
            ushort maxPacketSize,
            IEnumerable<ObexHeader> headers)
        {
            return new ObexConnectRequest(null, obexVersion, flag, maxPacketSize, headers);
        }

        public static ObexConnectRequest Create(ushort requestLength,
            byte obexVersion,
            byte flag,
            ushort maxPacketSize,
            IEnumerable<ObexHeader> headers)
        {
            return new ObexConnectRequest(requestLength, obexVersion, flag, maxPacketSize, headers);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is ObexConnectRequest request
                && base.Equals(obj)
                && ObexVersion == request.ObexVersion
                && Flags == request.Flags
                && MaxPacketSize == request.MaxPacketSize;
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
