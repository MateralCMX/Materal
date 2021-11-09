using System.Collections.Generic;
using System.Diagnostics;

namespace Deploy.ServiceImpl.Models
{
    public interface IApplicationHandler
    {
        /// <summary>
        /// 进程
        /// </summary>
        public Process BindProcess{ get; }
        /// <summary>
        /// 控制台消息
        /// </summary>
        public ICollection<string> ConsoleMessage { get; }
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
