using System.ComponentModel.DataAnnotations;

namespace Materal.Logger.Models
{
    /// <summary>
    /// Sqiite数据库字段
    /// </summary>
    public class SqliteDBFiled
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 数据库类型
        /// </summary>
        [Required(ErrorMessage = "数据库类型不能为空")]
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// 值
        /// </summary>
        public string? Value { get; set; }
        /// <summary>
        /// 主键
        /// </summary>
        public bool PK { get; set; } = false;
        /// <summary>
        /// 索引
        /// </summary>
        public bool Index { get; set; } = false;
        /// <summary>
        /// 可以为空
        /// </summary>
        public bool IsNull { get; set; } = true;
        /// <summary>
        /// 获得创建表字段SQL
        /// </summary>
        /// <returns></returns>
        public string GetCreateTableFiledSQL()
        {
            string result = $"\"{Name}\" {Type} ";
            if (PK)
            {
                result += "NOT NULL";
            }
            else
            {
                result += IsNull ? "NULL" : "NOT NULL";
            }
            return result;
        }
    }
}
