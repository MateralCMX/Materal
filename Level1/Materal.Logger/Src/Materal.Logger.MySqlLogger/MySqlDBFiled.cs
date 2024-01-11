namespace Materal.Logger.MySqlLogger
{
    /// <summary>
    /// Sqiite数据库字段
    /// </summary>
    public class MySqlDBFiled : BaseDBFiled, IDBFiled
    {
        /// <summary>
        /// C#类型
        /// </summary>
        public override Type CSharpType
        {
            get
            {
                string type = Type;
                int tempIndex = type.IndexOf('(');
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
        /// 获得创建表字段SQL
        /// </summary>
        /// <returns></returns>
        public override string GetCreateTableFiledSQL()
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
