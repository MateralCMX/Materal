using System;
using Materal.FileHelper;
using System.Drawing;
using System.Threading.Tasks;
using Materal.ConvertHelper;
using Materal.WindowsHelper;

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
                    " -i \"E:\\Project\\IntegratedPlatform\\Temp\\UploadFiles\\20190814\\9d6f9a056875456983ec1d90d7dceb6aQQ.mp4\" -movflags empty_moov+default_base_moof+frag_keyframe \"E:\\Project\\IntegratedPlatform\\Temp\\UploadFiles\\20190814\\9d6f9a056875456983ec1d90d7dceb6aQQ-Fragmented.mp4\"";
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
