using Materal.Gateway.OcelotExtension.ConfigModel;
using System.ComponentModel.DataAnnotations;

namespace Materal.Gateway.Service.Models.Route
{
    /// <summary>
    /// 添加路由请求模型
    /// </summary>
    public class AddRouteModel
    {
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
        public FileCacheOptionsModel? FileCacheOptions { get; set; }
        /// <summary>
        /// 服务名称(服务发现)
        /// </summary>
        public string? ServiceName { get; set; }
        /// <summary>
        /// 负载均衡
        /// </summary>
        public LoadBalancerOptionsModel LoadBalancerOptions { get; set; } = new();
        /// <summary>
        /// 下游主机和端口
        /// </summary>
        public List<HostAndPortModel>? DownstreamHostAndPorts { get; set; }
        /// <summary>
        /// 服务质量
        /// </summary>
        public QoSOptionsModel? QoSOptions { get; set; }
        /// <summary>
        /// 身份认证
        /// </summary>
        public AuthenticationOptionsModel? AuthenticationOptions { get; set; }
        /// <summary>
        /// 限流配置
        /// </summary>
        public RateLimitOptionsModel? RateLimitOptions { get; set; }
        /// <summary>
        /// 忽略证书错误
        /// </summary>
        public bool DangerousAcceptAnyServerCertificateValidator { get; set; } = false;
        /// <summary>
        /// Swagger标识
        /// </summary>
        public string? SwaggerKey { get; set; }
    }
}
