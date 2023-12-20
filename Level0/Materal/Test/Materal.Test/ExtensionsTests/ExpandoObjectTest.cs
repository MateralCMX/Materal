using System.Dynamic;
using System.Text.Json;

namespace Materal.Test.ExtensionsTests
{
    [TestClass]
    public class ExpandoObjectTest : BaseTest
    {
        [TestMethod]
        public void StringToExpandoObjectTest()
        {
            object input1 = "Materal";
            object? value1 = input1.ToExpandoObject();
            Assert.AreEqual(input1, value1);
            object input2 = "{\"Name\":\"Materal\"}";
            object? value2 = input2.ToExpandoObject();
            Assert.AreNotEqual(input2, value2);
            object input3 = "[{\"Name\":\"Materal1\"},{\"Name\":\"Materal2\"},{\"Name\":\"Materal3\"}]";
            object? value3 = input3.ToExpandoObject();
            Assert.AreNotEqual(input3, value3);
        }
        [TestMethod]
        public void ExpandoObjectToExpandoObjectTest()
        {
            object input1 = new ExpandoObject();
            object? value1 = input1.ToExpandoObject();
            Assert.AreEqual(input1, value1);
        }
        [TestMethod]
        public void ObjectToExpandoObjectTest()
        {
            object input1 = new { Name = "Materal" };
            object? value1 = input1.ToExpandoObject();
            Assert.AreNotEqual(input1, value1);
        }
        [TestMethod]
        public void ArrayToExpandoObjectTest()
        {
            object input1 = new[] { new { Name = "Materal1" }, new { Name = "Materal2" }, new { Name = "Materal3" } };
            object? value1 = input1.ToExpandoObject();
            Assert.AreNotEqual(input1, value1);
            object input2 = new List<object> { new { Name = "Materal1" }, new { Name = "Materal2" }, new { Name = "Materal3" } };
            object? value2 = input2.ToExpandoObject();
            Assert.AreNotEqual(input2, value2);
        }
        [TestMethod]
        public void DictionaryToExpandoObjectTest()
        {
            object input1 = new Dictionary<string, object> { { "Name", "Materal" } };
            object? value1 = input1.ToExpandoObject();
            Assert.AreNotEqual(input1, value1);
        }
        [TestMethod]
        public void JsonElementToExpandoObjectTest()
        {
            object input1 = JsonDocument.Parse("{\"Name\":\"Materal\"}").RootElement;
            object? value1 = input1.ToExpandoObject();
            Assert.AreNotEqual(input1, value1);
        }
    }
}
