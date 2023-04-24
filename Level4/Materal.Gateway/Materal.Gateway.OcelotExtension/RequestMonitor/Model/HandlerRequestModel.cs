using Ocelot.Configuration;
using Ocelot.Request.Middleware;

namespace Materal.Gateway.OcelotExtension.RequestMonitor.Model
{
    /// <summary>
    /// 处理请求模型
    /// </summary>
    public class HandlerRequestModel
    {
        /// <summary>
        /// 连接唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 路由
        /// </summary>
        public DownstreamRoute Route { get; set; }
        /// <summary>
        /// 请求
        /// </summary>
        public DownstreamRequest Request { get; set; }

        public HandlerRequestModel(Guid id, DownstreamRoute route, DownstreamRequest request)
        {
            ID = id;
            Route = route;
            Request = request;
        }
    }
}
