using Deploy.Common;
using Deploy.Enums;
using Materal.WindowsHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Deploy.ServiceImpl.Models
{
    public class ExeApplicationHandler : ApplicationHandler
    {
        public override void StartApplication(ApplicationRuntimeModel applicationRuntime)
        {
            string exePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Application", applicationRuntime.Path, $"{applicationRuntime.MainModule}");
            StartApplication(applicationRuntime, exePath, applicationRuntime.RunParams);
        }

        public override void StopApplication(ApplicationRuntimeModel applicationRuntime)
        {
            StopApplication(applicationRuntime, ApplicationTypeEnum.Exe);
        }
        /// <summary>
        /// 开始一个应用程序
        /// </summary>
        /// <param name="applicationRuntime"></param>
        /// <param name="exePath"></param>
        /// <param name="runParams"></param>
        public virtual void StartApplication(ApplicationRuntimeModel applicationRuntime, string exePath, string runParams)
        {
            if (!exePath.EndsWith(".exe", StringComparison.OrdinalIgnoreCase)) throw new DeployException("主模块必须以.exe结尾");
            if (applicationRuntime.Status != ApplicationStatusEnum.Stop) throw new DeployException("应用程序尚未停止");
            applicationRuntime.Status = ApplicationStatusEnum.ReadyRun;
            try
            {
                ProcessStartInfo processStartInfo = ProcessManager.GetProcessStartInfo(exePath, !string.IsNullOrWhiteSpace(runParams) ? runParams : "");
                BindProcess = new Process { StartInfo = processStartInfo };
                ConsoleMessage = new List<string>();
                void DataHandler(object sender, DataReceivedEventArgs e)
                {
                    if (string.IsNullOrWhiteSpace(e.Data)) return;
                    ConsoleMessage.Add(e.Data);
                }
                BindProcess.OutputDataReceived += DataHandler;
                BindProcess.ErrorDataReceived += DataHandler;
                if (BindProcess.Start())
                {
                    BindProcess.BeginOutputReadLine();
                    BindProcess.BeginErrorReadLine();
                }
                else
                {
                    throw new DeployException("启动失败");
                }
                applicationRuntime.Status = ApplicationStatusEnum.Running;
                ClearConsoleMessageTimer.Start();
            }
            catch (Exception)
            {
                applicationRuntime.Status = ApplicationStatusEnum.Stop;
                throw;
            }
        }
    }
}
