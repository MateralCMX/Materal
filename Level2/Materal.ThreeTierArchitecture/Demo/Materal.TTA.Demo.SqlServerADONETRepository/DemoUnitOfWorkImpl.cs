using Materal.TTA.Demo.Domain;
using Materal.TTA.SqlServerADONETRepository;

namespace Materal.TTA.Demo.SqlServerADONETRepository
{
    public class DemoUnitOfWorkImpl : SqlServerADONETUnitOfWorkImpl<DemoDBConfig, Guid>, IDemoUnitOfWork
    {
        public DemoUnitOfWorkImpl(IServiceProvider serviceProvider, DemoDBConfig dbConfig) : base(serviceProvider, dbConfig)
        {
        }
    }
}
