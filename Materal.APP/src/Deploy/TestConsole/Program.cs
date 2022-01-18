using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace TestConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ProcessStartInfo startInfo = GetProcessStartInfo(@"E:\Project\Materal\Application\Materal.APP\TestApplication\DotNetTestApplication\DotNetTestApplication.exe", string.Empty);
            Process process;
            void DataHandler(object sender, DataReceivedEventArgs e)
            {
                if (string.IsNullOrWhiteSpace(e.Data)) return;
                Console.WriteLine($"Child:{e.Data}");
            }
            if (startInfo.UseShellExecute)
            {
                process = new Process
                {
                    StartInfo = startInfo
                };
            }
            else
            {
                startInfo.RedirectStandardInput = true;
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                process = new Process
                {
                    StartInfo = startInfo
                };
                process.OutputDataReceived += DataHandler;
                process.ErrorDataReceived += DataHandler;
            }
            if (process.Start())
            {
                Console.WriteLine($"PID:{process.Id}");
                if (!startInfo.UseShellExecute)
                {
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                }
                Thread.Sleep(2000);
                if (process.CloseMainWindow())
                {
                    Console.WriteLine("成功关闭");
                }
                else
                {
                    Console.WriteLine("关闭失败,杀死进程");
                    process.Kill();
                }
                process.WaitForExit();
                process.Close();
                process.Dispose();
            }
        }
        private static ProcessStartInfo GetProcessStartInfo(string processPath, string arg)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = processPath,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Normal,
                UseShellExecute = true,
                Arguments = arg,
            };
            return processStartInfo;
        }
    }
}
