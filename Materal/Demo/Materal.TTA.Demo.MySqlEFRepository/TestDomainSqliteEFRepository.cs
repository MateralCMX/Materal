using Materal.TTA.Demo.Domain;
using Materal.TTA.MySqlEFRepository;

namespace Materal.TTA.Demo.MySqlEFRepository
{
    public class TestDomainMySqlEFRepository : MySqlEFRepositoryImpl<TestDomain, Guid, TTADemoDBContext>, ITestDomainRepository
    {
        public TestDomainMySqlEFRepository(TTADemoDBContext dbContext) : base(dbContext)
        {
        }
    }
}
