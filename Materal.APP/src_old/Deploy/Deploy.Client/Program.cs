using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Materal.WindowsHelper;

namespace Deploy.Client
{
    public class Program
    {
        public static async Task Main()
        {
            const string path = @"E:\Project\Materal\Application\Materal.APP\Deploy.Server\Application\template\Start.cmd";
            var cmdManager = new CmdManager();
            cmdManager.OutputDataReceived += CmdManager_OutputDataReceived;
            cmdManager.ErrorDataReceived += CmdManager_ErrorDataReceived;
            //await cmdManager.RunCmdCommandsAsync("E:", @"cd Project\Materal\Application\Materal.APP\Deploy.Server\Application\template", "npm run deploy");
            await cmdManager.RunCmdCommandsAsync("E:", @"cd Project\Materal\Application\Materal.APP\Deploy.Server\Application\template", "echo Hello World");
            await cmdManager.RunCmdCommandsAsync("echo Hello World");
            Console.ReadKey();
        }

        private static void CmdManager_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }

        private static void CmdManager_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }
    }
}
