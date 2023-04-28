using Materal.BusinessFlow.ADONETRepository.Models;

namespace Materal.BusinessFlow.SqliteRepository.Models
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
