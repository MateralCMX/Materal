using Materal.MergeBlock.Domain.Abstractions;
using Materal.TTA.Common;
using Materal.TTA.SqliteEFRepository;
using Microsoft.EntityFrameworkCore;
using MMB.Demo.Abstractions;

namespace MMB.Demo.Repository
{
    /// <summary>
    /// MMB仓储实现
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    /// <typeparam name="TDBContext"></typeparam>
    public abstract class MMBRepositoryImpl<TDomain, TDBContext>(TDBContext dbContext) : SqliteEFRepositoryImpl<TDomain, Guid, TDBContext>(dbContext), IMMBRepository<TDomain>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
        where TDBContext : DbContext
    {
    }
}