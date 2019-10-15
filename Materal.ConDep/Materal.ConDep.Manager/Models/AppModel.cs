using Materal.ConDep.Manager.Enums;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Materal.ConDep.Manager.Models
{
    public class AppModel
    {
        public event Action<object, DataReceivedEventArgs, AppModel> OutputDataReceived;
        public event Action<object, DataReceivedEventArgs, AppModel> ErrorDataReceived;
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; } = Guid.NewGuid();
        /// <summary>
        /// APP状态
        /// </summary>
        public AppStatusEnum AppStatus { get; protected set; } = AppStatusEnum.Stop;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string AppPath { get; set; }
        /// <summary>
        /// 主模组名称
        /// </summary>
        public string MainModuleName { get; set; }
        /// <summary>
        /// 运行参数
        /// </summary>
        public string Parameters { get; set; }
        /// <summary>
        /// 命令
        /// </summary>
        public string CmdCommand
        {
            get
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Application", AppPath, $"{MainModuleName}");
                string result = string.IsNullOrEmpty(Parameters) ? $"dotnet {path}" : $"dotnet {path} {Parameters}";
                return result;
            }
        }
        private Process _process;
        private readonly object _changeStatusLock = new object();
        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            if (AppStatus != AppStatusEnum.Stop) return;
            lock (_changeStatusLock)
            {
                if (AppStatus != AppStatusEnum.Stop) return;
                AppStatus = AppStatusEnum.Starting;
                ProcessStartInfo processStartInfo = GetProcessStartInfo();
                _process = new Process { StartInfo = processStartInfo };
                if (OutputDataReceived != null) _process.OutputDataReceived += _process_OutputDataReceived;
                if (ErrorDataReceived != null) _process.ErrorDataReceived += _process_ErrorDataReceived;
                if (_process.Start())
                {
                    if (OutputDataReceived != null) _process.BeginOutputReadLine();
                    if (ErrorDataReceived != null) _process.BeginErrorReadLine();
                }
                else
                {
                    throw new InvalidOperationException("启动失败");
                }
                _process.StandardInput.WriteLine(CmdCommand);
                AppStatus = AppStatusEnum.Start;
            }
        }

        private void _process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            ErrorDataReceived?.Invoke(sender, e, this);
        }

        private void _process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            OutputDataReceived?.Invoke(sender, e, this);
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            if (AppStatus != AppStatusEnum.Start) return;
            lock (_changeStatusLock)
            {
                if (AppStatus != AppStatusEnum.Start) return;
                AppStatus = AppStatusEnum.Stopping;
                KillProgram();
                _process.StandardInput.WriteLine("exit");
                _process.WaitForExit();
                _process.Close();
                _process.Dispose();
                AppStatus = AppStatusEnum.Stop;
            }
        }
        /// <summary>
        /// 重启
        /// </summary>
        public void Restart()
        {
            Stop();
            Start();
        }
        #region 私有方法
        /// <summary>
        /// 获取启动信息
        /// </summary>
        /// <returns></returns>
        private ProcessStartInfo GetProcessStartInfo()
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                CreateNoWindow = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                Verb = "RunAs"
            };
            return processStartInfo;
        }
        /// <summary>
        /// 杀死程序
        /// </summary>
        private void KillProgram()
        {
            Process[] processes = Process.GetProcessesByName("dotnet");
            Process currentProcess = Process.GetCurrentProcess();
            Parallel.ForEach(processes, process =>
            {
                if (currentProcess.Id == process.Id) return;
                foreach (ProcessModule processModule in process.Modules)
                {
                    if (processModule.ModuleName != MainModuleName) continue;
                    process.Kill();
                    break;
                }
            });
        }
        #endregion
    }
}
