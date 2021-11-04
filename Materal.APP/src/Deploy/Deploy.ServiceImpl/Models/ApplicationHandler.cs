using Deploy.Common;
using Deploy.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;

namespace Deploy.ServiceImpl.Models
{
    public abstract class ApplicationHandler : IApplicationHandler
    {
        public Process Process { get; protected set; }
        public ICollection<string> ConsoleMessage { get; protected set; }
        protected Timer ClearConsoleMessageTimer { get; }

        protected ApplicationHandler()
        {
            ClearConsoleMessageTimer = new Timer(10000);
            ClearConsoleMessageTimer.Elapsed += ClearConsoleMessageTimer_Elapsed;
        }
        public abstract void StartApplication(ApplicationRuntimeModel applicationRuntime);

        public abstract void StopApplication(ApplicationRuntimeModel applicationRuntime);
        /// <summary>
        /// 停止应用程序
        /// </summary>
        /// <param name="applicationRuntime"></param>
        /// <param name="applicationType"></param>
        protected virtual void StopApplication(ApplicationRuntimeModel applicationRuntime, ApplicationTypeEnum applicationType)
        {
            if (applicationRuntime.ApplicationType != applicationType) throw new DeployException("处理器类型错误");
            applicationRuntime.Status = ApplicationStatusEnum.Stopping;
            try
            {
                KillProcess();
                applicationRuntime.Status = ApplicationStatusEnum.Stop;
            }
            catch (Exception)
            {
                applicationRuntime.Status = ApplicationStatusEnum.Running;
                throw;
            }
        }
        /// <summary>
        /// 杀死程序
        /// </summary>
        protected virtual void KillProcess()
        {
            if (Process == null) return;
            Process.Kill();
            Process.WaitForExit();
            Process.Dispose();
        }
        /// <summary>
        /// 清理控制台定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearConsoleMessageTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ClearConsoleMessageTimer.Stop();
            if (ConsoleMessage.Count > 500)
            {
                ConsoleMessage.Clear();
            }
            ClearConsoleMessageTimer.Start();
        }
    }
}
