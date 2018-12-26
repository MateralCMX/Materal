using System.Diagnostics;
using System.Threading.Tasks;

namespace Materal.WindowsHelper
{
    public static class CmdManager
    {
        /// <summary>
        /// 运行命令提示符命令
        /// </summary>
        /// <param name="commands">命令组</param>
        /// <returns>输出结果</returns>
        public static async Task<string> RunCmdCommandsAsync(params string[] commands)
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
            string output;
            using (var process = new Process{StartInfo = processStartInfo })
            {
                process.Start();
                foreach (string command in commands)
                {
                    await process.StandardInput.WriteLineAsync(command);
                }
                await process.StandardInput.WriteLineAsync("exit");
                process.StandardInput.AutoFlush = true;
                output = await process.StandardOutput.ReadToEndAsync();
                process.WaitForExit();
                process.Close();
            }
            return output;
        }
    }
}
