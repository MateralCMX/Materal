namespace Materal.Logger.SqliteLogger
{
    /// <summary>
    /// Sqiite数据库字段
    /// </summary>
    public class SqliteDBFiled : BaseDBFiled, IDBFiled
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
                    "TEXT" => typeof(string),
                    "DATE" => typeof(DateTime),
                    "INTEGER" => typeof(int),
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
