using Materal.TTA.Common;
using Materal.TTA.SqliteEFRepository;
using Microsoft.EntityFrameworkCore;

namespace MMB.Core.Reposiroty
{
    /// <summary>
    /// MBC仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public abstract class MMBRepositoryImpl<T, TPrimaryKeyType, TDBContext>(TDBContext dbContext) : SqliteEFRepositoryImpl<T, TPrimaryKeyType, TDBContext>(dbContext), IMMBRepository<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
        where TDBContext : DbContext
    {
    }
}
