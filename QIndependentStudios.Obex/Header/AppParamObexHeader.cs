using QIndependentStudios.Obex.Comparison;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex.Header
{
    public class AppParamObexHeader : ObexHeader
    {
        private IEnumerable<ObexAppParameter> _parameters;

        protected AppParamObexHeader(IEnumerable<ObexAppParameter> parameters, ushort? headerLength)
        {
            Id = ObexHeaderId.ApplicationParameter;
            ActualLength = headerLength;
            _parameters = parameters ?? new List<ObexAppParameter>();
        }

        public IEnumerable<ObexAppParameter> Parameters => _parameters.ToList().AsReadOnly();

        public static AppParamObexHeader Create(params ObexAppParameter[] parameters)
            => new AppParamObexHeader(parameters, null);

        public static AppParamObexHeader Create(IEnumerable<ObexAppParameter> parameters)
            => new AppParamObexHeader(parameters, null);

        public static AppParamObexHeader Create(ushort? headerLength, params ObexAppParameter[] parameters)
            => new AppParamObexHeader(parameters, headerLength);

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
