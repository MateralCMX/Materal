using Deploy.Common;
using Deploy.Enums;
using Materal.WindowsHelper;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Deploy.ServiceImpl.Models
{
    public class DotNetCoreApplicationHandlerContext : ApplicationHandlerContext
    {
        public override Process GetProcess(ApplicationRuntimeModel model)
        {
            if (model.ApplicationInfo.ApplicationType == ApplicationTypeEnum.DotNetCore)
            {
                return GetRunProcess(model);
            }
            if (_next != null)
            {
                return _next.GetProcess(model);
            }
            throw new DeployException("未识别应用程序类型");
        }

        public override void KillProcess(ApplicationRuntimeModel model)
        {
            if (model.ApplicationInfo.ApplicationType == ApplicationTypeEnum.DotNetCore)
            {
                Process[] processes = Process.GetProcessesByName("dotnet");
                var currentProcess = Process.GetCurrentProcess();
                foreach (Process process in processes)
                {
                    if (currentProcess.Id == process.Id) return;
                    if (process.Modules.Cast<ProcessModule>().Any(processModule => processModule.ModuleName == model.ApplicationInfo.MainModule))
                    {
                        KillProcess(process);
                    }
                }
                return;
            }
            if (_next == null) throw new DeployException("未识别应用程序类型");
            _next.KillProcess(model);
        }

        public override void KillProcess(ApplicationRuntimeModel model, Process process)
        {
            if (model.ApplicationInfo.ApplicationType == ApplicationTypeEnum.DotNetCore)
            {
                KillProcess(process);
                return;
            }
            if (_next == null) throw new DeployException("未识别应用程序类型");
            _next.KillProcess(model, process);
        }

        #region 私有方法
        /// <summary>
        /// 获得启动参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string GetStartArgs(ApplicationInfoModel model)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Application", model.Path, $"{model.MainModule}");
            string result = string.IsNullOrEmpty(model.RunParams) ? $"{path}" : $"{path} {model.RunParams}";
            return result;
        }
        /// <summary>
        /// 获得启动程序
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private Process GetRunProcess(ApplicationRuntimeModel model)
        {
            if (model.ApplicationStatus != ApplicationStatusEnum.Stop) throw new DeployException("应用程序尚未停止");
            model.ApplicationStatus = ApplicationStatusEnum.ReadyRun;
            string startArgs = GetStartArgs(model.ApplicationInfo);
            ProcessStartInfo processStartInfo = ProcessManager.GetProcessStartInfo("dotnet.exe", !string.IsNullOrWhiteSpace(startArgs) ? startArgs : "");
            var result = new Process { StartInfo = processStartInfo };
            void DataHandler(object sender, DataReceivedEventArgs e)
            {
                model.ConsoleMessage.Add(e.Data);
            }
            result.OutputDataReceived += DataHandler;
            result.ErrorDataReceived += DataHandler;
            if (result.Start())
            {
                result.BeginOutputReadLine();
                result.BeginErrorReadLine();
            }
            else
            {
                model.ApplicationStatus = ApplicationStatusEnum.Error;
                throw new DeployException("启动失败");
            }
            model.ApplicationStatus = ApplicationStatusEnum.Runing;
            model.ClearConsoleMessageTimer.Start();
            return result;
        }
        #endregion
    }
}
