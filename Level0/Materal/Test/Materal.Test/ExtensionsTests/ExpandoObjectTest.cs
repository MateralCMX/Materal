using System.Data;
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
            object input4 = "<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"><soap:Body><GetUserResponse xmlns=\"http://WebService.Server/\"><GetUserResult><Name>string</Name><Age>int</Age><TestNull>int</TestNull><ScoreValue><Name>string</Name><Score>int</Score><TestNull>int</TestNull></ScoreValue><ArrayValues><int>int</int><int>int</int></ArrayValues><Scores><ScoreInfo><Name>string</Name><Score>int</Score><TestNull>int</TestNull></ScoreInfo><ScoreInfo><Name>string</Name><Score>int</Score> <TestNull>int</TestNull></ScoreInfo></Scores></GetUserResult></GetUserResponse></soap:Body></soap:Envelope>";
            object? value4 = input4.ToExpandoObject();
            Assert.AreNotEqual(input4, value4);
            object input5 = "<GetUserResult><Name>string</Name><Age>int</Age><TestNull>int</TestNull><ScoreValue><Name>string</Name><Score>int</Score><TestNull>int</TestNull></ScoreValue><ArrayValues><int>int</int><int>int</int></ArrayValues><Scores><ScoreInfo><Name>string</Name><Score>int</Score><TestNull>int</TestNull></ScoreInfo><ScoreInfo><Name>string</Name><Score>int</Score> <TestNull>int</TestNull></ScoreInfo></Scores></GetUserResult>";
            object? value5 = input5.ToExpandoObject();
            Assert.AreNotEqual(input5, value5);
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
        [TestMethod]
        public void DataTableToExpandoObjectTest()
        {
            DataTable dataTable = new();
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Age", typeof(int));
            for (int i = 0; i < 10; i++)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow[0] = $"Materal{i}";
                dataRow[1] = i;
                dataTable.Rows.Add(dataRow);
            }
            object? value = ((object)dataTable).ToExpandoObject();
            Assert.AreNotEqual(dataTable, value);
        }
    }
}
