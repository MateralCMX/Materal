using Materal.Abstractions;
using Materal.Utils.Http;
using Materal.Utils.Wechat;
using Materal.Utils.Wechat.Model;

namespace Materal.Test.UtilsTests.WechatTests
{
    [TestClass]
    public class WetchatTest : BaseTest
    {
        private readonly IHttpHelper _httpHelper;
        public WetchatTest() : base()
        {
            _httpHelper = MateralServices.GetService<IHttpHelper>();
        }
        [TestMethod]
        public async Task TestSendAsync()
        {
            WeChatMiniProgramManager manager = new(new Utils.Wechat.Model.WeChatConfigModel
            {
                APPID = "wx2f73fc984c4c0787",
                APPSECRET = "ee79fcbcca44c63a1aa2dd50c348d505"

            }, _httpHelper);
            string code = "081uWi000mYeBP1KsF100NtCTn0uWi0J";
            try
            {
                string openID = await manager.GetOpenIDByCodeAsync(code);
                Assert.IsNotNull(openID);
            }
            catch (WeChatException ex)
            {
                if (ex.Message.StartsWith("invalid code")) return;
                throw;
            }
        }
    }
}
