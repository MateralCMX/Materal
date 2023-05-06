using Materal.BusinessFlow.Abstractions;
using Materal.TTA.SqliteADONETRepository;

namespace Materal.BusinessFlow.SqliteRepository
{
    public class BusinessFlowUnitOfWorkImpl : SqliteADONETUnitOfWorkImpl<BusinessFlowSqliteDBOption, Guid>, IBusinessFlowUnitOfWork
    {
        public BusinessFlowUnitOfWorkImpl(IServiceProvider serviceProvider, BusinessFlowSqliteDBOption dbOption) : base(serviceProvider, dbOption)
        {
        }
        /// <summary>
        /// 获得参数
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public string GetParams(string paramName) => SqliteRepositoryHelper.GetParams(paramName);
    }
}
