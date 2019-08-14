using Materal.WindowsHelper;
using System;
using System.Threading.Tasks;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        public static void Main()
        {
            Task task = Task.Run(async () =>
            {
                var cmd = "\"D:\\Program Files\\ffmpeg-4.1.4-win64-shared\\bin\\ffmpeg.exe\"";
                var arg =
                    " -i \"E:\\Project\\IntegratedPlatform\\Temp\\UploadFiles\\20190814\\9d6f9a056875456983ec1d90d7dceb6aQQ.mp4\" -y -f image2 -ss 0 -t 0.001 \"E:\\Project\\IntegratedPlatform\\Temp\\UploadFiles\\20190814\\9d6f9a056875456983ec1d90d7dceb6aQQ-Thumbnail.jpg_temp\"";
                var processManager = new ProcessManager();
                processManager.ErrorDataReceived += CmdManager_ErrorDataReceived;
                processManager.OutputDataReceived += CmdManager_OutputDataReceived;
                string result = await processManager.ProcessStartAsync(cmd, arg);
                Console.WriteLine(result);
            });
            Task.WaitAll(task);
        }

        private static void CmdManager_OutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }

        private static void CmdManager_ErrorDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }
    }
}
