using System.Diagnostics;

namespace Materal.Tools.Core.Helper
{
    /// <summary>
    /// Cmd管理器
    /// </summary>
    public class CmdHelper
    {
        /// <summary>
        /// 输出数据
        /// </summary>
        public event DataReceivedEventHandler? OutputDataReceived;
        /// <summary>
        /// 错误数据
        /// </summary>
        public event DataReceivedEventHandler? ErrorDataReceived;
        /// <summary>
        /// 运行CMD命令
        /// </summary>
        /// <param name="commands"></param>
        /// <returns></returns>
        public async Task RunCmdCommandsAsync(params string[] commands)
        {
            ProcessStartInfo processStartInfo = ProcessHelper.GetProcessStartInfo("cmd.exe", string.Empty);
            using Process process = new() { StartInfo = processStartInfo };
            if (OutputDataReceived != null)
            {
                process.OutputDataReceived += OutputDataReceived;
            }
            if (ErrorDataReceived != null)
            {
                process.ErrorDataReceived += ErrorDataReceived;
            }
            if (process.Start())
            {
                if (OutputDataReceived != null)
                {
                    process.BeginOutputReadLine();
                }
                if (ErrorDataReceived != null)
                {
                    process.BeginErrorReadLine();
                }
                foreach (string command in commands)
                {
                    await process.StandardInput.WriteLineAsync(command);
                }
            }
            await process.StandardInput.WriteLineAsync("exit");
            process.StandardInput.AutoFlush = true;
#if NET
            await process.WaitForExitAsync();
#else
            process.WaitForExit();
#endif
        }
    }
}
