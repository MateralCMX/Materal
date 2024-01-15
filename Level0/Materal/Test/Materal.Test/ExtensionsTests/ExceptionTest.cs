using Materal.Utils.Http;
using System.Diagnostics;

namespace Materal.Test.ExtensionsTests
{
    [TestClass]
    public class ExceptionTest : BaseTest
    {
        [TestMethod]
        public void GetNotStackTraceExceptionErrorMessageTest()
        {
            Exception exception = new NotImplementedException("测试异常");
            Debug.WriteLine(exception.GetErrorMessage());
        }
        [TestMethod]
        public async Task GetHasStackTraceExceptionErrorMessageTestAsync()
        {
            try
            {
                await ThrowNewException1Async();
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.GetErrorMessage());
            }
        }
        private static async Task ThrowNewException1Async()
        {
            try
            {
                await ThrowNewException2Async();
            }
            catch (Exception exception)
            {
                throw new Exception("Exception 01", exception);
            }
        }
        private static async Task ThrowNewException2Async()
        {
            try
            {
                await ThrowNewException3Async();
            }
            catch (Exception exception)
            {
                throw new MateralException("Exception 02", exception);
            }
        }
        private static async Task ThrowNewException3Async()
        {
            await new HttpHelper().SendGetAsync("http://localhost:5000/Api/AccessLogTest/ErrorTest");
        }
    }
}
