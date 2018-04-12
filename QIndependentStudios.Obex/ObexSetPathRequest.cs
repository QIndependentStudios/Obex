using QIndependentStudios.Obex.Header;
using QIndependentStudios.Obex.Path;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex
{
    /// <summary>
    /// Represents an Obex request used specifically when setting the current Obex directory path.
    /// </summary>
    public class ObexSetPathRequest : ObexRequestBase
    {
        private ObexSetPathRequest(ushort? requestLength,
            ObexSetPathFlag flags,
            IEnumerable<ObexHeader> headers)
            : this(requestLength, flags, 0x00, headers)
        {
            // Constant is currently not used, using 0x00 as default.
        }

        private ObexSetPathRequest(ushort? requestLength,
            ObexSetPathFlag flags,
            byte constant,
            IEnumerable<ObexHeader> headers)
            : base(ObexOpCode.SetPath, requestLength, headers)
        {
            Flags = flags;
            Constant = constant;
        }

        /// <summary>
        /// Gets the flags that determines the directory navigation on the server.
        /// </summary>
        public ObexSetPathFlag Flags { get; }

        /// <summary>
        /// Gets the set path request constant.
        /// </summary>
        public byte Constant { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="ObexSetPathRequest"/>.
        /// </summary>
        /// <param name="flags">The flags that determine the directory navigation on the server.</param>
        /// <param name="headers">A collection of Obex headers.</param>
        /// <returns>The created request.</returns>
        public static ObexSetPathRequest Create(ObexSetPathFlag flags,
            params ObexHeader[] headers)
        {
            return new ObexSetPathRequest(null, flags, headers);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ObexSetPathRequest"/>.
        /// </summary>
        /// <param name="requestLength">The request length. Provide this value when deserializing from binary data.</param>
        /// <param name="flags">The flags that determine the directory navigation on the server.</param>
        /// <param name="headers">A collection of Obex headers.</param>
        /// <returns>The created request.</returns>
        public static ObexSetPathRequest Create(ushort? requestLength,
            ObexSetPathFlag flags,
            params ObexHeader[] headers)
        {
            return new ObexSetPathRequest(requestLength, flags, 0x00, headers);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ObexSetPathRequest"/>.
        /// </summary>
        /// <param name="flags">The flags that determine the directory navigation on the server.</param>
        /// <param name="constant">The set path request constant. Currently not used.</param>
        /// <param name="headers">A collection of Obex headers.</param>
        /// <returns>The created request.</returns>
        public static ObexSetPathRequest Create(ObexSetPathFlag flags,
            byte constant,
            params ObexHeader[] headers)
        {
            return new ObexSetPathRequest(null, flags, constant, headers);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ObexSetPathRequest"/>.
        /// </summary>
        /// <param name="requestLength">The request length. Provide this value when deserializing from binary data.</param>
        /// <param name="flags">The flags that determine the directory navigation on the server.</param>
        /// <param name="constant">The set path request constant. Currently not used.</param>
        /// <param name="headers">A collection of Obex headers.</param>
        /// <returns>The created request.</returns>
        public static ObexSetPathRequest Create(ushort? requestLength,
            ObexSetPathFlag flags,
            byte constant,
            params ObexHeader[] headers)
        {
            return new ObexSetPathRequest(requestLength, flags, constant, headers.ToList());
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ObexSetPathRequest"/>.
        /// </summary>
        /// <param name="flags">The flags that determine the directory navigation on the server.</param>
        /// <param name="headers">A collection of Obex headers.</param>
        /// <returns>The created request.</returns>
        public static ObexSetPathRequest Create(ObexSetPathFlag flags,
            IEnumerable<ObexHeader> headers)
        {
            return new ObexSetPathRequest(null, flags, headers);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ObexSetPathRequest"/>.
        /// </summary>
        /// <param name="requestLength">The request length. Provide this value when deserializing from binary data.</param>
        /// <param name="flags">The flags that determine the directory navigation on the server.</param>
        /// <param name="headers">A collection of Obex headers.</param>
        /// <returns>The created request.</returns>
        public static ObexSetPathRequest Create(ushort? requestLength,
            ObexSetPathFlag flags,
            IEnumerable<ObexHeader> headers)
        {
            return new ObexSetPathRequest(requestLength, flags, headers);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ObexSetPathRequest"/>.
        /// </summary>
        /// <param name="flags">The flags that determine the directory navigation on the server.</param>
        /// <param name="constant">The set path request constant. Currently not used.</param>
        /// <param name="headers">A collection of Obex headers.</param>
        /// <returns>The created request.</returns>
        public static ObexSetPathRequest Create(ObexSetPathFlag flags,
            byte constant,
            IEnumerable<ObexHeader> headers)
        {
            return new ObexSetPathRequest(null, flags, constant, headers);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ObexSetPathRequest"/>.
        /// </summary>
        /// <param name="requestLength">The request length. Provide this value when deserializing from binary data.</param>
        /// <param name="flags">The flags that determine the directory navigation on the server.</param>
        /// <param name="constant">The set path request constant. Currently not used.</param>
        /// <param name="headers">A collection of Obex headers.</param>
        /// <returns>The created request.</returns>
        public static ObexSetPathRequest Create(ushort? requestLength,
            ObexSetPathFlag flags,
            byte constant,
            IEnumerable<ObexHeader> headers)
        {
            return new ObexSetPathRequest(requestLength, flags, constant, headers);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is ObexSetPathRequest request
                && base.Equals(obj)
                && Flags == request.Flags
                && Constant == request.Constant;
        }

        /// <inheritdoc/>
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
