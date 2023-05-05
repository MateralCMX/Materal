using Materal.TTA.Common;
using Materal.TTA.SqliteEFRepository;
using Microsoft.EntityFrameworkCore;

namespace MBC.Core.EFRepository
{
    /// <summary>
    /// MBC仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public abstract class MBCEFRepositoryImpl<T, TPrimaryKeyType, TDBContext> : SqliteEFRepositoryImpl<T, TPrimaryKeyType, TDBContext>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
        where TDBContext : DbContext
    {
        protected MBCEFRepositoryImpl(TDBContext dbContext) : base(dbContext)
        {
        }
    }
}
