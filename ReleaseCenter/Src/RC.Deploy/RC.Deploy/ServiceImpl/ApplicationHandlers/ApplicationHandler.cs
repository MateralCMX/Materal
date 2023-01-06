using Materal.Common;
using RC.Core.Common;
using RC.Deploy.Enums;
using RC.Deploy.ServiceImpl.Models;
using System.Diagnostics;

namespace RC.Deploy.ServiceImpl.ApplicationHandlers
{
    /// <summary>
    /// 应用程序处理器
    /// </summary>
    public abstract class ApplicationHandler : IApplicationHandler
    {
        /// <summary>
        /// 绑定进程
        /// </summary>
        public Process? BindProcess { get; protected set; }
        /// <summary>
        /// 启动应用程序
        /// </summary>
        /// <param name="applicationRuntime"></param>
        public abstract void StartApplication(ApplicationRuntimeModel applicationRuntime);
        /// <summary>
        /// 停止应用程序
        /// </summary>
        /// <param name="applicationRuntime"></param>
        public abstract void StopApplication(ApplicationRuntimeModel applicationRuntime);
        /// <summary>
        /// 停止应用程序
        /// </summary>
        /// <param name="applicationRuntime"></param>
        /// <param name="applicationType"></param>
        protected virtual void StopApplication(ApplicationRuntimeModel applicationRuntime, ApplicationTypeEnum applicationType)
        {
            if (applicationRuntime.ApplicationInfo.ApplicationType != applicationType) throw new RCException("处理器类型错误");
            applicationRuntime.ConsoleMessages.Add($"{applicationRuntime.ApplicationInfo.Name}准备停止");
            applicationRuntime.ApplicationStatus = ApplicationStatusEnum.Stoping;
            try
            {
                CloseProcess(applicationRuntime);
                applicationRuntime.ApplicationStatus = ApplicationStatusEnum.Stop;
                applicationRuntime.ConsoleMessages.Add($"{applicationRuntime.ApplicationInfo.Name}已停止");
            }
            catch (Exception ex)
            {
                applicationRuntime.ApplicationStatus = ApplicationStatusEnum.Runing;
                applicationRuntime.ConsoleMessages.Add(ex.GetErrorMessage());
            }
        }
        /// <summary>
        /// 关闭进程
        /// </summary>
        /// <param name="applicationRuntime"></param>
        protected virtual void CloseProcess(ApplicationRuntimeModel applicationRuntime)
        {
            if (BindProcess == null) return;
            if (!BindProcess.CloseMainWindow())
            {
                BindProcess.Kill();
            }
            BindProcess.WaitForExit();
            BindProcess.Close();
            BindProcess.Dispose();
        }
    }
}
