using Materal.Gateway.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Materal.Gateway.Services.Models
{
    /// <summary>
    /// 配置项模型
    /// </summary>
    public class ConfigItemFileModel
    {
        /// <summary>
        /// 配置项模型
        /// </summary>
        public ConfigItemFileModel() { }
        /// <summary>
        /// 配置项模型
        /// </summary>
        /// <param name="model">服务名称</param>
        public ConfigItemFileModel(ConfigItemModel model)
        {
            ServiceName = model.ServiceName;
            UpstreamPathTemplate = $"/{ServiceName}/" + "{everything}";
            if (model.EnableCache)
            {
                FileCacheOptions = new CacheFileModel
                {
                    Region = $"{ServiceName}Region"
                };
            }
        }
        /// <summary>
        /// 下游路径
        /// </summary>
        public string DownstreamPathTemplate { get; set; } = "/api/{everything}";
        /// <summary>
        /// 下游Scheme
        /// </summary>
        public string DownstreamScheme { get; set; } = "http";
        /// <summary>
        /// 
        /// </summary>
        public string UpstreamPathTemplate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> UpstreamHttpMethod { get; set; } = new List<string>
        {
            "Get",
            "Post"
        };
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool UseServiceDiscovery { get; set; } = true;
        /// <summary>
        /// 缓存
        /// </summary>
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public CacheFileModel FileCacheOptions { get; set; }
    }
}
