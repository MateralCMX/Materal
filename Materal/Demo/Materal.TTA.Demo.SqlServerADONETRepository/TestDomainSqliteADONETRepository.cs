using Materal.TTA.ADONETRepository;
using Materal.TTA.Demo.Domain;
using Materal.TTA.SqlServerADONETRepository;

namespace Materal.TTA.Demo.SqlServerADONETRepository
{
    public class TestDomainSqlServerADONETRepository : SqlServerADONETRepositoryImpl<TestDomain, Guid>, ITestDomainRepository
    {
        public TestDomainSqlServerADONETRepository(IADONETUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
