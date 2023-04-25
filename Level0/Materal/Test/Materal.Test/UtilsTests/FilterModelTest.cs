using Materal.Abstractions;
using Materal.Test.Models;
using Materal.Utils.Http;
using System.Text;

namespace Materal.Test.UtilsTest
{
    [TestClass]
    public class FilterModel : BaseTest
    {
        [TestMethod]
        public void TestSend()
        {
            User user = new()
            {
                Name = "Materal",
            };
            Func<User, bool> a = user.GetSearchDelegate<User>();
        }
    }
}