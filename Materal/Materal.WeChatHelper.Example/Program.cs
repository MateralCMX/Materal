using System;

namespace Materal.WeChatHelper.Example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var example = new ExampleByWeChatPublicNumberManager("wx5f8742015dad99f0", "dc5f36424391cdcfb488b45e44565f63");
            //example.GetAccessToken();
            const string accessToken = @"22_vgRGOZRf-O2ZOUY65ljw363H9IltzMDpdbx-BoahQS-A3CYHfAeV3dHTu7kpOIKhjDrTg5lOFUQochQVdmX26N8gdLcfutY4q2w4h-9oIex3ox5puNicUKxipSriE9lIGbciIz6-HiUofFWuGNNiAEAJQQ";
            example.CreateMenu(accessToken);
            Console.ReadKey();
        }
    }
}
