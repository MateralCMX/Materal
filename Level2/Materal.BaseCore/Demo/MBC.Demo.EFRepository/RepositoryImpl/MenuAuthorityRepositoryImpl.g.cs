using MBC.Demo.Domain;
using MBC.Demo.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MBC.Demo.EFRepository.RepositoryImpl
{
    /// <summary>
    /// 仓储实现
    /// </summary>
    public partial class MenuAuthorityRepositoryImpl: DemoRepositoryImpl<MenuAuthority>, IMenuAuthorityRepository
    {
        public MenuAuthorityRepositoryImpl(DemoDBContext dbContext) : base(dbContext) { }

        /// <summary>
        /// 获取最大位序
        /// </summary>
        /// <param name="subSystemID"></param>
        /// <returns></returns>
        public async Task<int> GetMaxIndexAsync(Guid subSystemID)
        {
            if (!DBSet.Any(m => m.SubSystemID == subSystemID)) return -1;
            int result = await DBSet.Where(m => m.SubSystemID == subSystemID).MaxAsync(m => m.Index);
            return result;
        }
    }
}
