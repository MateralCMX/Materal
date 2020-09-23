using Deploy.Common;
using Deploy.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;

namespace Deploy.ServiceImpl.Models
{
    public class ApplicationRuntimeModel
    {
        private readonly ApplicationHandlerContext _applicationHandlerContext;
        public Timer ClearConsoleMessageTimer { get; } = new Timer(10000);
        public ApplicationInfoModel ApplicationInfo { get; }

        public ApplicationStatusEnum ApplicationStatus
        {
            get => ApplicationInfo.ApplicationStatus;
            set => ApplicationInfo.ApplicationStatus = value;
        }

        public ApplicationRuntimeModel(ApplicationInfoModel applicationInfo, ApplicationHandlerContext applicationHandlerContext)
        {
            _applicationHandlerContext = applicationHandlerContext;
            ApplicationInfo = applicationInfo;
            ClearConsoleMessageTimer.Elapsed += ClearConsoleMessageTimer_Elapsed;
        }
        /// <summary>
        /// 控制台消息
        /// </summary>
        public ICollection<string> ConsoleMessage { get; private set; }
        private Process _process;
        private readonly object _changeStatusLock = new object();
        /// <summary>
        /// 开始
        /// </summary>
        /// <returns></returns>
        public void Start()
        {
            if (ApplicationStatus != ApplicationStatusEnum.Stop) throw new DeployException("应用程序尚未停止");
            lock (_changeStatusLock)
            {
                ConsoleMessage = new List<string>();
                _process = _applicationHandlerContext.GetProcess(this);
            }
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        public void Stop()
        {
            if (ApplicationStatus != ApplicationStatusEnum.Runing && ApplicationStatus != ApplicationStatusEnum.Error) return;
            lock (_changeStatusLock)
            {
                if (ApplicationStatus != ApplicationStatusEnum.Runing && ApplicationStatus != ApplicationStatusEnum.Error) return;
                ApplicationStatus = ApplicationStatusEnum.Stoping;
                try
                {
                    _applicationHandlerContext.KillProcess(this, _process);
                    ApplicationStatus = ApplicationStatusEnum.Stop;
                }
                catch (Exception)
                {
                    ApplicationStatus = ApplicationStatusEnum.Error;
                }
                ClearConsoleMessageTimer.Stop();
                ConsoleMessage = null;
            }
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
