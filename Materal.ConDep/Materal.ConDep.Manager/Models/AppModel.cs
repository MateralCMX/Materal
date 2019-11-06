using Materal.ConDep.Manager.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using Materal.ConDep.Common;

namespace Materal.ConDep.Manager.Models
{
    public class AppModel
    {
        private readonly ILogger<AppModel> _logger;
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

        public AppModel()
        {
            _logger = ApplicationData.GetService<ILogger<AppModel>>();
        }

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
            if (AppStatus != AppStatusEnum.Start && AppStatus != AppStatusEnum.Error) return;
            lock (_changeStatusLock)
            {
                if (AppStatus != AppStatusEnum.Start && AppStatus != AppStatusEnum.Error) return;
                AppStatus = AppStatusEnum.Stopping;
                try
                {
                    //_process.StandardInput.WriteLine("exit");
                    //_process.WaitForExit();
                    //_process.Close();
                    _process.Kill();
                    _process.Dispose();
                    AppStatus = AppStatusEnum.Stop;
                }
                catch (Exception ex)
                {
                    AppStatus = AppStatusEnum.Error;
                    _logger.LogError(ex.Message);
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
        #endregion
    }
}
