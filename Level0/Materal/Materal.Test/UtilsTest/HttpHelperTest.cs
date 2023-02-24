using Materal.Abstractions;
using Materal.Utils.Http;

namespace Materal.Test.UtilsTest
{
    [TestClass]
    public class HttpHelperTest : BaseTest
    {
        private readonly IHttpHelper _httpHelper;
        public HttpHelperTest()
        {
            _httpHelper = MateralServices.GetService<IHttpHelper>();
        }

        [TestMethod]
        public async Task TestSendAsync()
        {
            string url = "http://127.0.0.1:8500/v1/agent/services";
            string result = await _httpHelper.SendGetAsync(url);
        }
    }
}