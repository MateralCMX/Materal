using Materal.Utils.Model;

namespace Materal.Gateway.WebAPI.PresentationModel.Route
{
    /// <summary>
    /// 查询路由请求模型
    /// </summary>
    public class QueryRouteRequestModel : FilterModel
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
        [Contains]
        public string? SwaggerKey { get; set; }
        /// <summary>
        /// 启用缓存
        /// </summary>
        public bool? EnableCache { get; set; }
    }
}
