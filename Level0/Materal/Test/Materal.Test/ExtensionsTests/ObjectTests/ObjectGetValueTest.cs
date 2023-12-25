using System.ComponentModel.DataAnnotations;

namespace Materal.Test.ExtensionsTests.ObjectTests
{
    [TestClass]
    public class ObjectGetValueTest : BaseTest
    {
        [TestMethod]
        public void GetValue()
        {
            var data = new
            {
                Name = "Materal",
                Age = 18,
                Test = new { TestA = "TestA", TestB = "TestB" },
                Numbers = new int[] { 0, 1, 2, 3, 4 },
                SubData = new[] { new { Name = "Materal", Age = 18 }, new { Name = "Materal", Age = 18 } }
            };
            Assert.AreEqual("Materal", data.GetValue<string>(nameof(data.Name)));
            Assert.AreEqual(18, data.GetValue<int>(nameof(data.Age)));
            Assert.AreEqual("TestA", data.GetValue<string>($"{nameof(data.Test)}.{nameof(data.Test.TestA)}"));
            Assert.AreEqual(0, data.GetValue<int>($"{nameof(data.Numbers)}.0"));
            Assert.AreEqual("Materal", data.GetValue<string>($"{nameof(data.SubData)}.0.{nameof(data.Name)}"));
        }
    }
}
