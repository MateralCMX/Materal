using System;
using System.Threading.Tasks;

namespace Materal.WeChatHelper.Example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var example = new ExampleByWeChatAppletManager();
            Task.Run(async () =>
            {
                await example.GetOpenIDByCode("021Mdocj0lSMFp1WWzdj0kXlcj0Mdocn");
            });
            //var example = new ExampleByWeChatPublicNumberManager("wx5f8742015dad99f0", "dc5f36424391cdcfb488b45e44565f63");
            //string accessToken = example.GetAccessToken();
            ////const string accessToken = @"22_Qf4k8u8nCABjOzSm1hVM38Rkzs87t2GLhVAmsmTQbCBrN_D5ktiJKm7wNOdfBLyRNDZSFy6_QeWxLCSDjs55dEs2VPH8TWBhuZ5hgtK7tKTIf8yjEP4V4dfrho5C8PlkICupnTBQvXvR829XXDWdAFARXQ";
            //example.GetTemporaryMaterial(accessToken);
            Console.ReadKey();
        }
    }
}
