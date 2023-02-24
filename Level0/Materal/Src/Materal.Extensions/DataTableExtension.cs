﻿using Materal.Extensions;
using System.Reflection;

namespace System.Data
{
    /// <summary>
    /// DataTable扩展
    /// </summary>
    public static class DataTableExtension
    {
        /// <summary>
        /// 转换List为DataTable
        /// </summary>
        /// <typeparam name="T">转换模型</typeparam>
        /// <param name="listM">要转换的List</param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> listM)
        {
            listM ??= new List<T>();
            Type type = typeof(T);
            DataTable dt = type.ToDataTable();
            foreach (T item in listM)
            {
                if (item == null) continue;
                dt.Rows.Add(item.ToDataRow(dt.NewRow()));
            }
            return dt;
        }
        /// <summary>
        /// 对象转换为数据行
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="dr">数据行模版</param>
        /// <returns>数据行</returns>
        public static DataRow ToDataRow(this object obj, DataRow dr)
        {
            if (dr == null) throw new ExtensionException("数据行不可为空");
            var type = obj.GetType();
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                var value = prop.GetValue(obj, null);
                if (value == null)
                {
                    dr[prop.Name] = DBNull.Value;
                }
                else
                {
                    dr[prop.Name] = value;
                }
            }
            return dr;
        }
        /// <summary>
        /// 对象转换为数据行
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>数据行</returns>
        public static DataRow ToDataRow(this object obj)
        {
            Type type = obj.GetType();
            DataTable dt = type.ToDataTable();
            DataRow dr = dt.NewRow();
            return obj.ToDataRow(dr);
        }
        /// <summary>
        /// 通过数据行设置对象的值
        /// </summary>
        /// <param name="obj">要设置的对象</param>
        /// <param name="dr">数据行</param>
        /// <param name="exceptions"></param>
        public static void SetValueByDataRow(this object obj, DataRow dr, ref List<Exception> exceptions)
        {
            Type type = obj.GetType();
            PropertyInfo[] props = type.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                try
                {
                    prop.SetValue(obj, dr[prop.Name].ConvertTo(prop.PropertyType), null);
                }
                catch (Exception exception)
                {
                    exceptions.Add(exception);
                }
            }
        }
        /// <summary>
        /// 数据行转换为目标对象
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="dataRow">数据行</param>
        /// <param name="exceptions"></param>
        /// <returns>目标对象</returns>
        public static T ToObject<T>(this DataRow dataRow, ref List<Exception> exceptions)
        {
            T result = TypeHelper.Instantiation<T>();
            if (result == null) throw new ExtensionException("转换失败");
            result.SetValueByDataRow(dataRow, ref exceptions);
            return result;
        }
        /// <summary>
        /// 把数据表转换为List
        /// </summary>
        /// <typeparam name="T">要转换的类型(需要有一个没有参数的构造方法)</typeparam>
        /// <param name="dataTable">数据表</param>
        /// <param name="exceptions"></param>
        /// <returns>转换后的List</returns>
        public static List<T> ToList<T>(this DataTable dataTable, ref List<Exception> exceptions)
        {
            List<T> result = new();
            foreach (DataRow dr in dataTable.Rows)
            {
                var value = dr.ToObject<T>(ref exceptions);
                if (value == null) continue;
                result.Add(value);
            }
            return result;
        }
        /// <summary>
        /// 把数据表转换为数组
        /// </summary>
        /// <typeparam name="T">要转换的类型(需要有一个没有参数的构造方法)</typeparam>
        /// <param name="dataTable">数据表</param>
        /// <param name="exceptions"></param>
        /// <returns>转换后的数组</returns>
        public static T?[] ToArray<T>(this DataTable dataTable, ref List<Exception> exceptions)
        {
            int count = dataTable.Rows.Count;
            var result = new T?[count];
            for (var i = 0; i < count; i++)
            {
                result[i] = dataTable.Rows[i].ToObject<T>(ref exceptions);
            }
            return result;
        }
        /// <summary>
        /// 把数据集转换为List
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="dataSet">数据集</param>
        /// <param name="exceptions"></param>
        /// <returns>转换后的List</returns>
        public static List<List<T>> ToList<T>(this DataSet dataSet, ref List<Exception> exceptions)
        {
            List<List<T>> result = new();
            foreach (DataTable dt in dataSet.Tables)
            {
                result.Add(dt.ToList<T>(ref exceptions));
            }
            return result;
        }
        /// <summary>
        /// 把数据集转换为数组
        /// </summary>
        /// <typeparam name="T">要转换的类型(需要有一个没有参数的构造方法)</typeparam>
        /// <param name="dataSet">数据集</param>
        /// <param name="exceptions"></param>
        /// <returns>转换后的数组</returns>
        public static T?[,] ToArray<T>(this DataSet dataSet, ref List<Exception> exceptions)
        {
            int tableCount = dataSet.Tables.Count;
            DataTableCollection tables = dataSet.Tables;
            var rowCounts = new int[tableCount];
            for (var i = 0; i < tableCount; i++)
            {
                rowCounts[i] = tables[i].Rows.Count;
            }
            int rowCount = rowCounts.Max();
            var result = new T?[tableCount, rowCount];
            for (var i = 0; i < tableCount; i++)
            {
                for (var j = 0; j < rowCounts[i]; j++)
                {
                    DataRow row = tables[i].Rows[j];
                    result[i, j] = row.ToObject<T>(ref exceptions);
                }
            }
            return result;
        }
    }
}