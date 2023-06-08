using Materal.Utils.Model;

namespace Materal.Test.ExtensionsTests.StringTests
{
    [TestClass]
    public class StringTest : BaseTest
    {
        [TestMethod]
        public void GetTypeByTypeNameTest()
        {
            Type? studentType = nameof(Student).GetTypeByTypeName<User>("1234", 22);
            if (studentType == null) return;
            User model = studentType.Instantiation<User>("1234", 22);
            User model2 = model.CopyProperties<User>();
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
