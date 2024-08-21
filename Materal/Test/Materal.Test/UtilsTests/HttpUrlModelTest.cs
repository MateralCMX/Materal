namespace Materal.Test.UtilsTests
{
    /// <summary>
    /// HttpUrl模型测试
    /// </summary>
    [TestClass]
    public class HttpUrlModelTest : MateralTestBase
    {
        [TestMethod]
        public void Test01()
        {
            HttpUrlModel httpUrlModel = new("http://www.baidu.com:1237/dasf?id=0000");
            Assert.IsNotNull(httpUrlModel);
        }
    }
}
