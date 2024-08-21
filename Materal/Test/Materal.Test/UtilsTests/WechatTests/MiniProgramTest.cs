using Materal.Utils.Http;
using Materal.Utils.Wechat;
using Materal.Utils.Wechat.Model;
using Materal.Utils.Wechat.Model.Result;

namespace Materal.Test.UtilsTests.WechatTests
{
    [TestClass]
    public class MiniProgramTest : WechatTest
    {
        private readonly WechatMiniProgramHelper _wechatHelper;
        public MiniProgramTest() : base()
        {
            IHttpHelper httpHelper = ServiceProvider.GetRequiredService<IHttpHelper>();
            _wechatHelper = new(Config, httpHelper);
        }
        protected override async Task<AccessTokenResultModel> GetAccessTokenByWechatAsync() => await _wechatHelper.GetAccessTokenAsync();
        [TestMethod]
        public async Task TestSendAsync()
        {
            string code = "081uWi000mYeBP1KsF100NtCTn0uWi0J";
            try
            {
                string openID = await _wechatHelper.GetOpenIDByCodeAsync(code);
                Assert.IsNotNull(openID);
            }
            catch (WechatException ex)
            {
                if (ex.Message.StartsWith("invalid code")) return;
                throw;
            }
        }
    }
}
