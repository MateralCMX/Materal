using MongoDB.Driver;

namespace Materal.Utils.MongoDB
{
    public partial interface IMongoRepository<T>
    {
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        Task<List<T>> FindAsync(FilterDefinition<T> filter, FindOptions<T, T>? findOptions = null);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        List<T> Find(FilterDefinition<T> filter, FindOptions<T, T>? findOptions = null);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        Task<List<T>> FindAsync(Expression<Func<T, bool>> filter, FindOptions<T, T>? findOptions = null);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        List<T> Find(Expression<Func<T, bool>> filter, FindOptions<T, T>? findOptions = null);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<List<T>> FindAsync(FilterDefinition<T> filter, SortDefinition<T> sort);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        List<T> Find(FilterDefinition<T> filter, SortDefinition<T> sort);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<List<T>> FindAsync(Expression<Func<T, bool>> filter, SortDefinition<T> sort);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        List<T> Find(Expression<Func<T, bool>> filter, SortDefinition<T> sort);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<List<T>> FindAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        List<T> Find(FilterDefinition<T> filter, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        Task<List<T>> FindAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        List<T> Find(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<List<T>> FindAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        List<T> Find(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        Task<List<T>> FindAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        List<T> Find(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        Task<List<T>> FindAsync(FilterModel filterModel);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        List<T> Find(FilterModel filterModel);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<List<T>> FindAsync(FilterModel filterModel, SortDefinition<T> sort);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        List<T> Find(FilterModel filterModel, SortDefinition<T> sort);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<List<T>> FindAsync(FilterModel filterModel, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        List<T> Find(FilterModel filterModel, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        Task<List<T>> FindAsync(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        List<T> Find(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
    }
}
