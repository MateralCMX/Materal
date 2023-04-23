using Materal.TTA.EFRepository;

namespace MBC.Demo.Domain.Repositories
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    public partial interface IMenuAuthorityRepository : IEFRepository<MenuAuthority, Guid>
    {
        /// <summary>
        /// 获取最大位序
        /// </summary>
        /// <param name="subSystemID"></param>
        /// <returns></returns>
        Task<int> GetMaxIndexAsync(Guid subSystemID);
    }
}
