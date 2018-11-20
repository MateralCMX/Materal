using Materal.NetworkHelper;
using System;
using System.Threading.Tasks;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        public static void Main()
        {
            Task.Run(async () => { await RunTask(); });
            Console.ReadKey();
        }

        public static async Task RunTask()
        {
            const string downloadUrl = "http://www.yncwbd.com:13510/setup.exe";
            const string filePath = "D:/MateralDownload/setup.exe";
            decimal sumSize = 0;
            void FileSumSize(long fileSize)
            {
                sumSize = fileSize;
                Console.WriteLine($"文件总大小{fileSize / 1024d:F2}KB");
            }
            void Progress(long sum)
            {
                Console.WriteLine($"已下载{sum / sumSize * 100:F2}%");
            }

            await HttpHelper.HttpDownload(downloadUrl, filePath, 1024, FileSumSize, Progress);
            Console.WriteLine("下载完毕");
        }
    }
}
