using SignalRSwaggerGen.Attributes;

namespace RC.Deploy.Application.Hubs
{
    /// <summary>
    /// 控制台消息Hub
    /// </summary>
    [SignalRHub("/hubs/ConsoleMessage")]
    public interface IConsoleMessageHub
    {
        /// <summary>
        /// 新控制台消息事件
        /// </summary>
        /// <param name="applicationID">应用程序唯一标识</param>
        /// <param name="message">消息</param>
        /// <returns></returns>
        Task NewConsoleMessageEvent(Guid applicationID, string message);
        /// <summary>
        /// 清空控制台消息事件
        /// </summary>
        /// <param name="applicationID">应用程序唯一标识</param>
        /// <returns></returns>
        Task ClearConsoleMessageEvent(Guid applicationID);
    }
}
