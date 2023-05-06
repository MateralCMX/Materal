namespace Materal.TTA.Common
{
    /// <summary>
    /// 缓存读写分离仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICacheSubordinateRepository<T> : ICacheRepository<T>, ISubordinateRepository<T>
        where T : class, IEntity
    {
        /// <summary>
        /// 通过读库缓存获得所有信息
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAllInfoFromSubordinateCacheAsync();
        /// <summary>
        /// 通过读库缓存获得信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<List<T>> GetInfoFromSubordinateCacheAsync(string key);
    }
    /// <summary>
    /// 缓存读写分离仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public interface ICacheSubordinateRepository<T, TPrimaryKeyType> : ICacheSubordinateRepository<T>, ISubordinateRepository<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>
        where TPrimaryKeyType : struct
    {
    }
}
