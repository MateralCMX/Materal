namespace Materal.TTA.EFRepository
{
    /// <summary>
    /// EF读写分离缓存仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public interface ICacheEFSubordinateRepository<TEntity, TPrimaryKeyType> : ICacheSubordinateRepository<TEntity, TPrimaryKeyType>
        where TEntity : class, IEntity<TPrimaryKeyType>
        where TPrimaryKeyType : struct
    {
    }
}
