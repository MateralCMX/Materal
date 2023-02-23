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
            string url = "http://82.156.11.176:8700/Authority_FatAPI/api/ActionAuthority/GetList";
            Dictionary<string, string> headers = new()
            {
                ["Authorization"] = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOlsiV2ViQVBJIiwiV2ViQVBJIl0sImlzcyI6Ik1hdGVyYWwuQVBQIiwiVXNlcklEIjoiYWQ0N2YzZWEtMTA5NS00NTRiLWI2YjItNDQ1MTk5YTBhNzIxIiwiU3ViU3lzdGVtQ29kZSI6Ik1hbmFnZW1lbnRTeXN0ZW0iLCJuYmYiOjE2NzcxMzQ5NjYsImV4cCI6MTY3NzE1Mjk2NiwiaWF0IjoxNjc3MTM0OTY2fQ.SGSZ1skoTl9X08hffL-Gk92sFCapB_x02BQL6Jmnw0o"
            };
            object data = new
            {
                PageIndex = 1,
                PageSize = 10
            };
            Dictionary<string, string> queryData = new()
            {
                ["Name"] = "ะกร๗",
                ["Age"] = "25"
            };
            string result = await _httpHelper.SendPostAsync(url, queryData, data, headers);
        }
    }
}