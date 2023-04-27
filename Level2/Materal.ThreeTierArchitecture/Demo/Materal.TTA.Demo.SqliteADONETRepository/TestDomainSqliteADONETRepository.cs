using Materal.BusinessFlow.SqliteRepository.Repositories;
using Materal.TTA.ADONETRepository;
using Materal.TTA.Demo.Domain;

namespace Materal.TTA.Demo.SqliteADONETRepository
{
    public class TestDomainSqliteADONETRepository : SqliteADONETRepositoryImpl<TestDomain, Guid>, ITestDomainRepository
    {
        public TestDomainSqliteADONETRepository(IADONETUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
