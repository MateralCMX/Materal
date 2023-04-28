using Materal.TTA.Demo.Domain;
using Materal.TTA.SqlServerADONETRepository;

namespace Materal.TTA.Demo.SqlServerADONETRepository
{
    public class DemoUnitOfWorkImpl : SqlServerADONETUnitOfWorkImpl<DemoDBOption, Guid>, IDemoUnitOfWork
    {
        public DemoUnitOfWorkImpl(IServiceProvider serviceProvider, DemoDBOption dbConfig) : base(serviceProvider, dbConfig)
        {
        }
    }
}
