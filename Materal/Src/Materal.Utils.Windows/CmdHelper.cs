namespace Materal.Utils.Windows
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
            if (OutputDataReceived is not null)
            {
                process.OutputDataReceived += OutputDataReceived;
            }
            if (ErrorDataReceived is not null)
            {
                process.ErrorDataReceived += ErrorDataReceived;
            }
            if (process.Start())
            {
                if (OutputDataReceived is not null)
                {
                    process.BeginOutputReadLine();
                }
                if (ErrorDataReceived is not null)
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
            process.WaitForExit();
            process.Close();
        }
    }
}
