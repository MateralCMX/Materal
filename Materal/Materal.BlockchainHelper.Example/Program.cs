using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Materal.BlockchainHelper.Example
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var service = new InformationSourceService();
            while (true)
            {
                string inputString = Console.ReadLine();
                switch (inputString)
                {
                    case "Exit":
                        return;
                    case "GetAll":
                        List<string> allInformation = await service.GetAllInformationAsync();
                        Console.WriteLine("--------------------------------------------------------------");
                        foreach (string information in allInformation)
                        {
                            Console.WriteLine(information);
                        }
                        Console.WriteLine("--------------------------------------------------------------");
                        break;
                    default:
                        await service.RecordNewMessage(inputString);
                        break;
                }
            }
        }
    }
}
