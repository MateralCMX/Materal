using System.Collections.ObjectModel;

namespace System.Collections.Generic
{
    /// <summary>
    /// 集合扩展
    /// </summary>
    public static partial class CollectionExtension
    {
        /// <summary>
        /// 将列表转换为动态数据集合
        /// </summary>
        /// <typeparam name="T">模型</typeparam>
        /// <param name="listM">列表</param>
        /// <returns>动态数据集</returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> listM) => new ObservableCollection<T>(listM);
        /// <summary>
        /// 获得需要新增数组与需要删除的数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceArray"></param>
        /// <param name="oldArray"></param>
        /// <returns></returns>
        public static (ICollection<T> addArray, ICollection<T> removeArray) GetAddArrayAndRemoveArray<T>(this ICollection<T> sourceArray, ICollection<T> oldArray)
        {
            ICollection<T> addArray = sourceArray.Except(oldArray).ToArray();
            ICollection<T> removeArray = oldArray.Except(sourceArray).ToArray();
            return (addArray, removeArray);
        }
        /// <summary>
        /// 去重
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
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
