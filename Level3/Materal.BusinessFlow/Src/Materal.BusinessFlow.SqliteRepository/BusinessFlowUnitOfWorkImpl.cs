using Materal.BusinessFlow.Abstractions;
using Materal.TTA.SqliteADONETRepository;

namespace Materal.BusinessFlow.SqliteRepository
{
    public class BusinessFlowUnitOfWorkImpl : SqliteADONETUnitOfWorkImpl<SqliteDBOption, Guid>, IBusinessFlowUnitOfWork
    {
        public BusinessFlowUnitOfWorkImpl(IServiceProvider serviceProvider, SqliteDBOption dbOption) : base(serviceProvider, dbOption)
        {
        }
    }
}
