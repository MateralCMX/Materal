using MySqlConnector;
using System.ComponentModel.DataAnnotations;

namespace Materal.Logger.MySqlLogger
{
    /// <summary>
    /// Sqiite数据库字段
    /// </summary>
    public class MySqlDBFiled
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
        /// C#类型
        /// </summary>
        public Type CSharpType
        {
            get
            {
                string type = Type;
                int tempIndex;
                if (type[0] == '`')
                {
                    tempIndex = type.IndexOf('`');
                    type = type[1..tempIndex];
                }
                tempIndex = type.IndexOf('(');
                if (tempIndex > 0)
                {
                    type = type[0..tempIndex];
                }
                type = type.ToUpper();
                return type switch
                {
                    "TINYINT" => typeof(byte),
                    "SMALLINT" => typeof(short),
                    "MEDIUMINT" => typeof(int),
                    "INT" or "INTEGER" => typeof(int),
                    "BIGINT" => typeof(long),
                    "FLOAT" => typeof(float),
                    "DOUBLE" => typeof(double),
                    "DECIMAL" => typeof(decimal),
                    "CHAR" or "VARCHAR" or "TEXT" or "LONGTEXT" or "TINYTEXT" => typeof(string),
                    "DATE" => typeof(DateTime),
                    "TIME" => typeof(TimeSpan),
                    "DATETIME" or "TIMESTAMP" => typeof(DateTime),
                    "YEAR" => typeof(short),
                    "BINARY" or "VARBINARY" or "BLOB" or "MEDIUMBLOB" or "LONGBLOB" => typeof(byte[]),
                    _ => throw new LoggerException($"未知类型{Type}")
                };
            }
        }
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
        public string? Index { get; set; }
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
            string result = $"`{Name}` {Type} ";
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
