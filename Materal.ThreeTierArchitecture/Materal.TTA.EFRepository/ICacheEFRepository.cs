using Materal.TTA.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Materal.TTA.EFRepository
{
    public interface ICacheEFRepository<T, in TPrimaryKeyType> : IEFRepository<T, TPrimaryKeyType> where T : class, IEntity<TPrimaryKeyType>
    {
        /// <summary>
        /// 通过缓存获得所有信息
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAllInfoFromCacheAsync();
        /// <summary>
        /// 清理缓存
        /// </summary>
        /// <returns></returns>
        Task ClearCacheAsync();
    }
}
