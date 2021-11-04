using Deploy.Domain;
using Deploy.Domain.Repositories;

namespace Deploy.SqliteEFRepository.RepositoryImpl
{
    public class ApplicationInfoRepositoryImpl : DeploySqliteEFRepositoryImpl<ApplicationInfo>, IApplicationInfoRepository
    {
        public ApplicationInfoRepositoryImpl(DeployDBContext dbContext) : base(dbContext)
        {
        }
    }
}
