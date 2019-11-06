using System.Diagnostics;
using System.Threading.Tasks;

namespace Materal.WindowsHelper
{
    public class ProcessManager
    {
        public event DataReceivedEventHandler OutputDataReceived;

        public event DataReceivedEventHandler ErrorDataReceived;
        public static ProcessStartInfo GetProcessStartInfo(string cmd, string arg)
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = cmd,
                CreateNoWindow = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                Verb = "RunAs",
                Arguments = arg
            };
            return processStartInfo;
        }
        public async Task<string> ProcessStartAsync(string cmd, string arg)
        {
            ProcessStartInfo processStartInfo = GetProcessStartInfo(cmd, arg);
            string output;
            using (var process = new Process { StartInfo = processStartInfo })
            {
                if (OutputDataReceived != null)
                {
                    process.OutputDataReceived += OutputDataReceived;
                }
                if (ErrorDataReceived != null)
                {
                    process.OutputDataReceived += ErrorDataReceived;
                }
                process.Start();
                process.BeginErrorReadLine();
                process.StandardInput.AutoFlush = true;
                output = await process.StandardOutput.ReadToEndAsync();
                process.WaitForExit();
                process.Close();
            }
            return output;
        }
    }
}
