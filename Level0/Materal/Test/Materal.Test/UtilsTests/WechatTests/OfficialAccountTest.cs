using Materal.Utils.Http;
using Materal.Utils.Wechat;
using Materal.Utils.Wechat.Model.OfficialAccount.Request;
using Materal.Utils.Wechat.Model.Result;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.Test.UtilsTests.WechatTests
{
    [TestClass]
    public class OfficialAccountTest : WechatTest
    {
        private readonly WechatOfficialAccountHelper _wechatHelper;
        public OfficialAccountTest() : base()
        {
            IHttpHelper httpHelper = Services.GetRequiredService<IHttpHelper>();
            _wechatHelper = new(Config, httpHelper);
        }
        protected override async Task<AccessTokenResultModel> GetAccessTokenByWechatAsync() => await _wechatHelper.GetAccessTokenAsync();

        [TestMethod]
        public async Task GetAccessTokenTest()
        {
            AccessTokenResultModel result = await GetAccessTokenByWechatAsync();
            Assert.IsNotNull(result.AccessToken);
        }
        [TestMethod]
        public async Task SendTemplateMessageAsync()
        {
            string accessToken = await GetAccessTokenAsync();
            SendTemplateMessageRequestModel requestModel = new()
            {
                AccessToken = accessToken,
                UserOpenID = OpenID2,
                TemplateID = "Ij-dTjG85bdYvzJp9sXPwSAwUdD0xULAG2_XvhAnwoc",
                Url = "https://www.baidu.com",
                TemplateDatas = new()
                {
                    new(){ Key = "keyword1", Value = "后台推送的消息"},
                    new(){ Key = "keyword2", Value = "带连接的消息"},
                    new(){ Key = "keyword3", Value = "点一下看能不能跳转到百度"}
                }
            };
            await _wechatHelper.SendTemplateMessageAsync(requestModel);
        }
    }
}
