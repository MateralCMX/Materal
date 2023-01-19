using Materal.TFMS.Demo.Core;

namespace Materal.TFMS.Demo.Client01
{
    public class Program
    {
        public static async Task Main()
        {
            Console.Title = ClientHelper.AppName;
            var client = ClientHelper.GetService<IClient>();
            string? inputString;
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
