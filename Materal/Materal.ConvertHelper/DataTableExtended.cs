using System.Collections.Generic;
using System.Data;

namespace Materal.ConvertHelper
{
    /// <summary>
    /// DataTable扩展
    /// </summary>
    public static class DataTableExtended
    {
        /// <summary>
        /// 数据行转换为目标对象
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="dr">数据行</param>
        /// <returns>目标对象</returns>
        public static T ToObject<T>(this DataRow dr)
        {
            var model = ConvertManager.GetDefultObject<T>();
            if (model != null)
            {
                model.SetValueByDataRow(dr);
            }
            return model == null ? default(T) : model;
        }
        /// <summary>
        /// 把数据表转换为List
        /// </summary>
        /// <typeparam name="T">要转换的类型(需要有一个没有参数的构造方法)</typeparam>
        /// <param name="dt">数据表</param>
        /// <returns>转换后的List</returns>
        public static List<T> ToList<T>(this DataTable dt)
        {
            var listMs = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                listMs.Add(dr.ToObject<T>());
            }
            return listMs;
        }
        /// <summary>
        /// 把数据集转换为List
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="ds">数据集</param>
        /// <returns>转换后的List</returns>
        public static List<List<T>> ToList<T>(this DataSet ds)
        {
            var listMs = new List<List<T>>();
            foreach (DataTable dt in ds.Tables)
            {
                listMs.Add(dt.ToList<T>());
            }
            return listMs;
        }
    }
}
