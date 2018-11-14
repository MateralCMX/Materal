using System;
using System.Data;
using System.Reflection;

namespace Materal.ConvertHelper
{
    /// <summary>
    /// Type扩展
    /// </summary>
    public static class TypeExtension
    {
        /// <summary>
        /// 将类型转换为数据表
        /// 该数据表的列即为类型的属性
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>数据表</returns>
        public static DataTable ToDataTable(this Type type)
        {
            var dt = new DataTable();
            PropertyInfo[] props = type.GetProperties();
            foreach (PropertyInfo item in props)
            {
                Type colType = item.PropertyType;
                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {
                    colType = colType.GetGenericArguments()[0];
                }
                var dc = new DataColumn(item.Name, colType);
                dt.Columns.Add(dc);
            }
            dt.TableName = type.Name;
            return dt;
        }
    }
}
