﻿using Materal.Common;
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
        /// 杀死应用程序
        /// </summary>
        /// <param name="applicationRuntime"></param>
        public virtual void KillApplication(ApplicationRuntimeModel applicationRuntime)
        {
            if (applicationRuntime.ApplicationStatus == ApplicationStatusEnum.Stop) throw new RCException("应用程序尚未启动");
            KillProcess();
            applicationRuntime.ApplicationStatus = ApplicationStatusEnum.Stop;
            applicationRuntime.AddConsoleMessage($"{applicationRuntime.ApplicationInfo.Name}已强制停止");
        }
        /// <summary>
        /// 停止应用程序
        /// </summary>
        /// <param name="applicationRuntime"></param>
        /// <param name="applicationType"></param>
        protected virtual void StopApplication(ApplicationRuntimeModel applicationRuntime, ApplicationTypeEnum applicationType)
        {
            if (applicationRuntime.ApplicationInfo.ApplicationType != applicationType) throw new RCException("处理器类型错误");
            applicationRuntime.AddConsoleMessage($"{applicationRuntime.ApplicationInfo.Name}准备停止");
            applicationRuntime.ApplicationStatus = ApplicationStatusEnum.Stoping;
            try
            {
                CloseProcess(applicationRuntime);
                applicationRuntime.ApplicationStatus = ApplicationStatusEnum.Stop;
                applicationRuntime.AddConsoleMessage($"{applicationRuntime.ApplicationInfo.Name}已停止");
            }
            catch (Exception ex)
            {
                applicationRuntime.ApplicationStatus = ApplicationStatusEnum.Runing;
                applicationRuntime.AddConsoleMessage(ex.GetErrorMessage());
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
            BindProcess = null;
        }
        /// <summary>
        /// 杀死进程
        /// </summary>
        protected virtual void KillProcess()
        {
            if (BindProcess == null) return;
            BindProcess.Kill();
            BindProcess.WaitForExit();
            BindProcess.Close();
            BindProcess.Dispose();
            BindProcess = null;
        }
    }
}
