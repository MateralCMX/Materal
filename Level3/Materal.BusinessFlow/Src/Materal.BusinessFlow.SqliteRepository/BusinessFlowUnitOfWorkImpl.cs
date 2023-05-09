using Materal.BusinessFlow.Abstractions;
using Materal.TTA.SqliteADONETRepository;

namespace Materal.BusinessFlow.SqliteRepository
{
    public class BusinessFlowUnitOfWorkImpl : SqliteADONETUnitOfWorkImpl<BusinessFlowDBOption, Guid>, IBusinessFlowUnitOfWork
    {
        public BusinessFlowUnitOfWorkImpl(IServiceProvider serviceProvider, BusinessFlowDBOption dbOption) : base(serviceProvider, dbOption)
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
