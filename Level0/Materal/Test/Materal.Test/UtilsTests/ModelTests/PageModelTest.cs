﻿namespace Materal.Test.UtilsTests.ModelTests
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
        }
    }
}