using Microsoft.EntityFrameworkCore;
using MBC.Core.EFRepository;
using MBC.Demo.Domain;
using MBC.Demo.Domain.Repositories;
using MBC.Demo.Enums;
using Materal.Utils.Cache;

namespace MBC.Demo.EFRepository.RepositoryImpl
{
    /// <summary>
    /// 我的树仓储实现
    /// </summary>
    public partial class MyTreeRepositoryImpl: MBCCacheRepositoryImpl<MyTree, Guid, DemoDBContext>, IMyTreeRepository
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public MyTreeRepositoryImpl(DemoDBContext dbContext, ICacheHelper cacheManager) : base(dbContext, cacheManager) { }
        /// <summary>
        /// 获得所有缓存名称
        /// </summary>
        protected override string GetAllCacheName() => "AllMyTree";
        /// <summary>
        /// 获取最大位序
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetMaxIndexAsync()
        {
            if (!await DBSet.AnyAsync()) return -1;
            int result = await DBSet.MaxAsync(m => m.Index);
            return result;
        }
    }
}
