using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Materal.WeChatHelper.Model;
using Materal.WeChatHelper.Model.Basis.Request;

namespace Materal.WeChatHelper.Example
{
    public class ExampleByWeChatAppletManager
    {
        private readonly WeChatConfigModel _config;
        private readonly WeChatMiniProgramManager _weChatAppletManager;

        public ExampleByWeChatAppletManager()
        {
            _config = new WeChatConfigModel
            {
                APPID = "wxc2291633533c6c52",
                APPSECRET = "db48bfefbe93524e3b06b428fc765d52"
            };
            _weChatAppletManager = new WeChatMiniProgramManager(_config);
        }

        public async Task GetOpenIDByCode(string code)
        {
            string openID = await _weChatAppletManager.GetOpenIDByCodeAsync(code);
            Console.WriteLine($"OpenID为:{openID}");
        }
        public async Task<string> GetAccessToken()
        {
            var accessToken = await _weChatAppletManager.GetAccessTokenAsync();
            Console.WriteLine($"AccessToken为:{accessToken.AccessToken}");
            return accessToken.AccessToken;
        }
        public async Task SubscribeMessageSend(string accessToken)
        {
            try
            {
                await _weChatAppletManager.SubscribeMessageSendAsync(new SubscribeMessageSendRequestModel
                {
                    AccessToken = accessToken,
                    OpenID = "oB0hK5XuQWCmWc91RGeO_147i3y4",
                    TemplateID = "lxo5sUnYb4HtayVez9pO8B-y0CHEemjwvXCxBR8kQLE",
                    TemplateData = new Dictionary<string, string>
                    {
                        ["character_string1"] = "202211031428",
                        ["phrase4"] = "订单状态",
                        ["thing8"] = "据说是温馨提示"
                    },
                    GoToPage = "pages/important/index",
                    MiniprogramState = "trial"
                }); 
                Console.WriteLine("发送成功");
            }
            catch (WeChatException ex)
            {
                Console.WriteLine($"发送消息失败：{ex.ErrorCode}, {ex.Message}");
            }
        }
    }
}
