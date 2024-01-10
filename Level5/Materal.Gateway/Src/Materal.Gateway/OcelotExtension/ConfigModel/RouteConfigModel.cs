using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Materal.Gateway.OcelotExtension.ConfigModel
{
    /// <summary>
    /// 路由配置模型
    /// </summary>
    public class RouteConfigModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; } = Guid.NewGuid();
        /// <summary>
        /// 上游路径模版
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public string UpstreamPathTemplate { get; set; } = "/{everything}";
        /// <summary>
        /// 上游Http方法
        /// </summary>
        public List<string> UpstreamHttpMethod { get; set; } = [];
        /// <summary>
        /// 下游路径模版
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public string DownstreamPathTemplate { get; set; } = "/{everything}";
        /// <summary>
        /// 下游方案
        /// http https ws wss grpc grpcs
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public string DownstreamScheme { get; set; } = "http";
        /// <summary>
        /// 下游Http版本
        /// </summary>
        public string DownstreamHttpVersion { get; set; } = "1.1";
        /// <summary>
        /// 缓存
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public FileCacheOptionsModel? FileCacheOptions { get; set; }
        /// <summary>
        /// 服务名称(服务发现)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? ServiceName { get; set; }
        /// <summary>
        /// 负载均衡
        /// </summary>
        public LoadBalancerOptionsModel LoadBalancerOptions { get; set; } = new();
        /// <summary>
        /// 下游主机和端口
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<HostAndPortModel>? DownstreamHostAndPorts { get; set;}
        /// <summary>
        /// 服务质量
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public QoSOptionsModel? QoSOptions { get; set; }
        /// <summary>
        /// 身份认证
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AuthenticationOptionsModel? AuthenticationOptions { get; set; }
        /// <summary>
        /// 限流配置
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public RateLimitOptionsModel? RateLimitOptions { get; set; }
        /// <summary>
        /// 忽略证书错误
        /// </summary>
        public bool DangerousAcceptAnyServerCertificateValidator { get; set; } = false;
        /// <summary>
        /// Swagger标识
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? SwaggerKey { get; set; }
        /// <summary>
        /// 位序
        /// </summary>
        public int Index { get; set; }
    }
}
