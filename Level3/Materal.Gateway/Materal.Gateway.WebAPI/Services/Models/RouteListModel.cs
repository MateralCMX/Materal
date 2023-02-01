namespace Materal.Gateway.WebAPI.Services.Models
{
    /// <summary>
    /// 路由列表模型
    /// </summary>
    public class RouteListModel
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName{ get; set; }
        /// <summary>
        /// 是否启用缓存
        /// </summary>
        public bool EnableCache{ get; set; }
    }
}
