using Materal.APP.Core;

namespace ConfigCenter.Environment.Common.Models
{
    public class EnvironmentConfigModel
    {
        /// <summary>
        /// 配置键
        /// </summary>
        private const string ConfigKey = "Environment";
        /// <summary>
        /// 键
        /// </summary>
        public string Key => GetConfigValue(nameof(Key));
        /// <summary>
        /// 名称
        /// </summary>
        public string Name => GetConfigValue(nameof(Name));
        /// <summary>
        /// 获得配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetConfigValue(string key)
        {
            return ApplicationConfig.Config.GetSection($"{ConfigKey}:{key}").Value;
        }
    }
}
