using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NCWM.Model;

namespace NCWM.Manager
{
    public class NCWMService
    {
        public event DataReceivedEventHandler OutputDataReceived;
        public event DataReceivedEventHandler ErrorDataReceived;
        private Process _process;
        private readonly ConfigModel _config;
        public NCWMService(ConfigModel config)
        {
            _config = config;
        }
        /// <summary>
        /// 开始
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync()
        {
            ProcessStartInfo processStartInfo = GetProcessStartInfo();
            _process = new Process { StartInfo = processStartInfo };
            if (OutputDataReceived != null) _process.OutputDataReceived += OutputDataReceived;
            if (ErrorDataReceived != null) _process.ErrorDataReceived += ErrorDataReceived;
            if (_process.Start())
            {
                if (OutputDataReceived != null) _process.BeginOutputReadLine();
                if (ErrorDataReceived != null) _process.BeginErrorReadLine();
            }
            else
            {
                throw new InvalidOperationException("启动失败");
            }
            await _process.StandardInput.WriteLineAsync(_config.CmdCommand);
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        public async Task StopAsync()
        {
            KillProgram();
            await _process.StandardInput.WriteLineAsync("exit");
            _process.WaitForExit();
            _process.Close();
            _process.Dispose();
        }
        /// <summary>
        /// 杀死程序
        /// </summary>
        private void KillProgram()
        {
            Process[] processes = Process.GetProcessesByName("dotnet");
            Process currentProcess = Process.GetCurrentProcess();
            foreach (Process process in processes)
            {
                if (currentProcess.Id == process.Id) continue;
                var isKill = false;
                foreach (ProcessModule processModule in process.Modules)
                {
                    if (processModule.ModuleName != _config.MainModuleName) continue;
                    process.Kill();
                    isKill = true;
                    break;
                }
                if (isKill)
                {
                    break;
                }
            }
        }
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
    }
}
