using Materal.Logger.Repositories;
using System.Data;

namespace Materal.Logger
{
    /// <summary>
    /// 数据库字段扩展
    /// </summary>
    public static class DBFiledExtension
    {
        /// <summary>
        /// 创建数据表
        /// </summary>
        /// <param name="dBFileds"></param>
        /// <returns></returns>
        public static DataTable CreateDataTable(this ICollection<IDBFiled> dBFileds)
        {
            DataTable dt = new();
            foreach (IDBFiled filed in dBFileds)
            {
                dt.Columns.Add(filed.Name, filed.CSharpType);
            }
            return dt;
        }
        /// <summary>
        /// 获得新行
        /// </summary>
        /// <param name="dBFileds"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static void AddNewRow(this ICollection<IDBFiled> dBFileds, DataTable dt)
        {
            DataRow dr = dt.NewRow();
            foreach (IDBFiled filed in dBFileds)
            {
                if (filed.Value is not null)
                {
                    Type? targetType = dt.Columns[filed.Name]?.DataType;
                    if (targetType == null) continue;
                    if (filed.Value.CanConvertTo(targetType))
                    {
                        dr[filed.Name] = filed.Value.ConvertTo(targetType);
                    }
                    else
                    {
                        dr[filed.Name] = filed.Value;
                    }
                }
                else
                {
                    dr[filed.Name] = DBNull.Value;
                }
            }
            dt.Rows.Add(dr);
        }
    }
}
