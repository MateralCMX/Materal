using Materal.BusinessFlow.Abstractions.Domain;
using Materal.TTA.Common;

namespace Materal.BusinessFlow.Abstractions.Repositories
{
    public interface IFlowUserRepository : IRepository<FlowUser, Guid>
    {
        /// <summary>
        /// 获得用户参与流程模版唯一标识
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        List<Guid> GetUserFlowTemplateIDs(Guid userID);
    }
}
