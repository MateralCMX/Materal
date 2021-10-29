namespace Materal.APP.Core.ConfigModels
{
    public class GatewayConfigModel
    {
        /// <summary>
        /// 配置键
        /// </summary>
        private const string ConfigKey = "Gateway";
        /// <summary>
        /// 地址
        /// </summary>
        public string Address => GetConfigValue(nameof(Address));
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
