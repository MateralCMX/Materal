using Materal.Gateway.OcelotExtension.RequestMonitor.Model;

namespace Materal.Gateway.OcelotExtension.RequestMonitor
{
    public interface IRequestMonitorHandlers
    {
        /// <summary>
        /// 添加
        /// </summary>
        void Add<T>() where T : class, IRequestMonitorHandler;
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
    }
}
