using Materal.Utils.Http;
using Materal.Utils.Wechat;
using Materal.Utils.Wechat.Model.OfficialAccount.Request;
using Materal.Utils.Wechat.Model.OfficialAccount.Result;
using Materal.Utils.Wechat.Model.Result;

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
        public async Task GetAccessTokenTestAsync()
        {
            AccessTokenResultModel result = await GetAccessTokenByWechatAsync();
            Assert.IsNotNull(result.AccessToken);
        }
        [TestMethod]
        public async Task GetWebAssessTokenByCodeTestAsync()
        {
            string code = "0415R8Ga1URgwF0zYEHa1CaIND05R8Ge";
            WebAssessTokenResultModel result = await _wechatHelper.GetWebAssessTokenByCodeAsync(code);
            Assert.IsNotNull(result.OpenID);
        }
        [TestMethod]
        public async Task SendTemplateMessageTestAsync()
        {
            string accessToken = await GetAccessTokenAsync();
            SendTemplateMessageRequestModel requestModel = new()
            {
                AccessToken = accessToken,
                UserOpenID = OpenID1,
                TemplateID = "gF7v6mCv2X94vb6cLO7I3hc0IWYAr5DD9vDgSFHgjf8",
                Url = "https://www.baidu.com",
                ClientMessageID = Guid.NewGuid().ToString(),
                Miniprogram = new GoToMiniprogramModel
                {
                    AppID = "wxc4823d71dcecbc56",
                    PagePath = "pages/index/index"
                },
                TemplateDatas =
                [
                    new("thing9", "测试上报人"),
                    new("time10", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    new("number11", "测试的，没有进度"),
                    new("thing8", "测试小程序跳转"),
                ]
            };
            await _wechatHelper.SendTemplateMessageAsync(requestModel);
        }
    }
}
