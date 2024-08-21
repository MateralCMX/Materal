namespace Materal.Test.UtilsTests
{
    [TestClass]
    public class FilterModelTest : MateralTestBase
    {
        [TestMethod]
        public void SortTest()
        {
            List<User> users =
            [
                new() { Name = "D" },
                new() { Name = "C" },
                new() { Name = "A" },
                new() { Name = "B" },
                new() { Name = "E" }
            ];
            QueryUserModel queryModel = new()
            {
                SortPropertyName = nameof(User.CreateTime),
                IsAsc = false,
                SourceIDs = [Guid.NewGuid(), Guid.NewGuid()]
            };
            users = queryModel.Sort(users);
            if (queryModel.IsAsc)
            {
                Assert.IsTrue(users.First().Name == "A", "排序错误");
            }
            else
            {
                Assert.IsTrue(users.First().Name == "E", "排序错误");
            }
        }
        [TestMethod]
        public void ListContains()
        {
            List<User> users =
            [
                new() { Name = "D" },
                new() { Name = "C" },
                new() { Name = "A" },
                new() { Name = "B" },
                new() { Name = "E" }
            ];
            QueryUserModel queryModel = new()
            {
                SourceIDs = [Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"), Guid.NewGuid()]
            };
            Expression<Func<User, bool>> expression = queryModel.GetSearchExpression<User>();
            Assert.IsTrue(users.Where(expression.Compile()).Count() == users.Count);
        }
        [TestMethod]
        public void ArrayContains()
        {
            List<User> users =
            [
                new() { Name = "D" },
                new() { Name = "C" },
                new() { Name = "A" },
                new() { Name = "B" },
                new() { Name = "E" }
            ];
            QueryUserModel queryModel = new()
            {
                SourceID2s = [Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"), Guid.NewGuid()]
            };
            Expression<Func<User, bool>> expression = queryModel.GetSearchExpression<User>();
            Assert.IsTrue(users.Where(expression.Compile()).Count() == users.Count);
        }
        private class User
        {
            public string Name { get; set; } = string.Empty;
            public DateTime CreateTime { get; set; } = DateTime.Now;
            public Guid? SourceID { get; set; } = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");
        }
        private class QueryUserModel : PageRequestModel
        {
            [Contains]
            public string? Name { get; set; }
            [GreaterThanOrEqual("CreateTime")]
            public DateTime? MinCreateTime { get; set; }
            [LessThanOrEqual("CreateTime")]
            public DateTime? MaxCreateTime { get; set; }
            [Contains("SourceID")]
            public List<Guid>? SourceIDs { get; set; }
            [Contains("SourceID")]
            public Guid[]? SourceID2s { get; set; }
        }
    }
}