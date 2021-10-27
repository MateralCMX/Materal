namespace Materal.Gateway.WebAPI.Services.Models
{
    /// <summary>
    /// 路由模型
    /// </summary>
    public class RouteModel : RouteListModel
    {
        /// <summary>
        /// 缓存时间(s)
        /// </summary>
        public int CacheTimer{ get; set; }
    }
}
