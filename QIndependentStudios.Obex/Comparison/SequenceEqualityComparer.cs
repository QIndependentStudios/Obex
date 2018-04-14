using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex.Comparison
{
    internal class SequenceEqualityComparer<TItem> : IEqualityComparer<IEnumerable<TItem>>
    {
        public bool Equals(IEnumerable<TItem> x, IEnumerable<TItem> y)
        {
            return (x == null && y == null)
                || IsSameSequence(x.ToList(), y.ToList());
        }

        public int GetHashCode(IEnumerable<TItem> obj)
        {
            var hashCode = 2029447620;
            foreach (var item in obj)
            {
                hashCode = hashCode * -1521134295 + item.GetHashCode();
            }

            return hashCode;
        }

        private bool IsSameSequence(List<TItem> x, List<TItem> y)
        {
            if (x.Count != y.Count)
                return false;

            for (int i = 0; i < x.Count; i++)
            {
                if (!x[i].Equals(y[i]))
                    return false;
            }

            return true;
        }
    }
}
