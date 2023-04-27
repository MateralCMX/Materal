using Materal.TTA.Demo.Domain;
using Materal.TTA.SqlServerEFRepository;

namespace Materal.TTA.Demo.SqlServerEFRepository
{
    public class TestDomainSqlServerEFRepository : SqlServerEFRepositoryImpl<TestDomain, Guid, TTADemoDBContext>, ITestDomainRepository
    {
        public TestDomainSqlServerEFRepository(TTADemoDBContext dbContext) : base(dbContext)
        {
        }
    }
}
