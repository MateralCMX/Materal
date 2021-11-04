using Materal.APP.Core;

namespace Deploy.Common.Models
{
    public class DeployConfigModel
    {
        /// <summary>
        /// 配置键
        /// </summary>
        private const string ConfigKey = "Deploy";
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
