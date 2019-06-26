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
        private readonly object _runLock = new object();
        private readonly object _commandLock = new object();
        public bool IsRun { get; private set; }
        public NCWMService(ConfigModel config)
        {
            _config = config;
        }
        /// <summary>
        /// 开始
        /// </summary>
        /// <returns></returns>
        public void Start()
        {
            if (IsRun) return;
            lock (_runLock)
            {
                if (IsRun) return;
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
                _process.StandardInput.WriteLine(_config.CmdCommand);
                IsRun = true;
            }
        }
        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="command"></param>
        public void SendCommand(string command)
        {
            if (!IsRun || _process == null) return;
            lock (_commandLock)
            {
                _process.StandardInput.WriteLine(command);
            }
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        public void Stop()
        {
            if (!IsRun) return;
            lock (_runLock)
            {
                if (!IsRun) return;
                KillProgram();
                _process.StandardInput.WriteLine("exit");
                _process.WaitForExit();
                _process.Close();
                _process.Dispose();
                IsRun = false;
            }
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
