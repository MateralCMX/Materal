using Materal.Common;
using RC.Core.Common;
using RC.Deploy.Enums;
using RC.Deploy.ServiceImpl.Models;
using System.Diagnostics;

namespace RC.Deploy.ServiceImpl.ApplicationHandlers
{
    /// <summary>
    /// Exe应用程序处理器
    /// </summary>
    public class ExeApplicationHandler : ApplicationHandler
    {
        /// <summary>
        /// 启动应用程序
        /// </summary>
        /// <param name="applicationRuntime"></param>
        public override void StartApplication(ApplicationRuntimeModel applicationRuntime)
        {
            string exePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Application", applicationRuntime.ApplicationInfo.RootPath, $"{applicationRuntime.ApplicationInfo.MainModule}");
            StartApplication(applicationRuntime, exePath, applicationRuntime.ApplicationInfo.RunParams);
        }
        /// <summary>
        /// 停止应用程序
        /// </summary>
        /// <param name="applicationRuntime"></param>
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
        public virtual void StartApplication(ApplicationRuntimeModel applicationRuntime, string exePath, string? runParams)
        {
            if (!exePath.EndsWith(".exe", StringComparison.OrdinalIgnoreCase)) throw new RCException("主模块必须以.exe结尾");
            if (applicationRuntime.ApplicationStatus != ApplicationStatusEnum.Stop) throw new RCException("应用程序尚未停止");
            applicationRuntime.ApplicationStatus = ApplicationStatusEnum.ReadyRun;
            applicationRuntime.ClearConsoleMessage();
            applicationRuntime.AddConsoleMessage($"{applicationRuntime.ApplicationInfo.Name}准备启动....");
            try
            {
                ProcessStartInfo processStartInfo = GetProcessStartInfo(applicationRuntime, exePath, !string.IsNullOrWhiteSpace(runParams) ? runParams : "");
                BindProcess = new Process { StartInfo = processStartInfo };
                void DataHandler(object sender, DataReceivedEventArgs e)
                {
                    if (string.IsNullOrWhiteSpace(e.Data)) return;
                    applicationRuntime.AddConsoleMessage(e.Data);
                }
                BindProcess.OutputDataReceived += DataHandler;
                BindProcess.ErrorDataReceived += DataHandler;
                applicationRuntime.AddConsoleMessage($"{applicationRuntime.ApplicationInfo.Name}开始启动");
                if (BindProcess.Start())
                {
                    applicationRuntime.AddConsoleMessage($"{applicationRuntime.ApplicationInfo.Name}启动完毕");
                    BindProcess.BeginOutputReadLine();
                    BindProcess.BeginErrorReadLine();
                }
                else
                {
                    throw new RCException("启动失败");
                }
                applicationRuntime.ApplicationStatus = ApplicationStatusEnum.Runing;
            }
            catch (Exception ex)
            {
                applicationRuntime.ApplicationStatus = ApplicationStatusEnum.Stop;
                applicationRuntime.AddConsoleMessage(ex.GetErrorMessage());
            }
        }
        private static ProcessStartInfo GetProcessStartInfo(ApplicationRuntimeModel applicationRuntime, string processPath, string arg)
        {
            string workingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Application", applicationRuntime.ApplicationInfo.RootPath);
            ProcessStartInfo processStartInfo = new()
            {
                FileName = processPath,
                UseShellExecute = false,
                CreateNoWindow = false,
                WindowStyle = ProcessWindowStyle.Minimized,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                Arguments = arg,
                WorkingDirectory = workingDirectory
            };
            return processStartInfo;
        }
    }
}
