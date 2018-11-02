using Materal.FileHelper;
using Materal.WindowsHelper;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        static void Main()
        {
            Task.Run(async () => { await Init(); });
            Console.ReadKey();
        }

        public static async Task Init()
        {
            var fileName = @"D:\Log.txt";
            var fileInfo = new FileInfo(fileName);
            await TextFileHelper.WriteText(fileName, "1234");
        }
    }
}
