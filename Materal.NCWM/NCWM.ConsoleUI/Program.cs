using NCWM.Manager;
using NCWM.Model;
using System;
using System.Threading.Tasks;

namespace NCWM.ConsoleUI
{
    public class Program
    {
        private static NCWMService _manager;
        public static void Main(string[] args)
        {
            var config = new ConfigModel
            {
                DotNetCoreVersion = 2.2f,
                Name = "日志WebAPI",
                Path = @"E:\Project\IntegratedPlatform\Project\IntegratedPlatform\Log.WebAPI\bin\Debug\netcoreapp2.2",
                TargetName = "Log.WebAPI",
                Arguments = "--ConfigTarget Development --urls=http://*:8800"
            };
            _manager = new NCWMService(config);
            Task.Run(RunAsync);
            string inputString;
            do
            {
                inputString = Console.ReadLine();
                switch (inputString)
                {
                    case "Stop":
                        Task.Run(StopAsync);
                        break;
                }
            } while (inputString != "Exit");
        }

        public static async Task RunAsync()
        {
            try
            {
                _manager.OutputDataReceived += Manager_OutputDataReceived;
                _manager.ErrorDataReceived += Manager_ErrorDataReceived;
                await _manager.StartAsync();
            }
            catch (InvalidOperationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public static async Task StopAsync()
        {
            try
            {
                await _manager.StopAsync();
            }
            catch (InvalidOperationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private static void Manager_ErrorDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.Data);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void Manager_OutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }
    }
}
