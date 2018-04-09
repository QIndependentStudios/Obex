using QIndependentStudios.Obex.Header;
using QIndependentStudios.Obex.Path;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex
{
    public class ObexSetPathRequest : ObexRequestBase
    {
        protected ObexSetPathRequest(ushort? requestLength,
            ObexSetPathFlag flags,
            IEnumerable<ObexHeader> headers)
            : this(requestLength, flags, 0x00, headers)
        {
            // Constant is currently not used, using 0x00 as default.
        }

        protected ObexSetPathRequest(ushort? requestLength,
            ObexSetPathFlag flags,
            byte constant,
            IEnumerable<ObexHeader> headers)
            : base(ObexOpCode.SetPath, requestLength, headers)
        {
            Flags = flags;
            Constant = constant;
        }

        public ObexSetPathFlag Flags { get; }
        public byte Constant { get; }

        public static ObexSetPathRequest Create(ObexSetPathFlag flags,
            params ObexHeader[] headers)
        {
            return new ObexSetPathRequest(null, flags, headers);
        }

        public static ObexSetPathRequest Create(ushort? requestLength,
            ObexSetPathFlag flags,
            params ObexHeader[] headers)
        {
            return new ObexSetPathRequest(requestLength, flags, 0x00, headers);
        }

        public static ObexSetPathRequest Create(ObexSetPathFlag flags,
            byte constant,
            params ObexHeader[] headers)
        {
            return new ObexSetPathRequest(null, flags, constant, headers);
        }

        public static ObexSetPathRequest Create(ushort? requestLength,
            ObexSetPathFlag flags,
            byte constant,
            params ObexHeader[] headers)
        {
            return new ObexSetPathRequest(requestLength, flags, constant, headers.ToList());
        }

        public static ObexSetPathRequest Create(ObexSetPathFlag flags,
            IEnumerable<ObexHeader> headers)
        {
            return new ObexSetPathRequest(null, flags, headers);
        }

        public static ObexSetPathRequest Create(ushort? requestLength,
            ObexSetPathFlag flags,
            IEnumerable<ObexHeader> headers)
        {
            return new ObexSetPathRequest(requestLength, flags, headers);
        }

        public static ObexSetPathRequest Create(ObexSetPathFlag flags,
            byte constant,
            IEnumerable<ObexHeader> headers)
        {
            return new ObexSetPathRequest(null, flags, constant, headers);
        }

        public static ObexSetPathRequest Create(ushort? requestLength,
            ObexSetPathFlag flags,
            byte constant,
            IEnumerable<ObexHeader> headers)
        {
            return new ObexSetPathRequest(requestLength, flags, constant, headers);
        }

        public override bool Equals(object obj)
        {
            return obj is ObexSetPathRequest request
                && base.Equals(obj)
                && Flags == request.Flags
                && Constant == request.Constant;
        }

        public override int GetHashCode()
        {
            var hashCode = -109303055;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + Flags.GetHashCode();
            hashCode = hashCode * -1521134295 + Constant.GetHashCode();
            return hashCode;
        }
    }
}
