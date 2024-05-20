namespace Materal.Test.UtilsTests.ModelTests
{
    [TestClass]
    public class PageModelTest : BaseTest
    {
        [TestMethod]
        public void JsonTest()
        {
            PageModel pageModel = new(2, 10, 1000);
            string jsonStr = pageModel.ToJson();
            Console.WriteLine(jsonStr);
            jsonStr = @"{
  ""PageIndex"": 2,
  ""PageSize"": 20
}";
            TestQueryModel testModel = jsonStr.JsonToObject<TestQueryModel>();
            Assert.AreEqual(2, testModel.PageIndex);
            Assert.AreEqual(20, testModel.PageSize);
        }
        private class TestQueryModel : PageRequestModel
        {

        }
    }
}
