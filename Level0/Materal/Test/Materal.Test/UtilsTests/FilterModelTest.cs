using Materal.Utils.Model;

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
        private class User
        {
            public string Name { get; set; } = string.Empty;
            public DateTime CreateTime { get; set; } = DateTime.Now;
        }
        private class QueryUserModel : PageRequestModel
        {
            [Contains]
            public string? Name { get; set; }
            [GreaterThanOrEqual("CreateTime")]
            public DateTime? MinCreateTime { get; set; }
            [LessThanOrEqual("CreateTime")]
            public DateTime? MaxCreateTime { get; set; }
        }
    }
}