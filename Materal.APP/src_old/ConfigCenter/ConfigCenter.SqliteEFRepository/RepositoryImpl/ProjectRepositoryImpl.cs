using ConfigCenter.Domain;
using ConfigCenter.Domain.Repositories;

namespace ConfigCenter.SqliteEFRepository.RepositoryImpl
{
    public class ProjectRepositoryImpl : ConfigCenterSqliteEFRepositoryImpl<Project>, IProjectRepository
    {
        public ProjectRepositoryImpl(ConfigCenterDBContext dbContext) : base(dbContext)
        {
        }
    }
}
