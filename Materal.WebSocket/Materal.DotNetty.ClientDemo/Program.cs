using System;
using System.Threading.Tasks;

namespace Materal.DotNetty.ClientDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Task.Run(async () => await RunClientAsync());
            string inputKey = string.Empty;
            while (!string.Equals(inputKey, "Exit", StringComparison.Ordinal))
            {
                inputKey = Console.ReadLine();
            }
        }

        private static async Task RunClientAsync()
        {

        }
    }
}
