using Materal.Utils.MongoDB.Extensions;

namespace Materal.Test.UtilsTests.MongoTests
{
    public abstract class BaseMongoRepositoryTest : MateralTestBase
    {
        protected readonly List<User> Users = [];
        public BaseMongoRepositoryTest() : base()
        {
            for (int i = 1; i <= 100; i++)
            {
                Users.Add(new() { Name = $"User{i}", Age = i });
            }
        }
        public override void AddServices(IServiceCollection services)
        {
            base.AddServices(services);
            services.AddMongoUtils();
        }
    }
}