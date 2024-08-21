namespace Materal.TTA.EFRepository
{
    /// <summary>
    /// 缓存EF仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public interface ICacheEFRepository<TEntity, in TPrimaryKeyType> : IEFRepository<TEntity, TPrimaryKeyType>, ICacheRepository<TEntity, TPrimaryKeyType>
        where TEntity : class, IEntity<TPrimaryKeyType>
        where TPrimaryKeyType : struct
    {
    }
}
