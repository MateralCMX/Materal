using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Materal.NetworkHelper;

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
            var data = new Dictionary<string, string>
            {
                ["key"] = "be13b619c6b775c29a49294cbad4d8d2",
                ["locations"] = "102.667131,25.052108|102.655771,25.059705|102.716076,25.010839",
                ["coordsys"] = "gps"
            };
            string httpResult = await HttpHelper.SendGetAsync("https://restapi.amap.com/v3/assistant/coordinate/convert", data);
        }
    }
}
