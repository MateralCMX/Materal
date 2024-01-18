using MongoDB.Driver;

namespace Materal.Utils.MongoDB
{
    public partial interface IMongoRepository<T>
    {
        /// <summary>
        /// 是否存在数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(FilterDefinition<T> filter);
        /// <summary>
        /// 是否存在数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        bool Any(FilterDefinition<T> filter);
        /// <summary>
        /// 是否存在数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
        /// <summary>
        /// 是否存在数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        bool Any(Expression<Func<T, bool>> filter);
        /// <summary>
        /// 是否存在数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(FilterModel filterModel);
        /// <summary>
        /// 是否存在数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        bool Any(FilterModel filterModel);
    }
}
