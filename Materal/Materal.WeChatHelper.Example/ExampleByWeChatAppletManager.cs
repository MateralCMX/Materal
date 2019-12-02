using System;
using System.Threading.Tasks;
using Materal.WeChatHelper.Model;

namespace Materal.WeChatHelper.Example
{
    public class ExampleByWeChatAppletManager
    {
        public async Task GetOpenIDByCode(string code)
        {
            var config = new WeChatConfigModel
            {
                APPID = "wx93154cd53fc514f9",
                APPSECRET = "f41448533e3aa671524202d5c0fbc243"
            };
            var weChatAppletManager = new WeChatMiniProgramManager(config);
            string openID = await weChatAppletManager.GetOpenIDByCodeAsync(code);
            Console.WriteLine($"OpenID为:{openID}");
        }
    }
}
