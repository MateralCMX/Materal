using MongoDB.Driver;

namespace Materal.Utils.MongoDB
{
    public partial class MongoRepository<T>
    {
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> FindAsync(FilterDefinition<T> filter, FindOptions<T, T>? findOptions = null)
        {
            IAsyncCursor<T> cursor = await FindCursorAsync(filter, findOptions);
            List<T> result = await cursor.ToListAsync();
            return result;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        public virtual List<T> Find(FilterDefinition<T> filter, FindOptions<T, T>? findOptions = null)
        {
            IAsyncCursor<T> cursor = FindCursor(filter, findOptions);
            List<T> result = cursor.ToList();
            return result;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> filter, FindOptions<T, T>? findOptions = null)
        {
            IAsyncCursor<T> cursor = await FindCursorAsync(filter, findOptions);
            List<T> result = await cursor.ToListAsync();
            return result;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        public virtual List<T> Find(Expression<Func<T, bool>> filter, FindOptions<T, T>? findOptions = null)
        {
            IAsyncCursor<T> cursor = FindCursor(filter, findOptions);
            List<T> result = cursor.ToList();
            return result;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> FindAsync(FilterDefinition<T> filter, SortDefinition<T> sort)
        {
            IAsyncCursor<T> cursor = await FindCursorAsync(filter, sort);
            List<T> result = await cursor.ToListAsync();
            return result;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual List<T> Find(FilterDefinition<T> filter, SortDefinition<T> sort)
        {
            IAsyncCursor<T> cursor = FindCursor(filter, sort);
            List<T> result = cursor.ToList();
            return result;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> filter, SortDefinition<T> sort)
        {
            IAsyncCursor<T> cursor = await FindCursorAsync(filter, sort);
            List<T> result = await cursor.ToListAsync();
            return result;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual List<T> Find(Expression<Func<T, bool>> filter, SortDefinition<T> sort)
        {
            IAsyncCursor<T> cursor = FindCursor(filter, sort);
            List<T> result = cursor.ToList();
            return result;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> FindAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort)
        {
            IAsyncCursor<T> cursor = await FindCursorAsync(filter, sort);
            List<T> result = await cursor.ToListAsync();
            return result;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual List<T> Find(FilterDefinition<T> filter, Expression<Func<T, object>> sort)
        {
            IAsyncCursor<T> cursor = FindCursor(filter, sort);
            List<T> result = cursor.ToList();
            return result;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> FindAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
        {
            IAsyncCursor<T> cursor = await FindCursorAsync(filter, sort, sortOrder);
            List<T> result = await cursor.ToListAsync();
            return result;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual List<T> Find(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
        {
            IAsyncCursor<T> cursor = FindCursor(filter, sort, sortOrder);
            List<T> result = cursor.ToList();
            return result;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort)
        {
            IAsyncCursor<T> cursor = await FindCursorAsync(filter, sort);
            List<T> result = await cursor.ToListAsync();
            return result;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual List<T> Find(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort)
        {
            IAsyncCursor<T> cursor = FindCursor(filter, sort);
            List<T> result = cursor.ToList();
            return result;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
        {
            IAsyncCursor<T> cursor = await FindCursorAsync(filter, sort, sortOrder);
            List<T> result = await cursor.ToListAsync();
            return result;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual List<T> Find(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
        {
            IAsyncCursor<T> cursor = FindCursor(filter, sort, sortOrder);
            List<T> result = cursor.ToList();
            return result;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> FindAsync(FilterModel filterModel)
        {
            IAsyncCursor<T> cursor = await FindCursorAsync(filterModel);
            List<T> result = await cursor.ToListAsync();
            return result;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual List<T> Find(FilterModel filterModel)
        {
            IAsyncCursor<T> cursor = FindCursor(filterModel);
            List<T> result = cursor.ToList();
            return result;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> FindAsync(FilterModel filterModel, SortDefinition<T> sort)
        {
            IAsyncCursor<T> cursor = await FindCursorAsync(filterModel, sort);
            List<T> result = await cursor.ToListAsync();
            return result;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual List<T> Find(FilterModel filterModel, SortDefinition<T> sort)
        {
            IAsyncCursor<T> cursor = FindCursor(filterModel, sort);
            List<T> result = cursor.ToList();
            return result;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> FindAsync(FilterModel filterModel, Expression<Func<T, object>> sort)
        {
            IAsyncCursor<T> cursor = await FindCursorAsync(filterModel, sort);
            List<T> result = await cursor.ToListAsync();
            return result;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual List<T> Find(FilterModel filterModel, Expression<Func<T, object>> sort)
        {
            IAsyncCursor<T> cursor = FindCursor(filterModel, sort);
            List<T> result = cursor.ToList();
            return result;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> FindAsync(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
        {
            IAsyncCursor<T> cursor = await FindCursorAsync(filterModel, sort, sortOrder);
            List<T> result = await cursor.ToListAsync();
            return result;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual List<T> Find(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
        {
            IAsyncCursor<T> cursor = FindCursor(filterModel, sort, sortOrder);
            List<T> result = cursor.ToList();
            return result;
        }
    }
}
