namespace RC.Core.Repository
{
    /// <summary>
    /// RC仓储
    /// </summary>
    public partial interface IRCCacheRepository<T, TPrimaryKeyType> : IRCRepository<T, TPrimaryKeyType>, ICacheEFRepository<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
    {
    }
}
