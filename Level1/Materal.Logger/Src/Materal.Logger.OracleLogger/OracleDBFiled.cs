namespace Materal.Logger.OracleLogger
{
    /// <summary>
    /// Oracle数据库字段
    /// </summary>
    public class OracleDBFiled : BaseDBFiled, IDBFiled
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
                    "NUMBER" => typeof(decimal),// Or double, float, int, etc., depending on scale and precision.
                    "DATE" or "TIMESTAMP" => typeof(DateTime),
                    "CLOB" or "CHAR" or "VARCHAR2" => typeof(string),
                    "BLOB" => typeof(byte[]),
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
            string result = $"\"{Name}\" {Type}";
            if (PK || !IsNull)
            {
                result += " NOT NULL";
            }
            return result;
        }
    }
}
