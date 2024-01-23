namespace Materal.Gateway.Service.Models.Tools
{
    /// <summary>
    /// 同步路由模型
    /// </summary>
    public class GetRouteFromConsulModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 标签
        /// </summary>
        public string Tag { get; set; } = string.Empty;
        /// <summary>
        /// 是否允许不安全证书
        /// </summary>
        public bool DangerousAcceptAnyServerCertificateValidator { get; set; } = true;
        /// <summary>
        /// 同步模式
        /// </summary>
        public SyncModeEnum Mode { get; set; } = SyncModeEnum.Replace;
        /// <summary>
        /// 下游方案
        /// </summary>
        public string DownstreamScheme { get; set; } = "http";
        /// <summary>
        /// Http版本
        /// </summary>
        public string HttpVersion { get; set; } = "1.1";
        /// <summary>
        /// 上游路径模板
        /// </summary>
        public string UpstreamPathTemplate { get; set; } = "/api/{everything}";
        /// <summary>
        /// 下游路径模板
        /// </summary>
        public string DownstreamPathTemplate { get; set; } = "/api/{everything}";
    }
}
