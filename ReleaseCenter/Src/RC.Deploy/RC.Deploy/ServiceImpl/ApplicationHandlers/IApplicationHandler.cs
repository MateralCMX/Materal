using RC.Deploy.ServiceImpl.Models;
using System.Diagnostics;

namespace RC.Deploy.ServiceImpl.ApplicationHandlers
{
    /// <summary>
    /// 应用程序处理器
    /// </summary>
    public interface IApplicationHandler
    {
        /// <summary>
        /// 进程
        /// </summary>
        public Process? BindProcess { get; }
        /// <summary>
        /// 启动应用程序
        /// </summary>
        /// <param name="applicationRuntime"></param>
        /// <returns></returns>
        public void StartApplication(ApplicationRuntimeModel applicationRuntime);
        /// <summary>
        /// 启动应用程序
        /// </summary>
        /// <param name="applicationRuntime"></param>
        /// <returns></returns>
        public void StopApplication(ApplicationRuntimeModel applicationRuntime);
    }
}
