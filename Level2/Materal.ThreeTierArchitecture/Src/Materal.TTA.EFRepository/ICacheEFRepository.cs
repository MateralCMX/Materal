using Materal.TTA.Common;

namespace Materal.TTA.EFRepository
{
    /// <summary>
    /// 缓存EF仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public interface ICacheEFRepository<TEntity, in TPrimaryKeyType> : IEFRepository<TEntity, TPrimaryKeyType>
        where TEntity : class, IEntity<TPrimaryKeyType>
        where TPrimaryKeyType : struct
    {
        /// <summary>
        /// 通过缓存获得所有信息
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetAllInfoFromCacheAsync();
        /// <summary>
        /// 通过缓存获得所有信息
        /// </summary>
        /// <returns></returns>
        List<TEntity> GetAllInfoFromCache();
        /// <summary>
        /// 通过缓存获得信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetInfoFromCacheAsync(string key);
        /// <summary>
        /// 通过缓存获得信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        List<TEntity> GetInfoFromCache(string key);
        /// <summary>
        /// 清理缓存
        /// </summary>
        /// <returns></returns>
        Task ClearAllCacheAsync();
        /// <summary>
        /// 清理缓存
        /// </summary>
        /// <returns></returns>
        void ClearAllCache();
        /// <summary>
        /// 清理缓存
        /// </summary>
        /// <returns></returns>
        Task ClearCacheAsync(string key);
        /// <summary>
        /// 清理缓存
        /// </summary>
        /// <returns></returns>
        void ClearCache(string key);
    }
}
