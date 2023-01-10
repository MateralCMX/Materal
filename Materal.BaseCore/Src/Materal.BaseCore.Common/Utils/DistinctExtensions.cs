using Materal.BaseCore.Common.Utils;

namespace Materal.BaseCore.Common.Utils
{
    public static class DistinctExtensions
    {
        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> source, Func<T, T, bool> comparer) where T : class
        {
            return source.Distinct(new DynamicEqualityComparer<T>(comparer));
        }

        private sealed class DynamicEqualityComparer<T> : IEqualityComparer<T> where T : class
        {
            private readonly Func<T, T, bool> _func;

            public DynamicEqualityComparer(Func<T, T, bool> func)
            {
                _func = func;
            }

            public bool Equals(T? x, T? y)
            {
                if (x == null && y == null) return true;
                else if (x == null || y == null) return false;
                return _func(x, y);
            }

            public int GetHashCode(T obj) => 0;
        }
    }
}
