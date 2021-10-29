using ConfigCenter.Domain;
using ConfigCenter.Domain.Repositories;

namespace ConfigCenter.SqliteEFRepository.RepositoryImpl
{
    public class NamespaceRepositoryImpl : ConfigCenterSqliteEFRepositoryImpl<Namespace>, INamespaceRepository
    {
        public NamespaceRepositoryImpl(ConfigCenterDBContext dbContext) : base(dbContext)
        {
        }
    }
}
