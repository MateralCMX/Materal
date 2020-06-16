using Materal.TTA.EFRepository;

namespace Materal.ConfigCenter.ConfigServer.SqliteEFRepository
{
    public interface IConfigServerUnitOfWork: IEFUnitOfWork<ConfigServerDBContext>
    {
    }
}
