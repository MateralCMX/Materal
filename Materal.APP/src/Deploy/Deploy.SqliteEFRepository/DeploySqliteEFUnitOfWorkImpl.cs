using Materal.TTA.SqliteRepository;

namespace Deploy.SqliteEFRepository
{
    public class DeploySqliteEFUnitOfWorkImpl : SqliteEFUnitOfWorkImpl<DeployDBContext>, IDeploySqliteEFUnitOfWork
    {
        public DeploySqliteEFUnitOfWorkImpl(DeployDBContext dbContext) : base(dbContext)
        {
        }
    }
}
