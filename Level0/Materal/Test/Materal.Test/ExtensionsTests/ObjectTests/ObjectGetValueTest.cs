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
                SubData = new[] { new { Name = "Materal", Age = 18 }, new { Name = "Materal", Age = 18 } },
                Dic = new Dictionary<string, object>
                {
                    ["Item0"] = 0,
                    ["Item1"] = "String",
                    ["Item2"] = new int[] { 0, 1, 2, 3, 4 },
                    ["Item3"] = new { TestA = "TestA", TestB = "TestB" }
                }
            };
            //Assert.AreEqual("Materal", data.GetObjectValue<string>(nameof(data.Name)));
            //Assert.AreEqual(18, data.GetObjectValue<int>(nameof(data.Age)));
            //Assert.AreEqual("TestA", data.GetObjectValue<string>($"{nameof(data.Test)}.{nameof(data.Test.TestA)}"));
            //Assert.AreEqual(0, data.GetObjectValue<int>($"{nameof(data.Numbers)}.0"));
            //Assert.AreEqual(0, data.GetObjectValue<int>($"{nameof(data.Numbers)}[0]"));
            Assert.AreEqual(0, data.GetObjectValue<int>($"{nameof(data.Dic)}.Item0"));
            Assert.AreEqual("String", data.GetObjectValue<string>($"{nameof(data.Dic)}.Item1"));
            Assert.AreEqual(0, data.GetObjectValue<int>($"{nameof(data.Dic)}.Item2.0"));
            Assert.AreEqual(0, data.GetObjectValue<int>($"{nameof(data.Dic)}.Item2[0]"));
            Assert.AreEqual("TestA", data.GetObjectValue<string>($"{nameof(data.Dic)}.Item3.TestA"));
            //Assert.AreEqual("Materal", data.GetObjectValue<string>($"{nameof(data.SubData)}.0.{nameof(data.Name)}"));
        }
    }
}
