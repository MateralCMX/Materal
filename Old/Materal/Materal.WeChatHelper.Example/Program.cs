﻿using System;
using System.Threading.Tasks;

namespace Materal.Utils.Wechat.Example
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var example = new ExampleByWeChatAppletManager();
            //await example.GetOpenIDByCode("021Mdocj0lSMFp1WWzdj0kXlcj0Mdocn");
            var accessToken = "62_XbEhDNExfMBqGzHz2SfoRCZeBb2FkEmr7Rm7g_O65KzBNYTzId2WWumGx-LAFBgECybxP_PzwofOjx2m8pG-9BTFTjJ4DU_XWc_pnLTGwPK63gzfpdNKnFAmNuUOuEb93cDk74PbSMoYqUB0UGJeAFADPS";
            //var accessToken = await example.GetAccessToken();
            while (true)
            {
                string inputString = Console.ReadLine();
                if (inputString == "Exit'") break;
                await example.SubscribeMessageSend(accessToken);                
            }





            //var example = new ExampleByWeChatPublicNumberManager("wx5f8742015dad99f0", "dc5f36424391cdcfb488b45e44565f63");
            //string accessToken = example.GetAccessToken();
            ////const string accessToken = @"22_Qf4k8u8nCABjOzSm1hVM38Rkzs87t2GLhVAmsmTQbCBrN_D5ktiJKm7wNOdfBLyRNDZSFy6_QeWxLCSDjs55dEs2VPH8TWBhuZ5hgtK7tKTIf8yjEP4V4dfrho5C8PlkICupnTBQvXvR829XXDWdAFARXQ";
            //example.GetTemporaryMaterial(accessToken);
        }
    }
}
