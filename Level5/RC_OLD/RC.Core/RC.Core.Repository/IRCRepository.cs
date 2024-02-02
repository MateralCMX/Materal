namespace RC.Core.Repository
{
    /// <summary>
    /// RC仓储
    /// </summary>
    public partial interface IRCRepository<T, TPrimaryKeyType> : IEFRepository<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
    {
    }
}
