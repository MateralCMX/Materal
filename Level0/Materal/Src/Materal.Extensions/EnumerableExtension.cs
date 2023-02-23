using System.Collections.ObjectModel;

namespace System.Collections.Generic
{
    /// <summary>
    /// 列表扩展
    /// </summary>
    public static class EnumerableExtension
    {
        /// <summary>
        /// 将列表转换为动态数据集合
        /// </summary>
        /// <typeparam name="T">模型</typeparam>
        /// <param name="listM">列表</param>
        /// <returns>动态数据集</returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> listM) => new ObservableCollection<T>(listM);
    }
}
