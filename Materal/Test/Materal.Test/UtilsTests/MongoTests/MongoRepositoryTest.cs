using Materal.Utils.MongoDB;
using Materal.Utils.MongoDB.Extensions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Materal.Test.UtilsTests.MongoTests
{
    [TestClass]
    public class MongoRepositoryTest : BaseMongoRepositoryTest
    {
        private readonly IMongoRepository _repository;
        public MongoRepositoryTest() : base()
        {
            _repository = ServiceProvider.GetRequiredService<IMongoRepository>();
            _repository.ConnectionString = MongoTestConstData.ConnectionString;
            _repository.DatabaseName = MongoTestConstData.DatabaseName;
            _repository.CollectionName = MongoTestConstData.CollectionName;
        }
        [TestMethod]
        public async Task InsertTestAsync()
        {
            await _repository.ClearAsync();
            await _repository.InsertAsync(Users.First().ToBsonObject());
            bool result = await _repository.AnyAsync(Builders<BsonDocument>.Filter.Empty);
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public async Task InsertManyTestAsync()
        {
            await _repository.ClearAsync();
            await _repository.InsertAsync(Users.ToBsonObjects());
            long count = await _repository.CountAsync(Builders<BsonDocument>.Filter.Empty);
            Assert.AreEqual(Users.Count, count);
        }
        [TestMethod]
        public async Task InsertObjectTestAsync()
        {
            await _repository.ClearAsync();
            await _repository.InsertObjectAsync(Users.First());
            bool result = await _repository.AnyAsync(Builders<BsonDocument>.Filter.Empty);
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public async Task InsertManyObjectTestAsync()
        {
            await _repository.ClearAsync();
            await _repository.InsertObjectAsync(Users);
            long count = await _repository.CountAsync(Builders<BsonDocument>.Filter.Empty);
            Assert.AreEqual(Users.Count, count);
        }
        [TestMethod]
        public async Task UpdateModelTestAsync()
        {
            User user = Users.First();
            await _repository.ClearAsync();
            await _repository.InsertObjectAsync(user);
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq(nameof(User.ID), user.ID);
            user.Name = "测试修改-Model模式";
            long updateCount = await _repository.UpdateAsync(filter, user.ToBsonObject());
            Assert.AreEqual(1, updateCount);
            BsonDocument dbUser = await _repository.FirstAsync(filter);
            Assert.AreEqual(dbUser.GetValue(nameof(User.ID)).AsGuid, user.ID);
            Assert.AreEqual(dbUser.GetValue(nameof(User.Name)).AsString, user.Name);
        }
        [TestMethod]
        public async Task UpdateDefinitionTestAsync()
        {
            const string newName = "User测试修改-Definition模式";
            const int dataCount = 6;
            await _repository.ClearAsync();
            await _repository.InsertObjectAsync(Users.Take(dataCount));
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Regex(nameof(User.Name), "/User/");
            long updateCount = await _repository.UpdateAsync(filter, Builders<BsonDocument>.Update.Set(nameof(User.Name), newName));
            Assert.AreEqual(dataCount, updateCount);
            List<BsonDocument> dbUsers = await _repository.FindAsync(filter);
            Assert.AreEqual(dataCount, dbUsers.Count);
            for (int i = 0; i < dbUsers.Count; i++)
            {
                Assert.AreEqual(dbUsers[i].GetValue(nameof(User.ID)).AsGuid, Users[i].ID);
                Assert.AreEqual(dbUsers[i].GetValue(nameof(User.Name)).AsString, newName);
            }
        }
        [TestMethod]
        public async Task UpdateOneTestAsync()
        {
            const string newName = "User测试修改-UpdateOne模式";
            const int dataCount = 6;
            await _repository.ClearAsync();
            await _repository.InsertObjectAsync(Users.Take(dataCount));
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Regex(nameof(User.Name), "/User/");
            await _repository.UpdateOneAsync(filter, Builders<BsonDocument>.Update.Set(nameof(User.Name), newName));
            BsonDocument dbUser = await _repository.FirstAsync(filter);
            Assert.AreEqual(dbUser.GetValue(nameof(User.ID)).AsGuid, Users[0].ID);
            Assert.AreEqual(dbUser.GetValue(nameof(User.Name)).AsString, newName);
        }
        [TestMethod]
        public async Task UpdateObjectTestAsync()
        {
            User user = Users.First();
            await _repository.ClearAsync();
            await _repository.InsertObjectAsync(user);
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq(nameof(User.ID), user.ID);
            user.Name = "测试修改-Object模式";
            long updateCount = await _repository.UpdateObjectAsync(filter, user);
            Assert.AreEqual(1, updateCount);
            BsonDocument dbUser = await _repository.FirstAsync(filter);
            Assert.AreEqual(dbUser.GetValue(nameof(User.ID)).AsGuid, user.ID);
            Assert.AreEqual(dbUser.GetValue(nameof(User.Name)).AsString, user.Name);
        }
        [TestMethod]
        public async Task DeleteTestAsync()
        {
            const int dataCount = 6;
            await _repository.ClearAsync();
            await _repository.InsertObjectAsync(Users.Take(dataCount));
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq(nameof(User.Name), "User1");
            FilterDefinition<BsonDocument> filter2 = Builders<BsonDocument>.Filter.Eq(nameof(User.Name), "User2");
            filter = Builders<BsonDocument>.Filter.Or(filter, filter2);
            long updateCount = await _repository.DeleteAsync(filter);
            Assert.AreEqual(2, updateCount);
            long dbCount = await _repository.CountAsync(Builders<BsonDocument>.Filter.Empty);
            Assert.AreEqual(dataCount - 2, dbCount);
        }
        [TestMethod]
        public async Task DeleteOneTestAsync()
        {
            const int dataCount = 6;
            await _repository.ClearAsync();
            await _repository.InsertObjectAsync(Users.Take(dataCount));
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq(nameof(User.Name), "User1");
            FilterDefinition<BsonDocument> filter2 = Builders<BsonDocument>.Filter.Eq(nameof(User.Name), "User2");
            filter = Builders<BsonDocument>.Filter.Or(filter, filter2);
            await _repository.DeleteOneAsync(filter);
            long dbCount = await _repository.CountAsync(Builders<BsonDocument>.Filter.Empty);
            Assert.AreEqual(dataCount - 1, dbCount);
        }
        [TestMethod]
        public async Task FindTestAsync()
        {
            await _repository.ClearAsync();
            await _repository.InsertObjectAsync(Users);
            QueryUserModel queryModel = new()
            {
                IsAsc = true,
                SortPropertyName = "Name"
            };
            const int count = 100;
            queryModel.Name = "User";
            queryModel.MinCreateTime = DateTime.Now.AddDays(-1);
            queryModel.MaxCreateTime = DateTime.Now.AddDays(1);
            List<BsonDocument> users = await _repository.FindAsync(queryModel);
            Assert.AreEqual(count, users.Count);
        }
        [TestMethod]
        public async Task PagingTestAsync()
        {
            await _repository.ClearAsync();
            await _repository.InsertObjectAsync(Users);
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
            (List<BsonDocument> users, PageModel pageInfo) = await _repository.PagingAsync(queryModel);
            Assert.AreEqual(count, pageInfo.DataCount);
            Assert.AreEqual(queryModel.PageSize, users.Count);
        }
    }
}