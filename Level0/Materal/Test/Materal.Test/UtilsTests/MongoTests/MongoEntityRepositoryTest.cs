using Materal.Utils.MongoDB;
using MongoDB.Driver;

namespace Materal.Test.UtilsTests.MongoTests
{
    [TestClass]
    public partial class MongoRepositoryEntityTest : BaseMongoRepositoryTest
    {
        private readonly IMongoRepository<User, Guid> _repository;
        public MongoRepositoryEntityTest() : base()
        {
            _repository = GetRequiredService<IMongoRepository<User, Guid>>();
            _repository.ConnectionString = MongoTestConstData.ConnectionString;
            _repository.DatabaseName = MongoTestConstData.DatabaseName;
            _repository.CollectionName = MongoTestConstData.CollectionName;
        }
        [TestMethod]
        public async Task InsertTestAsync()
        {
            await _repository.ClearAsync();
            await _repository.InsertAsync(Users.First());
            bool result = await _repository.AnyAsync(Builders<User>.Filter.Empty);
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public async Task InsertManyTestAsync()
        {
            await _repository.ClearAsync();
            await _repository.InsertAsync(Users);
            long count = await _repository.CountAsync(Builders<User>.Filter.Empty);
            Assert.AreEqual(Users.Count, count);
        }
        [TestMethod]
        public async Task UpdateModelTestAsync()
        {
            User user = Users.First();
            await _repository.ClearAsync();
            await _repository.InsertAsync(user);
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(m => m.ID, user.ID);
            user.Name = "测试修改-Model模式";
            long updateCount = await _repository.UpdateAsync(filter, user);
            Assert.AreEqual(1, updateCount);
            User dbUser = await _repository.FirstAsync(filter);
            Assert.AreEqual(dbUser.ID, user.ID);
            Assert.AreEqual(dbUser.Name, user.Name);
        }
        [TestMethod]
        public async Task UpdateDefinitionTestAsync()
        {
            const string newName = "User测试修改-Definition模式";
            const int dataCount = 6;
            await _repository.ClearAsync();
            await _repository.InsertAsync(Users.Take(dataCount));
            FilterDefinition<User> filter = Builders<User>.Filter.Regex(m => m.Name, "/User/");
            long updateCount = await _repository.UpdateAsync(filter, Builders<User>.Update.Set(m => m.Name, newName));
            Assert.AreEqual(dataCount, updateCount);
            List<User> dbUsers = await _repository.FindAsync(filter);
            Assert.AreEqual(dataCount, dbUsers.Count);
            for (int i = 0; i < dbUsers.Count; i++)
            {
                Assert.AreEqual(dbUsers[i].ID, Users[i].ID);
                Assert.AreEqual(dbUsers[i].Name, newName);
            }
        }
        [TestMethod]
        public async Task UpdateOneTestAsync()
        {
            const string newName = "User测试修改-UpdateOne模式";
            const int dataCount = 6;
            await _repository.ClearAsync();
            await _repository.InsertAsync(Users.Take(dataCount));
            FilterDefinition<User> filter = Builders<User>.Filter.Regex(m => m.Name, "/User/");
            await _repository.UpdateOneAsync(filter, Builders<User>.Update.Set(m => m.Name, newName));
            User dbUser = await _repository.FirstAsync(filter);
            Assert.AreEqual(dbUser.ID, Users[0].ID);
            Assert.AreEqual(dbUser.Name, newName);
        }
        [TestMethod]
        public async Task DeleteTestAsync()
        {
            const int dataCount = 6;
            await _repository.ClearAsync();
            await _repository.InsertAsync(Users.Take(dataCount));
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(m => m.Name, "User1");
            FilterDefinition<User> filter2 = Builders<User>.Filter.Eq(m => m.Name, "User2");
            filter = Builders<User>.Filter.Or(filter, filter2);
            long updateCount = await _repository.DeleteAsync(filter);
            Assert.AreEqual(2, updateCount);
            long dbCount = await _repository.CountAsync(Builders<User>.Filter.Empty);
            Assert.AreEqual(dataCount - 2, dbCount);
        }
        [TestMethod]
        public async Task DeleteOneTestAsync()
        {
            const int dataCount = 6;
            await _repository.ClearAsync();
            await _repository.InsertAsync(Users.Take(dataCount));
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(m => m.Name, "User1");
            FilterDefinition<User> filter2 = Builders<User>.Filter.Eq(m => m.Name, "User2");
            filter = Builders<User>.Filter.Or(filter, filter2);
            await _repository.DeleteOneAsync(filter);
            long dbCount = await _repository.CountAsync(Builders<User>.Filter.Empty);
            Assert.AreEqual(dataCount - 1, dbCount);
        }
        [TestMethod]
        public async Task FindTestAsync()
        {
            await _repository.ClearAsync();
            await _repository.InsertAsync(Users);
            QueryUserModel queryModel = new()
            {
                IsAsc = true,
                SortPropertyName = "Name"
            };
            const int count = 100;
            queryModel.Name = "User";
            queryModel.MinCreateTime = DateTime.Now.AddDays(-1);
            queryModel.MaxCreateTime = DateTime.Now.AddDays(1);
            List<User> users = await _repository.FindAsync(queryModel);
            Assert.AreEqual(count, users.Count);
        }
        [TestMethod]
        public async Task PagingTestAsync()
        {
            await _repository.ClearAsync();
            await _repository.InsertAsync(Users);
            QueryUserModel queryModel = new()
            {
                IsAsc = true,
                SortPropertyName = "Name",
                PageIndex = 1,
                PageSize = 10
            };
            const int count = 100;
            queryModel.Name = "User";
            queryModel.MinCreateTime = DateTime.Now.AddDays(-1);
            queryModel.MaxCreateTime = DateTime.Now.AddDays(1);
            (List<User> users, PageModel pageInfo) = await _repository.PagingAsync(queryModel);
            Assert.AreEqual(count, pageInfo.DataCount);
            Assert.AreEqual(queryModel.PageSize, users.Count);
        }
    }
}