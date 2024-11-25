using Materal.MergeBlock.Domain.Abstractions;
using Materal.TTA.Common;
using Materal.TTA.EFRepository;

namespace MMB.Demo.Abstractions
{
    /// <summary>
    /// MMB仓储接口
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public interface IMMBRepository<TDomain> : IEFRepository<TDomain, Guid>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
    {
    }
}