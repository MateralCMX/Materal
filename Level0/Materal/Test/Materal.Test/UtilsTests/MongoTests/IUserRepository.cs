using Materal.Utils.MongoDB;

namespace Materal.Test.UtilsTests.MongoTests
{
    public interface IUserRepository : IMongoRepository<User, Guid>
    {
    }
}