using Materal.TTA.Common;

namespace Materal.TTA.EFRepository
{
    public interface ICacheEFRepository<T, in TPrimaryKeyType> : IEFRepository<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>
        where TPrimaryKeyType : struct
    {
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
        Task<List<T>> GetInfoFromCacheAsync(string key);
        /// <summary>
        /// 清理缓存
        /// </summary>
        /// <returns></returns>
        Task ClearAllCacheAsync();
        /// <summary>
        /// 清理缓存
        /// </summary>
        /// <returns></returns>
        Task ClearCacheAsync(string key);
    }
}
