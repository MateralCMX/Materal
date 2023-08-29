using Materal.TTA.Common;
using Materal.TTA.SqliteEFRepository;
using Microsoft.EntityFrameworkCore;
using RC.Core.Domain.Repositories;

namespace RC.Core.EFRepository
{
    /// <summary>
    /// RC仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public abstract class RCEFRepositoryImpl<T, TPrimaryKeyType, TDBContext> : SqliteEFRepositoryImpl<T, TPrimaryKeyType, TDBContext>, IRCRepository<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
        where TDBContext : DbContext
    {
        protected RCEFRepositoryImpl(TDBContext dbContext) : base(dbContext)
        {
        }
    }
}
