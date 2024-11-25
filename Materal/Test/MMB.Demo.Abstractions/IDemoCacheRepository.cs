using Materal.MergeBlock.Domain.Abstractions;
using Materal.TTA.Common;

namespace MMB.Demo.Abstractions
{
    /// <summary>
    /// Demo缓存仓储接口
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public interface IDemoCacheRepository<TDomain> : IMMBCacheRepository<TDomain>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
    {
    }
}