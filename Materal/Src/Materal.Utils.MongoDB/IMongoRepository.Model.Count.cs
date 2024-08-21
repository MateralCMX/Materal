using MongoDB.Driver;

namespace Materal.Utils.MongoDB
{
    public partial interface IMongoRepository<T>
    {
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<long> CountAsync(FilterDefinition<T> filter);
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        long Count(FilterDefinition<T> filter);
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<long> CountAsync(Expression<Func<T, bool>> filter);
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        long Count(Expression<Func<T, bool>> filter);
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        Task<long> CountAsync(FilterModel filterModel);
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        long Count(FilterModel filterModel);
    }
}
