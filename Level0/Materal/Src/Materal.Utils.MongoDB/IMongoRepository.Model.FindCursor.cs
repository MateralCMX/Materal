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
        Task<IAsyncCursor<T>> FindCursorAsync(FilterDefinition<T> filter, FindOptions<T, T>? findOptions = null);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        IAsyncCursor<T> FindCursor(FilterDefinition<T> filter, FindOptions<T, T>? findOptions = null);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> FindCursorAsync(Expression<Func<T, bool>> filter, FindOptions<T, T>? findOptions = null);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        IAsyncCursor<T> FindCursor(Expression<Func<T, bool>> filter, FindOptions<T, T>? findOptions = null);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> FindCursorAsync(FilterDefinition<T> filter, SortDefinition<T> sort);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        IAsyncCursor<T> FindCursor(FilterDefinition<T> filter, SortDefinition<T> sort);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> FindCursorAsync(Expression<Func<T, bool>> filter, SortDefinition<T> sort);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        IAsyncCursor<T> FindCursor(Expression<Func<T, bool>> filter, SortDefinition<T> sort);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> FindCursorAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        IAsyncCursor<T> FindCursor(FilterDefinition<T> filter, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> FindCursorAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        IAsyncCursor<T> FindCursor(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> FindCursorAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        IAsyncCursor<T> FindCursor(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> FindCursorAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        IAsyncCursor<T> FindCursor(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> FindCursorAsync(FilterModel filterModel);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        IAsyncCursor<T> FindCursor(FilterModel filterModel);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> FindCursorAsync(FilterModel filterModel, SortDefinition<T> sort);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        IAsyncCursor<T> FindCursor(FilterModel filterModel, SortDefinition<T> sort);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> FindCursorAsync(FilterModel filterModel, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        IAsyncCursor<T> FindCursor(FilterModel filterModel, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> FindCursorAsync(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        IAsyncCursor<T> FindCursor(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
    }
}
