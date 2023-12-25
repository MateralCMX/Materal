using System.ComponentModel.DataAnnotations;

namespace Materal.Logger.Models
{
    /// <summary>
    /// Sqiite数据库字段
    /// </summary>
    public class SqlServerDBFiled
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
                if (type[0] == '[')
                {
                    int tempIndex = type.IndexOf(']');
                    type = type[1..tempIndex];
                }
                type = type.ToLower();
                return type switch
                {
                    "bigint" => typeof(long),
                    "binary" => typeof(byte[]),
                    "bit" => typeof(bool),
                    "char" => typeof(string),
                    "date" => typeof(DateTime),
                    "datetime" => typeof(DateTime),
                    "datetime2" => typeof(DateTime),
                    "datetimeoffset" => typeof(DateTimeOffset),
                    "decimal" => typeof(decimal),
                    "float" => typeof(double),
                    "image" => typeof(byte[]),
                    "int" => typeof(int),
                    "money" => typeof(decimal),
                    "nchar" => typeof(string),
                    "ntext" => typeof(string),
                    "numeric" => typeof(decimal),
                    "nvarchar" => typeof(string),
                    "real" => typeof(float),
                    "smalldatetime" => typeof(DateTime),
                    "smallint" => typeof(short),
                    "smallmoney" => typeof(decimal),
                    "text" => typeof(string),
                    "time" => typeof(TimeSpan),
                    "timestamp" => typeof(byte[]),
                    "tinyint" => typeof(byte),
                    "uniqueidentifier" => typeof(Guid),
                    "varbinary" => typeof(byte[]),
                    "varchar" => typeof(string),
                    "xml" => typeof(string),
                    _ => throw new LoggerException("未知类型")
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
            string result = $"[{Name}] {Type} ";
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
