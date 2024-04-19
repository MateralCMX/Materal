using Materal.Gateway.OcelotExtension.RequestMonitor.Model;

namespace Materal.Gateway.OcelotExtension.RequestMonitor
{
    /// <summary>
    /// 默认请求监控处理器
    /// </summary>
    public class DefaultRequestMonitorHandler : IRequestMonitorHandler
    {
        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="model"></param>
        public virtual void HandlerRequst(HandlerRequestModel model) { }
        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual Task HandlerRequstAsync(HandlerRequestModel model)
        {
            HandlerRequst(model);
            return Task.CompletedTask;
        }
        /// <summary>
        /// 处理响应
        /// </summary>
        /// <param name="model"></param>
        public virtual void HandlerResponse(HandlerResponseModel model) { }
        /// <summary>
        /// 处理响应
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual Task HandlerResponseAsync(HandlerResponseModel model)
        {
            HandlerResponse(model);
            return Task.CompletedTask;
        }
    }
}
