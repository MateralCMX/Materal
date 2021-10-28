using System;

namespace Materal.APP.Core.ConfigModels
{
    public class ConsulConfigModel
    {
        /// <summary>
        /// 配置键
        /// </summary>
        private const string ConfigKey = "Consul";
        /// <summary>
        /// 地址
        /// </summary>
        public string Address => GetConfigValue(nameof(Address));
        /// <summary>
        /// 健康检查间隔
        /// </summary>
        public int HealthInterval => Convert.ToInt32(GetConfigValue(nameof(HealthInterval)));
        /// <summary>
        /// 重连间隔
        /// </summary>
        public int ReconnectionInterval => Convert.ToInt32(GetConfigValue(nameof(ReconnectionInterval)));
        /// <summary>
        /// 获得NLog配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetConfigValue(string key)
        {
            return ApplicationConfig.Config.GetSection($"{ConfigKey}:{key}").Value;
        }
    }
}
