using QIndependentStudios.Obex.Comparison;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex.Header
{
    /// <summary>
    /// Represents an Obex header that has a collection of application parameters.
    /// </summary>
    public class AppParamObexHeader : ObexHeader
    {
        private IEnumerable<ObexAppParameter> _parameters;

        private AppParamObexHeader(IEnumerable<ObexAppParameter> parameters, ushort? headerLength)
        {
            Id = ObexHeaderId.ApplicationParameter;
            ActualLength = headerLength;
            _parameters = parameters ?? new List<ObexAppParameter>();
        }

        /// <summary>
        /// Gets the application parameters in the header.
        /// </summary>
        public IEnumerable<ObexAppParameter> Parameters => _parameters.ToList().AsReadOnly();

        /// <summary>
        /// Creates a new instance of the <see cref="AppParamObexHeader"/> class with Obex application parameters.
        /// </summary>
        /// <param name="parameters">The Obex application parameters for the header.</param>
        /// <returns>The created header.</returns>
        public static AppParamObexHeader Create(params ObexAppParameter[] parameters)
            => new AppParamObexHeader(parameters, null);

        /// <summary>
        /// Creates a new instance of the <see cref="AppParamObexHeader"/> class with Obex application parameters.
        /// </summary>
        /// <param name="parameters">The Obex application parameters for the header.</param>
        /// <returns>The created header.</returns>
        public static AppParamObexHeader Create(IEnumerable<ObexAppParameter> parameters)
            => new AppParamObexHeader(parameters, null);

        /// <summary>
        /// Creates a new instance of the <see cref="AppParamObexHeader"/> class with Obex application parameters and header length.
        /// </summary>
        /// <param name="headerLength">The header length. Provide this value when deserializing from binary data.</param>
        /// <param name="parameters">The Obex application parameters for the header.</param>
        /// <returns>The created header.</returns>
        public static AppParamObexHeader Create(ushort? headerLength, params ObexAppParameter[] parameters)
            => new AppParamObexHeader(parameters, headerLength);


        /// <summary>
        /// Creates a new instance of the <see cref="AppParamObexHeader"/> class with Obex application parameters and header length.
        /// </summary>
        /// <param name="headerLength">The header length. Provide this value when deserializing from binary data.</param>
        /// <param name="parameters">The Obex application parameters for the header.</param>
        /// <returns>The created header.</returns>
        public static AppParamObexHeader Create(ushort? headerLength, IEnumerable<ObexAppParameter> parameters)
            => new AppParamObexHeader(parameters, headerLength);

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is AppParamObexHeader header
                && base.Equals(obj)
                && new SequenceEqualityComparer<ObexAppParameter>().Equals(_parameters, header._parameters);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hashCode = -1494980905;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + new SequenceEqualityComparer<ObexAppParameter>().GetHashCode(_parameters);
            return hashCode;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Id} - {_parameters.Count()} parameter(s)";
        }
    }
}
