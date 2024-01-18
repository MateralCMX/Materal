//using Materal.Utils.MongoDB;
//using Materal.Utils.MongoDB.Extensions;
//using MongoDB.Driver;

//namespace Materal.Test.UtilsTests.MongoTests
//{
//    [TestClass]
//    public class MongoRepositoryTest : BaseMongoRepositoryTest
//    {
//        private readonly IMongoRepository _mongoRepository;
//        public MongoRepositoryTest() : base()
//        {
//            _mongoRepository = GetRequiredService<IMongoRepository>();
//            _mongoRepository.ConnectionString = MongoTestConstData.ConnectionString;
//            _mongoRepository.DatabaseName = MongoTestConstData.DatabaseName;
//            _mongoRepository.CollectionName = MongoTestConstData.CollectionName;
//        }
//        /// <summary>
//        /// 插入测试
//        /// </summary>
//        [TestMethod]
//        public async Task InsertTestAsync()
//        {
//            await _mongoRepository.ClearAsync();
//            await _mongoRepository.InsertAsync(Users.First());
//        }
//        /// <summary>
//        /// 插入多条测试
//        /// </summary>
//        [TestMethod]
//        public async Task InsertManyTestAsync()
//        {
//            await _mongoRepository.ClearAsync();
//            await _mongoRepository.InsertAsync(Users);
//        }
//    }
//}