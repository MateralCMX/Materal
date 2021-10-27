namespace Materal.Gateway.Common.ConfigModels
{
    /// <summary>
    /// JWT配置模型
    /// </summary>
    public class OcelotConfigModel
    {
        /// <summary>
        /// 配置键
        /// </summary>
        private const string ConfigKey = "Ocelot";
        /// <summary>
        /// 管理工具Url
        /// </summary>
        public string AdministrationUrl => GetNLogConfigValue(nameof(AdministrationUrl));
        /// <summary>
        /// 客户端标识
        /// </summary>
        public string ClientSecret => GetNLogConfigValue(nameof(ClientSecret));
        /// <summary>
        /// 获得NLog配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetNLogConfigValue(string key)
        {
            return ApplicationConfig.Config.GetSection($"{ConfigKey}:{key}").Value;
        }
    }
}
