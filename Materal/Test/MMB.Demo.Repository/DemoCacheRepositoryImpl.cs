using Materal.MergeBlock.Domain.Abstractions;
using Materal.TTA.Common;
using Materal.Utils.Cache;
using MMB.Demo.Abstractions;

namespace MMB.Demo.Repository
{
    /// <summary>
    /// Demo缓存仓储实现
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public abstract class DemoCacheRepositoryImpl<TDomain>(DemoDBContext dbContext, ICacheHelper cacheHelper) : MMBCacheRepositoryImpl<TDomain, DemoDBContext>(dbContext, cacheHelper), IDemoCacheRepository<TDomain>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
    {
    }
}