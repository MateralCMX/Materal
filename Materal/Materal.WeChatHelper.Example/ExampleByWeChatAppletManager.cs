using System;
using Materal.WeChatHelper.Model;

namespace Materal.WeChatHelper.Example
{
    public class ExampleByWeChatAppletManager
    {
        public void GetOpenIDByCode(string code)
        {
            var config = new WeChatConfigModel
            {
                APPID = "wx4b55d7249ec22918",
                APPSECRET = "a41231df549e137ae85d287480da1240"
            };
            var weChatAppletManager = new WeChatMiniProgramManager(config);
            string openID = weChatAppletManager.GetOpenIDByCode(code);
            Console.WriteLine($"OpenID为:{openID}");
        }
    }
}
