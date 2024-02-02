using RC.Deploy.Application.Services.Models;
using System.Diagnostics;

namespace RC.Deploy.Application.Services.ApplicationHandlers
{
    /// <summary>
    /// 应用程序处理器
    /// </summary>
    public interface IApplicationHandler
    {
        /// <summary>
        /// 进程
        /// </summary>
        Process? BindProcess { get; }
        /// <summary>
        /// 启动应用程序
        /// </summary>
        /// <param name="applicationRuntime"></param>
        /// <returns></returns>
        Task StartApplicationAsync(ApplicationRuntimeModel applicationRuntime);
        /// <summary>
        /// 关闭应用程序
        /// </summary>
        /// <param name="applicationRuntime"></param>
        /// <returns></returns>
        Task StopApplicationAsync(ApplicationRuntimeModel applicationRuntime);
        /// <summary>
        /// 杀死应用程序
        /// </summary>
        /// <param name="applicationRuntime"></param>
        /// <returns></returns>
        Task KillApplicationAsync(ApplicationRuntimeModel applicationRuntime);
    }
}
