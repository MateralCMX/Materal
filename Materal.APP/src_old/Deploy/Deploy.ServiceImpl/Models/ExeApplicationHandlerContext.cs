using Deploy.Common;
using Deploy.Enums;
using Materal.WindowsHelper;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Deploy.ServiceImpl.Models
{
    public class ExeApplicationHandlerContext : ApplicationHandlerContext
    {
        public override Process GetProcess(ApplicationRuntimeModel model)
        {
            if (model.ApplicationInfo.ApplicationType == ApplicationTypeEnum.Exe)
            {
                Task<Process> task = GetRunProcessAsync(model);
                Task.WaitAll(task);
                return task.Result;
            }
            if (_next != null)
            {
                return _next.GetProcess(model);
            }
            throw new DeployException("未识别应用程序类型");
        }

        public override void KillProcess(ApplicationRuntimeModel model)
        {
            if (model.ApplicationInfo.ApplicationType == ApplicationTypeEnum.Exe)
            {
                string fileName = Path.GetFileName(model.ApplicationInfo.MainModule);
                Process[] processes = Process.GetProcessesByName(fileName);
                foreach (Process process in processes)
                {
                    KillProcess(process);
                }
                return;
            }
            if (_next == null) throw new DeployException("未识别应用程序类型");
            _next.KillProcess(model);
        }
        public override void KillProcess(ApplicationRuntimeModel model, Process process)
        {
            if (model.ApplicationInfo.ApplicationType == ApplicationTypeEnum.Exe)
            {
                KillProcess(model);
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
        private IEnumerable<string> GetCommands(ApplicationInfoModel model)
        {
            var result = new List<string>
            {
#if DEBUG
                $"cd \"bin\\Debug\\netcoreapp3.1\\Application\\{model.Path}\"",
#else
                $"cd \"Application\\{model.Path}\"",
#endif
                $"{model.MainModule} {model.RunParams}"
            };
            return result;
        }
        /// <summary>
        /// 获得启动程序
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<Process> GetRunProcessAsync(ApplicationRuntimeModel model)
        {
            if (model.ApplicationStatus != ApplicationStatusEnum.Stop) throw new DeployException("应用程序尚未停止");
            model.ApplicationStatus = ApplicationStatusEnum.ReadyRun;
            ProcessStartInfo processStartInfo = ProcessManager.GetProcessStartInfo("cmd.exe", string.Empty);
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
                IEnumerable<string> commands = GetCommands(model.ApplicationInfo);
                foreach (string command in commands)
                {
                    await result.StandardInput.WriteLineAsync(command);
                }
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
