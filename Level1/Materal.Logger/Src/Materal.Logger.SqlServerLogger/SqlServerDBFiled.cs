﻿using System.ComponentModel.DataAnnotations;

namespace Materal.Logger.SqlServerLogger
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
                int tempIndex;
                if (type[0] == '[')
                {
                    tempIndex = type.IndexOf(']');
                    type = type[1..tempIndex];
                }
                tempIndex = type.IndexOf('(');
                if (tempIndex > 0)
                {
                    type = type[0..tempIndex];
                }
                type = type.ToLower();
                return type switch
                {
                    "bit" => typeof(bool),
                    "tinyint" => typeof(byte),
                    "smallint" => typeof(short),
                    "int" => typeof(int),
                    "bigint" => typeof(long),
                    "float" => typeof(float),
                    "real" => typeof(double),
                    "decimal" or "numeric" or "money" or "smallmoney" => typeof(decimal),
                    "char" or "varchar" or "text" or "nchar" or "nvarchar" or "ntext" or "xml" => typeof(string),
                    "date" or "datetime" or "datetime2" or "smalldatetime" => typeof(DateTime),
                    "datetimeoffset" => typeof(DateTimeOffset),
                    "time" => typeof(TimeSpan),
                    "timestamp" or "binary" or "varbinary" or "image" => typeof(byte[]),// 通常用于存储时间戳的二进制数据，需要序列化成字节数组
                    "uniqueidentifier" => typeof(Guid),
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
