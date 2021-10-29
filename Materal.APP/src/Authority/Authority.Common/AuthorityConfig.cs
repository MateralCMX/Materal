using Materal.APP.Core;
using Materal.ConvertHelper;
using Materal.TTA.SqliteRepository.Model;

namespace Authority.Common
{
    public class AuthorityConfig
    {
        /// <summary>
        /// Sqlite数据库配置
        /// </summary>
        public static SqliteConfigModel SqliteConfig => new SqliteConfigModel
        {
            FilePath = GetConfigValue("SqliteDB:FilePath"),
            Password = null,
            Version = null
        };
        /// <summary>
        /// 默认密码
        /// </summary>
        public static string DefaultPassword => GetConfigValue("DefaultPassword");
        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string EncodePassword(string inputString)
        {
            return $"Materal{inputString}Materal".ToMd5_32Encode();
        }
        /// <summary>
        /// 获得配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetConfigValue(string key)
        {
            return ApplicationConfig.Config.GetSection(key).Value;
        }
    }
}
