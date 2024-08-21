namespace Materal.TTA.Common
{
    /// <summary>
    /// 缓存仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICacheRepository<T> : IRepository<T>
        where T : class, IEntity
    {
        /// <summary>
        /// 通过缓存获得所有信息
        /// </summary>
        /// <returns></returns>
        List<T> GetAllInfoFromCache();
        /// <summary>
        /// 通过缓存获得所有信息
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAllInfoFromCacheAsync();
        /// <summary>
        /// 通过缓存获得信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        List<T> GetInfoFromCache(string key);
        /// <summary>
        /// 通过缓存获得信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<List<T>> GetInfoFromCacheAsync(string key);
        /// <summary>
        /// 清理缓存
        /// </summary>
        /// <returns></returns>
        void ClearAllCache();
        /// <summary>
        /// 清理缓存
        /// </summary>
        /// <returns></returns>
        Task ClearAllCacheAsync();
        /// <summary>
        /// 清理缓存
        /// </summary>
        /// <returns></returns>
        void ClearCache(string key);
        /// <summary>
        /// 清理缓存
        /// </summary>
        /// <returns></returns>
        Task ClearCacheAsync(string key);
    }
    /// <summary>
    /// 缓存仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public interface ICacheRepository<T, in TPrimaryKeyType> : ICacheRepository<T>, IRepository<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>
        where TPrimaryKeyType : struct
    {
    }
}
