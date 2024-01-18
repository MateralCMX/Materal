using Materal.Utils.MongoDB;

namespace Materal.Test.UtilsTests.MongoTests
{
    public class UserRepository : MongoRepository<User, Guid>, IUserRepository
    {
        public UserRepository()
        {
            ConnectionString = MongoTestConstData.ConnectionString;
            DatabaseName = MongoTestConstData.DatabaseName;
            CollectionName = MongoTestConstData.CollectionName;
        }
    }
}