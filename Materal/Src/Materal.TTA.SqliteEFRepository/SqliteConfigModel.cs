namespace Materal.TTA.SqliteEFRepository
{
    /// <summary>
    /// Sqlite配置模型
    /// </summary>
    public class SqliteConfigModel : IDBConfigModel
    {
        /// <summary>
        /// 参数前缀
        /// </summary>
        public const string ParamsPrefix = "@";
        /// <summary>
        /// 字段前缀
        /// </summary>
        public const string FieldPrefix = "\"";
        /// <summary>
        /// 字段后缀
        /// </summary>
        public const string FieldSuffix = "\"";
        /// <summary>
        /// 源
        /// </summary>
        public string Source { get; set; } = ":memory:";
        /// <summary>
        /// 密码
        /// </summary>
        public string? Password { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string? Version { get; set; }
        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString
        {
            get
            {
                string result = $"Data Source={Source};";
                if (!string.IsNullOrEmpty(Version))
                {
                    result += $"Version={Version};";
                }
                if (!string.IsNullOrEmpty(Password))
                {
                    result += $"Password={Password};";
                }
                return result;
            }
        }
    }
}
