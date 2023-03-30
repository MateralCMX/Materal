using Materal.TTA.Common;
using Materal.TTA.SqliteRepository;
using Microsoft.EntityFrameworkCore;

namespace MBC.Core.EFRepository
{
    /// <summary>
    /// MBC仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TDBContext"></typeparam>
    public abstract class MBCEFRepositoryImpl<T, TDBContext> : SqliteEFRepositoryImpl<T, Guid, TDBContext>
        where T : class, IEntity<Guid>, new()
        where TDBContext : DbContext
    {
        protected MBCEFRepositoryImpl(TDBContext dbContext) : base(dbContext)
        {
        }
    }
}
