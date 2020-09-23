using System.Diagnostics;

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
        public void ProcessStart(string cmd, string arg)
        {
            ProcessStartInfo processStartInfo = GetProcessStartInfo(cmd, arg);
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
                }
                process.StandardInput.AutoFlush = true;
                process.WaitForExit();
                process.Close();
            }
        }
    }
}
