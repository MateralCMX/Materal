using Materal.Test.Models;

namespace Materal.Test.ExtensionsTests
{
    [TestClass]
    public class StringTest : BaseTest
    {
        [TestMethod]
        public void GetTypeByTypeNameTest()
        {
            Type? studentType = nameof(Student).GetTypeByTypeName<User>("1234", 22);
            if(studentType != null)
            {
                User model = studentType.Instantiation<User>("1234", 22);
                User model2 = model.CopyProperties<User>();
            }
        }
    }
}
