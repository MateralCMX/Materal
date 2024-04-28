namespace Materal.Gateway.Service.Models.Route
{
    /// <summary>
    /// 查询路由模型
    /// </summary>
    public class QueryRouteModel : FilterModel
    {
        /// <summary>
        /// 上游路径模版
        /// </summary>
        [Contains]
        public string? UpstreamPathTemplate { get; set; }
        /// <summary>
        /// 下游路径模版
        /// </summary>
        [Contains]
        public string? DownstreamPathTemplate { get; set; }
        /// <summary>
        /// 服务名称(服务发现)
        /// </summary>
        [Contains]
        public string? ServiceName { get; set; }
        /// <summary>
        /// Swagger标识
        /// </summary>
        [Equal]
        public string? SwaggerKey { get; set; }
        /// <summary>
        /// 启用缓存
        /// </summary>
        public bool? EnableCache { get; set; }
    }
}
