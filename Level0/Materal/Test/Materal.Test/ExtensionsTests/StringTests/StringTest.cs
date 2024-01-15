using Materal.Extensions;
using Materal.Utils.Model;
using Newtonsoft.Json.Linq;
using System.Dynamic;

namespace Materal.Test.ExtensionsTests.StringTests
{
    [TestClass]
    public class StringTest : BaseTest
    {
        [TestMethod]
        public void GetTypeByTypeNameTest()
        {
            Type? studentType = nameof(Student).GetTypeByTypeName<User>(new object[] { "1234", 22 });
            if (studentType is null) return;
            User model = studentType.Instantiation<User>("1234", 22);
            User model2 = model.CopyProperties<User>();
        }
        [TestMethod]
        public void ToJsonTest()
        {
            string jsonString = new { 
                IntValue = 1, 
                DoubleValue = 2.1,
                FloatValue = 3.1f, 
                DecimalValue = 4.1m,
                StringValue = "Materal",
                DateTimeValue = new DateTime(1993,4,20,8,30,40),
                GuidValue = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                ListValue = new List<int>() { 1, 2, 3, 4, 5 },
                ArrayValue = new string[] { "1", "2", "3","4", "5" },
                SubObjectValue = new
                {
                    IntValue = 1,
                    DoubleValue = 2.1,
                    FloatValue = 3.1f,
                    DecimalValue = 4.1m,
                    StringValue = "Materal",
                    DateTimeValue = new DateTime(1993, 4, 20, 8, 30, 40),
                    GuidValue = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    ListValue = new List<int>() { 1, 2, 3, 4, 5 },
                    ArrayValue = new string[] { "1", "2", "3", "4", "5" },
                    SubObjectValue = new
                    {
                        IntValue = 1,
                        DoubleValue = 2.1,
                        FloatValue = 3.1f,
                        DecimalValue = 4.1m,
                        StringValue = "Materal",
                        DateTimeValue = new DateTime(1993, 4, 20, 8, 30, 40),
                        GuidValue = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                        ListValue = new List<int>() { 1, 2, 3, 4, 5 },
                        ArrayValue = new string[] { "1", "2", "3", "4", "5" },
                    }
                }
            }.ToJson();
            JObject jObject = jsonString.JsonToDeserializeObject<JObject>();
            ExpandoObject expandoObject = jsonString.JsonToDeserializeObject<ExpandoObject>();
            dynamic dynamicObject = jsonString.JsonToDeserializeObject<dynamic>();

        }
        private class TestJsonModel
        {
            public string Name { get; set; } = string.Empty;
            public TestJsonModel? JsonModel { get; set; }
        }
        private class User : FilterModel
        {
            public string Name { get; set; } = string.Empty;
            public User()
            {
            }
            public User(string name)
            {
                Name = name;
            }
        }
        private class Student : User
        {
            public int Age { get; set; }
            public Student() : base()
            {
            }
            public Student(string name, int age) : base(name)
            {
                Age = age;
            }
        }
    }
}
