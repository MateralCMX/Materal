using Materal.TTA.Demo.Domain;
using Materal.TTA.SqliteADONETRepository;

namespace Materal.TTA.Demo.SqliteADONETRepository
{
    public class DemoUnitOfWorkImpl : SqliteADONETUnitOfWorkImpl<DemoDBOption, Guid>, IDemoUnitOfWork
    {
        public DemoUnitOfWorkImpl(IServiceProvider serviceProvider, DemoDBOption dbConfig) : base(serviceProvider, dbConfig)
        {
        }
    }
}
