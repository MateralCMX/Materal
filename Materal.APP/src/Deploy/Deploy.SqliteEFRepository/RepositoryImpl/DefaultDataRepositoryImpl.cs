using Deploy.Domain;
using Deploy.Domain.Repositories;

namespace Deploy.SqliteEFRepository.RepositoryImpl
{
    public class DefaultDataRepositoryImpl : DeploySqliteEFRepositoryImpl<DefaultData>, IDefaultDataRepository
    {
        public DefaultDataRepositoryImpl(DeployDBContext dbContext) : base(dbContext)
        {
        }
    }
}
