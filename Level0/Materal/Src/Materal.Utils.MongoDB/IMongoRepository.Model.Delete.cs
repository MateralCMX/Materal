using MongoDB.Driver;

namespace Materal.Utils.MongoDB
{
    public partial interface IMongoRepository<T>
    {
        #region 删除多条
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<long> DeleteAsync(FilterDefinition<T> filter);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        long Delete(FilterDefinition<T> filter);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<long> DeleteAsync(Expression<Func<T, bool>> filter);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        long Delete(Expression<Func<T, bool>> filter);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        Task<long> DeleteAsync(FilterModel filterModel);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        long Delete(FilterModel filterModel);
        /// <summary>
        /// 清空数据
        /// </summary>
        /// <returns></returns>
        Task<long> ClearAsync();
        /// <summary>
        /// 清空数据
        /// </summary>
        /// <returns></returns>
        long Clear();
        #endregion
        #region 删除一条
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<long> DeleteOneAsync(FilterDefinition<T> filter);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        long DeleteOne(FilterDefinition<T> filter);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<long> DeleteOneAsync(Expression<Func<T, bool>> filter);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        long DeleteOne(Expression<Func<T, bool>> filter);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        Task<long> DeleteOneAsync(FilterModel filterModel);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        long DeleteOne(FilterModel filterModel);
        #endregion
    }
}
