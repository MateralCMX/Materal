using MongoDB.Driver;

namespace Materal.Utils.MongoDB
{
    public partial interface IMongoRepository<T>
    {
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        Task<T> FirstAsync(FilterDefinition<T> filter, FindOptions<T, T>? findOptions = null);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        T First(FilterDefinition<T> filter, FindOptions<T, T>? findOptions = null);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        Task<T> FirstAsync(Expression<Func<T, bool>> filter, FindOptions<T, T>? findOptions = null);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        T First(Expression<Func<T, bool>> filter, FindOptions<T, T>? findOptions = null);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<T> FirstAsync(FilterDefinition<T> filter, SortDefinition<T> sort);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        T First(FilterDefinition<T> filter, SortDefinition<T> sort);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<T> FirstAsync(Expression<Func<T, bool>> filter, SortDefinition<T> sort);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        T First(Expression<Func<T, bool>> filter, SortDefinition<T> sort);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<T> FirstAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        T First(FilterDefinition<T> filter, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        Task<T> FirstAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        T First(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<T> FirstAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        T First(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        Task<T> FirstAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        T First(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        Task<T> FirstAsync(FilterModel filterModel);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        T First(FilterModel filterModel);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<T> FirstAsync(FilterModel filterModel, SortDefinition<T> sort);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        T First(FilterModel filterModel, SortDefinition<T> sort);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<T> FirstAsync(FilterModel filterModel, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        T First(FilterModel filterModel, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        Task<T> FirstAsync(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        T First(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        Task<T?> FirstOrDefaultAsync(FilterDefinition<T> filter, FindOptions<T, T>? findOptions = null);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        T? FirstOrDefault(FilterDefinition<T> filter, FindOptions<T, T>? findOptions = null);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter, FindOptions<T, T>? findOptions = null);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        T? FirstOrDefault(Expression<Func<T, bool>> filter, FindOptions<T, T>? findOptions = null);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<T?> FirstOrDefaultAsync(FilterDefinition<T> filter, SortDefinition<T> sort);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        T? FirstOrDefault(FilterDefinition<T> filter, SortDefinition<T> sort);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter, SortDefinition<T> sort);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        T? FirstOrDefault(Expression<Func<T, bool>> filter, SortDefinition<T> sort);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<T?> FirstOrDefaultAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        T? FirstOrDefault(FilterDefinition<T> filter, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        Task<T?> FirstOrDefaultAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        T? FirstOrDefault(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        T? FirstOrDefault(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        T? FirstOrDefault(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        Task<T?> FirstOrDefaultAsync(FilterModel filterModel);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        T? FirstOrDefault(FilterModel filterModel);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<T?> FirstOrDefaultAsync(FilterModel filterModel, SortDefinition<T> sort);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        T? FirstOrDefault(FilterModel filterModel, SortDefinition<T> sort);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<T?> FirstOrDefaultAsync(FilterModel filterModel, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        T? FirstOrDefault(FilterModel filterModel, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        Task<T?> FirstOrDefaultAsync(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        T? FirstOrDefault(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
    }
}
