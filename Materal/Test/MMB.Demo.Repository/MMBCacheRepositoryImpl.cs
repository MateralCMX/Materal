using Materal.MergeBlock.Domain.Abstractions;
using Materal.TTA.Common;
using Materal.TTA.SqliteEFRepository;
using Materal.Utils.Cache;
using Microsoft.EntityFrameworkCore;
using MMB.Demo.Abstractions;

namespace MMB.Demo.Repository
{
    /// <summary>
    /// MMB缓存仓储实现
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    /// <typeparam name="TDBContext"></typeparam>
    public abstract class MMBCacheRepositoryImpl<TDomain, TDBContext>(TDBContext dbContext, ICacheHelper cacheHelper) : SqliteCacheEFRepositoryImpl<TDomain, Guid, TDBContext>(dbContext, cacheHelper), IMMBCacheRepository<TDomain>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
        where TDBContext : DbContext
    {
    }
}