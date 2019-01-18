using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Materal.ConvertHelper
{
    /// <summary>
    /// DataTable扩展
    /// </summary>
    public static class DataTableExtension
    {
        /// <summary>
        /// 数据行转换为目标对象
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="dataRow">数据行</param>
        /// <returns>目标对象</returns>
        public static T ToObject<T>(this DataRow dataRow)
        {
            var result = ConvertManager.GetDefaultObject<T>();
            if (result != null)
            {
                result.SetValueByDataRow(dataRow);
            }
            return result == null ? default(T) : result;
        }
        /// <summary>
        /// 把数据表转换为List
        /// </summary>
        /// <typeparam name="T">要转换的类型(需要有一个没有参数的构造方法)</typeparam>
        /// <param name="dataTable">数据表</param>
        /// <returns>转换后的List</returns>
        public static List<T> ToList<T>(this DataTable dataTable)
        {
            var result = new List<T>();
            foreach (DataRow dr in dataTable.Rows)
            {
                result.Add(dr.ToObject<T>());
            }
            return result;
        }
        /// <summary>
        /// 把数据表转换为数组
        /// </summary>
        /// <typeparam name="T">要转换的类型(需要有一个没有参数的构造方法)</typeparam>
        /// <param name="dataTable">数据表</param>
        /// <returns>转换后的数组</returns>
        public static T[] ToArray<T>(this DataTable dataTable)
        {
            int count = dataTable.Rows.Count;
            var result = new T[count];
            for (var i = 0; i < count; i++)
            {
                result[i] = dataTable.Rows[i].ToObject<T>();
            }
            return result;
        }
        /// <summary>
        /// 把数据集转换为List
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="dataSet">数据集</param>
        /// <returns>转换后的List</returns>
        public static List<List<T>> ToList<T>(this DataSet dataSet)
        {
            var result = new List<List<T>>();
            foreach (DataTable dt in dataSet.Tables)
            {
                result.Add(dt.ToList<T>());
            }
            return result;
        }
        /// <summary>
        /// 把数据集转换为数组
        /// </summary>
        /// <typeparam name="T">要转换的类型(需要有一个没有参数的构造方法)</typeparam>
        /// <param name="dataSet">数据集</param>
        /// <returns>转换后的数组</returns>
        public static T[,] ToArray<T>(this DataSet dataSet)
        {
            int tableCount = dataSet.Tables.Count;
            var rowCounts = new int[tableCount];
            for (var i = 0; i < tableCount; i++)
            {
                rowCounts[i] = dataSet.Tables[i].Rows.Count;
            }
            int rowCount = rowCounts.Max();
            var result = new T[tableCount, rowCount];
            for (var i = 0; i < tableCount; i++)
            {
                for (var j = 0; j < rowCounts[i]; j++)
                {
                    result[i, j] = dataSet.Tables[i].Rows[j].ToObject<T>();
                }
            }
            return result;
        }
    }
}
