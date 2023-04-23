using Materal.TTA.EFRepository;

namespace Materal.TTA.SqliteRepository.Model
{
    public class SqliteConfigModel : IDBConfigModel
    {
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
