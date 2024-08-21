using MongoDB.Driver;

namespace Materal.Utils.MongoDB
{
    public partial interface IMongoRepository<T>
    {
        #region 替换
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<long> UpdateAsync(FilterDefinition<T> filter, T data);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        long Update(FilterDefinition<T> filter, T data);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<long> UpdateAsync(Expression<Func<T, bool>> filter, T data);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        long Update(Expression<Func<T, bool>> filter, T data);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<long> UpdateAsync(FilterModel filterModel, T data);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        long Update(FilterModel filterModel, T data);
        #endregion
        #region 更新多条数据
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        Task<long> UpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> update);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        long Update(FilterDefinition<T> filter, UpdateDefinition<T> update);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        Task<long> UpdateAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        long Update(Expression<Func<T, bool>> filter, UpdateDefinition<T> update);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        Task<long> UpdateAsync(FilterModel filterModel, UpdateDefinition<T> update);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        long Update(FilterModel filterModel, UpdateDefinition<T> update);
        #endregion
        #region 更新一条数据
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        Task<long> UpdateOneAsync(FilterDefinition<T> filter, UpdateDefinition<T> update);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        long UpdateOne(FilterDefinition<T> filter, UpdateDefinition<T> update);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        Task<long> UpdateOneAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        long UpdateOne(Expression<Func<T, bool>> filter, UpdateDefinition<T> update);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        Task<long> UpdateOneAsync(FilterModel filterModel, UpdateDefinition<T> update);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        long UpdateOne(FilterModel filterModel, UpdateDefinition<T> update);
        #endregion
    }
}
