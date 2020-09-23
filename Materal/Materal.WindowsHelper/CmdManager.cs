using System.Diagnostics;
using System.Threading.Tasks;

namespace Materal.WindowsHelper
{
    public class CmdManager
    {
        public event DataReceivedEventHandler OutputDataReceived;
        public event DataReceivedEventHandler ErrorDataReceived;
        public async Task RunCmdCommandsAsync(params string[] commands)
        {
            ProcessStartInfo processStartInfo = ProcessManager.GetProcessStartInfo("cmd.exe", string.Empty);
            using (var process = new Process { StartInfo = processStartInfo })
            {
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
                process.WaitForExit();
                process.Close();
            }
        }
    }
}
