using Materal.TTA.Demo.Domain;
using Materal.TTA.SqliteEFRepository;

namespace Materal.TTA.Demo.SqliteEFRepository
{
    public class TestDomainSqliteEFRepository : SqliteEFRepositoryImpl<TestDomain, Guid, TTADemoDBContext>, ITestDomainRepository
    {
        public TestDomainSqliteEFRepository(TTADemoDBContext dbContext) : base(dbContext)
        {
        }
    }
}
