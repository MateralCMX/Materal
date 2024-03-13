using System.Data;

namespace Materal.Test.ExtensionsTests.ObjectTests
{
    [TestClass]
    public class ObjectGetValueTest : BaseTest
    {
        [TestMethod]
        public void GetValueTest()
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
                },
                DT = new DataTable()
            };
            data.DT.Columns.Add("ID", typeof(Guid));
            data.DT.Columns.Add("Name", typeof(string));
            DataRow dr = data.DT.NewRow();
            dr["ID"] = Guid.NewGuid();
            dr["Name"] = "小红";
            data.DT.Rows.Add(dr);
            dr = data.DT.NewRow();
            dr["ID"] = Guid.NewGuid();
            dr["Name"] = "小蓝";
            data.DT.Rows.Add(dr);
            Assert.AreEqual("Materal", data.GetObjectValue<string>(nameof(data.Name)));
            Assert.AreEqual(18, data.GetObjectValue<int>(nameof(data.Age)));
            Assert.AreEqual("TestA", data.GetObjectValue<string>($"{nameof(data.Test)}.{nameof(data.Test.TestA)}"));
            Assert.AreEqual(0, data.GetObjectValue<int>($"{nameof(data.Numbers)}.0"));
            Assert.AreEqual(0, data.GetObjectValue<int>($"{nameof(data.Numbers)}[0]"));
            Assert.AreEqual(0, data.GetObjectValue<int>($"{nameof(data.Dic)}.Item0"));
            Assert.AreEqual("String", data.GetObjectValue<string>($"{nameof(data.Dic)}.Item1"));
            Assert.AreEqual(0, data.GetObjectValue<int>($"{nameof(data.Dic)}.Item2.0"));
            Assert.AreEqual(0, data.GetObjectValue<int>($"{nameof(data.Dic)}.Item2[0]"));
            Assert.AreEqual("TestA", data.GetObjectValue<string>($"{nameof(data.Dic)}.Item3.TestA"));
            Assert.AreEqual("Materal", data.GetObjectValue<string>($"{nameof(data.SubData)}.0.{nameof(data.Name)}"));
            Assert.AreEqual("小红", data.GetObjectValue<string>($"DT.0.Name"));
            Assert.AreEqual("小红", data.GetObjectValue<string>($"DT.0.1"));
            Assert.AreEqual("小红", data.GetObjectValue<string>($"DT.0[1]"));
            Assert.AreEqual("小红", data.GetObjectValue<string>($"DT[0].Name"));
            Assert.AreEqual("小红", data.GetObjectValue<string>($"DT[0].1"));
            Assert.AreEqual("小红", data.GetObjectValue<string>($"DT[0][1]"));
            int[] numbers = data.Numbers;
            Assert.AreEqual(0, numbers.GetObjectValue<int>($"0"));
            Assert.AreEqual(0, numbers.GetObjectValue<int>($"[0]"));
            DataTable dt = data.DT;
            Assert.AreEqual("小红", dt.GetObjectValue<string>($"0.Name"));
            Assert.AreEqual("小红", dt.GetObjectValue<string>($"0.1"));
            Assert.AreEqual("小红", dt.GetObjectValue<string>($"0[1]"));
            Assert.AreEqual("小红", dt.GetObjectValue<string>($"[0].Name"));
            Assert.AreEqual("小红", dt.GetObjectValue<string>($"[0].1"));
            Assert.AreEqual("小红", dt.GetObjectValue<string>($"[0][1]"));
        }
    }
}
