using Materal.Abstractions;
using Materal.Utils.Http;
using Materal.Utils.Wechat;

namespace Materal.Test.UtilsTests.WechatTests
{
    //wx5752ec798fd5fe95
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
                //APPID = "wx5752ec798fd5fe95",//教师
                //APPSECRET = "5e04a5c6146f07bca0b4f5b572563bb2"
                //APPID = "wxc2291633533c6c52",//推广
                //APPSECRET = "db48bfefbe93524e3b06b428fc765d52"
                //APPID = "wxc4823d71dcecbc56",//学生
                //APPSECRET = "9347fb187b8226afaf63adcf152f017d"
                APPID = "wx2f73fc984c4c0787",//留学
                APPSECRET = "ee79fcbcca44c63a1aa2dd50c348d505"

            }, _httpHelper);
            string code = "081uWi000mYeBP1KsF100NtCTn0uWi0J";
            string openID = await manager.GetOpenIDByCodeAsync(code);
        }
    }
}
