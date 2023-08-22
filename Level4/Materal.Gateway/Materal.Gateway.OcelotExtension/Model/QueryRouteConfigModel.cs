namespace Materal.Gateway.OcelotExtension.Model
{
    /// <summary>
    /// Ocelot路由配置模型
    /// </summary>
    public class QueryRouteConfigModel
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string? Url { get; set; }
        /// <summary>
        /// 服务名称
        /// </summary>
        public string? ServiceName { get; set; }
        /// <summary>
        /// Swagger键
        /// </summary>
        public string? SwaggerKey { get; set; }
        /// <summary>
        /// 启用缓存
        /// </summary>
        public bool? EnableCache { get; set; }
    }
}
