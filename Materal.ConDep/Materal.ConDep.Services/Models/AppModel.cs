using Materal.ConDep.Services.Enums;
using Materal.WindowsHelper;
using System;
using System.Diagnostics;
using System.IO;

namespace Materal.ConDep.Services.Models
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
                string result = string.IsNullOrEmpty(Parameters) ? $"{path}" : $"{path} {Parameters}";
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
                ProcessStartInfo processStartInfo = ProcessManager.GetProcessStartInfo("dotnet.exe", CmdCommand);
                _process = new Process { StartInfo = processStartInfo };
                if (OutputDataReceived != null) _process.OutputDataReceived += ProcessOutputDataReceived;
                if (ErrorDataReceived != null) _process.ErrorDataReceived += ProcessErrorDataReceived;
                if (_process.Start())
                {
                    if (OutputDataReceived != null) _process.BeginOutputReadLine();
                    if (ErrorDataReceived != null) _process.BeginErrorReadLine();
                }
                else
                {
                    throw new InvalidOperationException("启动失败");
                }
                AppStatus = AppStatusEnum.Start;
            }
        }

        private void ProcessErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            ErrorDataReceived?.Invoke(sender, e, this);
        }

        private void ProcessOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            OutputDataReceived?.Invoke(sender, e, this);
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            if (AppStatus != AppStatusEnum.Start && AppStatus != AppStatusEnum.Error) return;
            lock (_changeStatusLock)
            {
                if (AppStatus != AppStatusEnum.Start && AppStatus != AppStatusEnum.Error) return;
                AppStatus = AppStatusEnum.Stopping;
                try
                {
                    _process.Kill();
                    _process.WaitForExit();
                    _process.Dispose();
                    AppStatus = AppStatusEnum.Stop;
                }
                catch (Exception)
                {
                    AppStatus = AppStatusEnum.Error;
                }
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
    }
}
