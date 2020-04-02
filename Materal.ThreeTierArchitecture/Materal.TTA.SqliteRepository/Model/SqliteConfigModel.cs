namespace Materal.TTA.SqliteRepository.Model
{
    public class SqliteConfigModel
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString
        {
            get
            {
                string result = $"Data Source={FilePath};";
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
