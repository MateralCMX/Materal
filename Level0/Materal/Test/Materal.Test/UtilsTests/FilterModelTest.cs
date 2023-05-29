using Materal.Test.Models;

namespace Materal.Test.UtilsTest
{
    [TestClass]
    public class FilterModel : BaseTest
    {
        [TestMethod]
        public void SortTest()
        {
            List<User> users = new()
            {
                new() { Name = "D" },
                new() { Name = "C" },
                new() { Name = "A" },
                new() { Name = "B" },
                new() { Name = "E" }
            };
            QueryUserModel querModel = new() { SortPropertyName = nameof(User.CreateTime), IsAsc = false };
            users = querModel.Sort(users);
            if (querModel.IsAsc)
            {
                Assert.IsTrue(users.First().Name == "A", "≈≈–Ú¥ÌŒÛ");
            }
            else
            {
                Assert.IsTrue(users.First().Name == "E", "≈≈–Ú¥ÌŒÛ");
            }            
        }
    }
}