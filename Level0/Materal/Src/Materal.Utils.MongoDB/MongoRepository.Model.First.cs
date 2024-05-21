using MongoDB.Driver;

namespace Materal.Utils.MongoDB
{
    public partial class MongoRepository<T>
    {
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        public virtual async Task<T> FirstAsync(FilterDefinition<T> filter, FindOptions<T, T>? findOptions = null)
        {
            IAsyncCursor<T> cursor = await FindCursorAsync(filter, findOptions);
            T result = await cursor.FirstAsync();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        public virtual T First(FilterDefinition<T> filter, FindOptions<T, T>? findOptions = null)
        {
            IAsyncCursor<T> cursor = FindCursor(filter, findOptions);
            T result = cursor.First();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        public virtual async Task<T> FirstAsync(Expression<Func<T, bool>> filter, FindOptions<T, T>? findOptions = null)
        {
            IAsyncCursor<T> cursor = await FindCursorAsync(filter, findOptions);
            T result = await cursor.FirstAsync();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        public virtual T First(Expression<Func<T, bool>> filter, FindOptions<T, T>? findOptions = null)
        {
            IAsyncCursor<T> cursor = FindCursor(filter, findOptions);
            T result = cursor.First();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<T> FirstAsync(FilterDefinition<T> filter, SortDefinition<T> sort)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, sort, 0, 1);
            T result = await cursor.FirstAsync();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual T First(FilterDefinition<T> filter, SortDefinition<T> sort)
        {
            IAsyncCursor<T> cursor = RangeCursor(filter, sort, 0, 1);
            T result = cursor.First();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<T> FirstAsync(Expression<Func<T, bool>> filter, SortDefinition<T> sort)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, sort, 0, 1);
            T result = await cursor.FirstAsync();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual T First(Expression<Func<T, bool>> filter, SortDefinition<T> sort)
        {
            IAsyncCursor<T> cursor = RangeCursor(filter, sort, 0, 1);
            T result = cursor.First();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<T> FirstAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, sort, 0, 1);
            T result = await cursor.FirstAsync();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual T First(FilterDefinition<T> filter, Expression<Func<T, object>> sort)
        {
            IAsyncCursor<T> cursor = RangeCursor(filter, sort, 0, 1);
            T result = cursor.First();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual async Task<T> FirstAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, sort, sortOrder, 0, 1);
            T result = await cursor.FirstAsync();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual T First(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
        {
            IAsyncCursor<T> cursor = RangeCursor(filter, sort, sortOrder, 0, 1);
            T result = cursor.First();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<T> FirstAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, sort, 0, 1);
            T result = await cursor.FirstAsync();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual T First(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort)
        {
            IAsyncCursor<T> cursor = RangeCursor(filter, sort, 0, 1);
            T result = cursor.First();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual async Task<T> FirstAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, sort, sortOrder, 0, 1);
            T result = await cursor.FirstAsync();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual T First(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
        {
            IAsyncCursor<T> cursor = RangeCursor(filter, sort, sortOrder, 0, 1);
            T result = cursor.First();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual async Task<T> FirstAsync(FilterModel filterModel)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filterModel, 0, 1);
            T result = await cursor.FirstAsync();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual T First(FilterModel filterModel)
        {
            IAsyncCursor<T> cursor = RangeCursor(filterModel, 0, 1);
            T result = cursor.First();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<T> FirstAsync(FilterModel filterModel, SortDefinition<T> sort)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filterModel, sort, 0, 1);
            T result = await cursor.FirstAsync();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual T First(FilterModel filterModel, SortDefinition<T> sort)
        {
            IAsyncCursor<T> cursor = RangeCursor(filterModel, sort, 0, 1);
            T result = cursor.First();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<T> FirstAsync(FilterModel filterModel, Expression<Func<T, object>> sort)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filterModel, sort, 0, 1);
            T result = await cursor.FirstAsync();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual T First(FilterModel filterModel, Expression<Func<T, object>> sort)
        {
            IAsyncCursor<T> cursor = RangeCursor(filterModel, sort, 0, 1);
            T result = cursor.First();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual async Task<T> FirstAsync(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filterModel, sort, sortOrder, 0, 1);
            T result = await cursor.FirstAsync();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual T First(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
        {
            IAsyncCursor<T> cursor = RangeCursor(filterModel, sort, sortOrder, 0, 1);
            T result = cursor.First();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        public virtual async Task<T?> FirstOrDefaultAsync(FilterDefinition<T> filter, FindOptions<T, T>? findOptions = null)
        {
            IAsyncCursor<T> cursor = await FindCursorAsync(filter, findOptions);
            T? result = await cursor.FirstOrDefaultAsync();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        public virtual T? FirstOrDefault(FilterDefinition<T> filter, FindOptions<T, T>? findOptions = null)
        {
            IAsyncCursor<T> cursor = FindCursor(filter, findOptions);
            T? result = cursor.FirstOrDefault();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter, FindOptions<T, T>? findOptions = null)
        {
            IAsyncCursor<T> cursor = await FindCursorAsync(filter, findOptions);
            T? result = await cursor.FirstOrDefaultAsync();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        public virtual T? FirstOrDefault(Expression<Func<T, bool>> filter, FindOptions<T, T>? findOptions = null)
        {
            IAsyncCursor<T> cursor = FindCursor(filter, findOptions);
            T? result = cursor.FirstOrDefault();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<T?> FirstOrDefaultAsync(FilterDefinition<T> filter, SortDefinition<T> sort)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, sort, 0, 1);
            T? result = await cursor.FirstOrDefaultAsync();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual T? FirstOrDefault(FilterDefinition<T> filter, SortDefinition<T> sort)
        {
            IAsyncCursor<T> cursor = RangeCursor(filter, sort, 0, 1);
            T? result = cursor.FirstOrDefault();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter, SortDefinition<T> sort)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, sort, 0, 1);
            T? result = await cursor.FirstOrDefaultAsync();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual T? FirstOrDefault(Expression<Func<T, bool>> filter, SortDefinition<T> sort)
        {
            IAsyncCursor<T> cursor = RangeCursor(filter, sort, 0, 1);
            T? result = cursor.FirstOrDefault();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<T?> FirstOrDefaultAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, sort, 0, 1);
            T? result = await cursor.FirstOrDefaultAsync();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual T? FirstOrDefault(FilterDefinition<T> filter, Expression<Func<T, object>> sort)
        {
            IAsyncCursor<T> cursor = RangeCursor(filter, sort, 0, 1);
            T? result = cursor.FirstOrDefault();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual async Task<T?> FirstOrDefaultAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, sort, sortOrder, 0, 1);
            T? result = await cursor.FirstOrDefaultAsync();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual T? FirstOrDefault(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
        {
            IAsyncCursor<T> cursor = RangeCursor(filter, sort, sortOrder, 0, 1);
            T? result = cursor.FirstOrDefault();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, sort, 0, 1);
            T? result = await cursor.FirstOrDefaultAsync();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual T? FirstOrDefault(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort)
        {
            IAsyncCursor<T> cursor = RangeCursor(filter, sort, 0, 1);
            T? result = cursor.FirstOrDefault();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, sort, sortOrder, 0, 1);
            T? result = await cursor.FirstOrDefaultAsync();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual T? FirstOrDefault(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
        {
            IAsyncCursor<T> cursor = RangeCursor(filter, sort, sortOrder, 0, 1);
            T? result = cursor.FirstOrDefault();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual async Task<T?> FirstOrDefaultAsync(FilterModel filterModel)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filterModel, 0, 1);
            T? result = await cursor.FirstOrDefaultAsync();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual T? FirstOrDefault(FilterModel filterModel)
        {
            IAsyncCursor<T> cursor = RangeCursor(filterModel, 0, 1);
            T? result = cursor.FirstOrDefault();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<T?> FirstOrDefaultAsync(FilterModel filterModel, SortDefinition<T> sort)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filterModel, sort, 0, 1);
            T? result = await cursor.FirstOrDefaultAsync();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual T? FirstOrDefault(FilterModel filterModel, SortDefinition<T> sort)
        {
            IAsyncCursor<T> cursor = RangeCursor(filterModel, sort, 0, 1);
            T? result = cursor.FirstOrDefault();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<T?> FirstOrDefaultAsync(FilterModel filterModel, Expression<Func<T, object>> sort)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filterModel, sort, 0, 1);
            T? result = await cursor.FirstOrDefaultAsync();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual T? FirstOrDefault(FilterModel filterModel, Expression<Func<T, object>> sort)
        {
            IAsyncCursor<T> cursor = RangeCursor(filterModel, sort, 0, 1);
            T? result = cursor.FirstOrDefault();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual async Task<T?> FirstOrDefaultAsync(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filterModel, sort, sortOrder, 0, 1);
            T? result = await cursor.FirstOrDefaultAsync();
            return result;
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual T? FirstOrDefault(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
        {
            IAsyncCursor<T> cursor = RangeCursor(filterModel, sort, sortOrder, 0, 1);
            T? result = cursor.FirstOrDefault();
            return result;
        }
    }
}
