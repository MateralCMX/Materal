using Materal.TTA.SqliteEFRepository;

namespace RC.Core.Repository
{
    /// <summary>
    /// RC仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    /// <typeparam name="TDBContext"></typeparam>
    public abstract class RCRepositoryImpl<T, TPrimaryKeyType, TDBContext>(TDBContext dbContext) : SqliteEFRepositoryImpl<T, TPrimaryKeyType, TDBContext>(dbContext), IRCRepository<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
        where TDBContext : DbContext
    {
    }
}
