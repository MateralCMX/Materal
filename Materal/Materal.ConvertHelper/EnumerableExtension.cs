using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace Materal.ConvertHelper
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
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> listM)
        {
            return new ObservableCollection<T>(listM);
        }
        /// <summary>
        /// 转换List为DataTable
        /// </summary>
        /// <typeparam name="T">转换模型</typeparam>
        /// <param name="listM">要转换的List</param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> listM)
        {
            if (listM == null)
            {
                listM = new List<T>();
            }
            Type type = typeof(T);
            DataTable dt = type.ToDataTable();
            foreach (T item in listM)
            {
                dt.Rows.Add(item.ToDataRow(dt.NewRow()));
            }
            return dt;
        }
    }
}
