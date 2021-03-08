using Materal.TFMS.Demo.Core;
using System;
using System.Threading.Tasks;

namespace Materal.TFMS.Demo.Client01
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.Title = ClientHelper.AppName;
            var client = ClientHelper.GetService<IClient>();
            string inputString;
            do
            {
                inputString = Console.ReadLine();
                if (inputString == "Send")
                {
                    await client.SendEventAsync();
                }
            } while (inputString != "Exit");
        }
    }
}
