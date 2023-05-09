using Materal.BusinessFlow.Abstractions;
using Materal.TTA.SqlServerADONETRepository;

namespace Materal.BusinessFlow.SqlServerRepository
{
    public class BusinessFlowUnitOfWorkImpl : SqlServerADONETUnitOfWorkImpl<BusinessFlowDBOption, Guid>, IBusinessFlowUnitOfWork
    {
        public BusinessFlowUnitOfWorkImpl(IServiceProvider serviceProvider, BusinessFlowDBOption dbOption) : base(serviceProvider, dbOption)
        {
        }
        /// <summary>
        /// 获得参数
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public string GetParams(string paramName) => SqlServerRepositoryHelper.GetParams(paramName);
    }
}
