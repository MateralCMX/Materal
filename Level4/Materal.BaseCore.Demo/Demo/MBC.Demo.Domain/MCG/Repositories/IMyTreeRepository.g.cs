using Materal.TTA.EFRepository;
using MBC.Core.Domain.Repositories;
using MBC.Demo.Enums;

namespace MBC.Demo.Domain.Repositories
{
    /// <summary>
    /// 我的树仓储接口
    /// </summary>
    public partial interface IMyTreeRepository : ICacheMBCRepository<MyTree, Guid>
    {
        /// <summary>
        /// 获取最大位序
        /// </summary>
        /// <returns></returns>
        Task<int> GetMaxIndexAsync();
    }
}
