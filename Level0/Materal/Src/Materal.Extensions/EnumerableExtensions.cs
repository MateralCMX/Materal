namespace Materal.Extensions
{
    /// <summary>
    /// Enumerable扩展
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// 通过HashSet去重
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sources"></param>
        /// <returns></returns>
        public static IEnumerable<T> DistinctByHashSet<T>(this IEnumerable<T> sources)
        {
            HashSet<T> hashSet = [];
            foreach (T source in sources)
            {
                if (!hashSet.Add(source)) continue;
                yield return source;
            }
        }
        /// <summary>
        /// 通过HashSet去重
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="sources"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<T> DistinctByHashSet<T, TKey>(this IEnumerable<T> sources, Func<T, TKey> keySelector)
        {
            HashSet<TKey> hashSet = [];
            foreach (T source in sources)
            {
                if (!hashSet.Add(keySelector(source))) continue;
                yield return source;
            }
        }
    }
}
