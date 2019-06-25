using System.Diagnostics;
using System.Threading.Tasks;

namespace Materal.WindowsHelper
{
    public class CmdManager
    {
        public event DataReceivedEventHandler OutputDataReceived;

        public event DataReceivedEventHandler ErrorDataReceived;
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
        public async Task<string> RunCmdCommandsAsync(params string[] commands)
        {
            ProcessStartInfo processStartInfo = GetProcessStartInfo();
            string output;
            using (var process = new Process { StartInfo = processStartInfo })
            {
                process.Start();
                if (OutputDataReceived != null)
                {
                    process.OutputDataReceived += OutputDataReceived;
                }

                if (ErrorDataReceived != null)
                {
                    process.OutputDataReceived += ErrorDataReceived;
                }
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
