﻿using Newtonsoft.Json;

namespace Materal.Gateway.OcelotExtension.ConfigModel
{
    /// <summary>
    /// 全局配置模型
    /// </summary>
    public class GlobalConfigurationModel
    {
        /// <summary>
        /// 服务发现配置
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ServiceDiscoveryProviderModel? ServiceDiscoveryProvider { get; set; }
        /// <summary>
        /// 限流配置
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public GlobalRateLimitOptionsModel? RateLimitOptions { get; set; }
    }
}
