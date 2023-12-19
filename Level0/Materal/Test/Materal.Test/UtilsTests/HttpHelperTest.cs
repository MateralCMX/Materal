using Materal.Abstractions;
using Materal.Utils.Http;
using System.Text;

namespace Materal.Test.UtilsTest
{
    [TestClass]
    public class HttpHelperTest : BaseTest
    {
        private readonly IHttpHelper _httpHelper;
        public HttpHelperTest() : base()
        {
            _httpHelper = MateralServices.GetRequiredService<IHttpHelper>();
        }
        [TestMethod]
        public async Task TestSendAsync()
        {
            try
            {
                string url = "https://175.27.254.187:8700/RCServerCenterAPI/api/Namespace/GetList";
                Dictionary<string, string> headers = new()
                {
                    ["Authorization"] = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOlsiV2ViQVBJIiwiV2ViQVBJIl0sImlzcyI6Ik1hdGVyYWxSZWxlYXNlQ2VudGVyIiwiVXNlcklEIjoiNjBjNDMzODktODA4Ny00ZDRiLWI3ZmQtODllMDZkZGRiMWM3IiwibmJmIjoxNjc3NjUxODY0LCJleHAiOjE2Nzc2Njk4NjQsImlhdCI6MTY3NzY1MTg2NH0.jFVs4wkoZJ5_sNrnnHDm03axcC8zN5SHfp46c980hBY"
                };
                var data = new
                {
                    PageIndex = 1,
                    PageCount = 10
                };
                string result = await _httpHelper.SendPostAsync(url, null, data, headers, Encoding.UTF8);
            }
            catch (MateralHttpException ex)
            {
                string errorMessage = ex.GetExceptionMessage();
                Console.WriteLine(errorMessage);
            }
        }
    }
}