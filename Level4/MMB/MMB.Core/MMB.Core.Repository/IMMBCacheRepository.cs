namespace MMB.Core.Reposiroty
{
    /// <summary>
    /// MMB仓储
    /// </summary>
    public partial interface IMMBCacheRepository<T, TPrimaryKeyType> : IMMBRepository<T, TPrimaryKeyType>, ICacheEFRepository<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
    {
    }
}
