using Materal.ConfigCenter.ProtalServer.Domain;
using Materal.ConfigCenter.ProtalServer.Domain.Repositories;

namespace Materal.ConfigCenter.ProtalServer.SqliteEFRepository.RepositoryImpl
{
    public class ProjectRepositoryImpl : ProtalServerSqliteEFRepositoryImpl<Project>, IProjectRepository
    {
        public ProjectRepositoryImpl(ProtalServerDBContext dbContext) : base(dbContext)
        {
        }
    }
}
