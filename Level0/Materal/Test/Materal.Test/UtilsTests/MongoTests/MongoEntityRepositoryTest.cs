//namespace Materal.Test.UtilsTests.MongoTests
//{
//    [TestClass]
//    public class MongoEntityRepositoryTest : BaseMongoRepositoryTest
//    {
//        private readonly IUserRepository _userRepository;
//        public MongoEntityRepositoryTest() : base()
//        {
//            _userRepository = GetRequiredService<IUserRepository>();
//        }
//        public override void AddServices(IServiceCollection services)
//        {
//            base.AddServices(services);
//            services.TryAddScoped<IUserRepository, UserRepository>();
//        }
//        /// <summary>
//        /// 插入测试
//        /// </summary>
//        [TestMethod]
//        public async Task InsertTestAsync()
//        {
//            await _userRepository.ClearAsync();
//            await _userRepository.InsertAsync(Users.Last());
//        }
//        /// <summary>
//        /// 插入多条测试
//        /// </summary>
//        [TestMethod]
//        public async Task InsertManyTestAsync()
//        {
//            await _userRepository.ClearAsync();
//            await _userRepository.InsertAsync(Users);
//        }
//    }
//}
