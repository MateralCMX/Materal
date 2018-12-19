using Materal.NetworkHelper;
using System;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        public static void Main()
        {
            //Task.Run(Start);
            const string updateConfigFile = "http://www.yncwbd.com:13510/Update.txt";
            HttpHelper.HttpDownload(updateConfigFile, "temp/Update.txt", true);
            Console.ReadKey();
        }
    }
}
