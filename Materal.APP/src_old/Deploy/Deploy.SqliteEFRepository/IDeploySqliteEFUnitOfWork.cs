using Materal.TTA.EFRepository;

namespace Deploy.SqliteEFRepository
{
    public interface IDeploySqliteEFUnitOfWork: IEFUnitOfWork<DeployDBContext>
    {
    }
}
