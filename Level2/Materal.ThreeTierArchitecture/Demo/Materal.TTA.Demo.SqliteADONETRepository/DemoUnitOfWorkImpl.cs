using Materal.TTA.Demo.Domain;
using Materal.TTA.SqliteADONETRepository;

namespace Materal.TTA.Demo.SqliteADONETRepository
{
    public class DemoUnitOfWorkImpl : SqliteADONETUnitOfWorkImpl<DemoDBConfig, Guid>, IDemoUnitOfWork
    {
        public DemoUnitOfWorkImpl(IServiceProvider serviceProvider, DemoDBConfig dbConfig) : base(serviceProvider, dbConfig)
        {
        }
    }
}
