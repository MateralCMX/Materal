using Materal.ConfigCenter.ProtalServer.Domain;
using Materal.ConfigCenter.ProtalServer.Domain.Repositories;

namespace Materal.ConfigCenter.ProtalServer.SqliteEFRepository.RepositoryImpl
{
    public class NamespaceRepositoryImpl : ProtalServerSqliteEFRepositoryImpl<Namespace>, INamespaceRepository
    {
        public NamespaceRepositoryImpl(ProtalServerDBContext dbContext) : base(dbContext)
        {
        }
    }
}
