using Newtonsoft.Json;

namespace Materal.Gateway.OcelotExtension.ConfigModel
{
    /// <summary>
    /// Swagger项配置模型
    /// </summary>
    public class SwaggerItemConfigModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = "SwaggerAPI";
        /// <summary>
        /// 服务发现
        /// </summary>
        public bool TakeServersFromDownstreamService { get; set; } = false;
        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; } = "v1";
        /// <summary>
        /// 服务配置
        /// </summary>
        public SwaggerServiceConfigModel? Service { get; set; }
        /// <summary>
        /// SwaggerJson文件地址
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Url { get; set; }
    }
}
