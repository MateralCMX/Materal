using Materal.TFMS.Demo.Core;
using System;

namespace Materal.TFMS.Demo.Client01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = ClientHelper.AppName;
            var client = ClientHelper.GetService<IClient>();
            string inputString;
            do
            {
                inputString = Console.ReadLine();
                if (inputString == "Send")
                {
                    client.SendEvent();
                }
            } while (inputString != "Exit");
        }
    }
}
