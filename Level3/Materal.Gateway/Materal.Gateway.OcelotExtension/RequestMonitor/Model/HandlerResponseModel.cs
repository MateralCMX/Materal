using Ocelot.Configuration;
using Ocelot.Middleware;
using Ocelot.Request.Middleware;

namespace Materal.Gateway.OcelotExtension.RequestMonitor.Model
{
    /// <summary>
    /// 处理请求模型
    /// </summary>
    public class HandlerResponseModel
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
        /// <summary>
        /// 响应
        /// </summary>
        public DownstreamResponse Response { get; set; }

        public HandlerResponseModel(Guid id, DownstreamRoute route, DownstreamRequest request, DownstreamResponse response)
        {
            ID = id;
            Route = route;
            Request = request;
            Response = response;
        }
    }
}
