using Materal.Gateway.OcelotExtension.RequestMonitor.Model;

namespace Materal.Gateway.OcelotExtension.RequestMonitor
{
    /// <summary>
    /// 请求监控处理器
    /// </summary>
    public interface IRequestMonitorHandler
    {
        /// <summary>
        /// 处理Request
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task HandlerRequstAsync(HandlerRequestModel model);
        /// <summary>
        /// 处理Response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task HandlerResponseAsync(HandlerResponseModel model);
        /// <summary>
        /// 处理Request
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void HandlerRequst(HandlerRequestModel model);
        /// <summary>
        /// 处理Response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void HandlerResponse(HandlerResponseModel model);
    }
}
