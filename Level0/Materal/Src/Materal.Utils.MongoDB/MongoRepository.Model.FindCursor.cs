using Materal.Utils.MongoDB.Extensions;
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
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(FilterDefinition<T> filter, FindOptions<T, T>? findOptions = null)
        {
            IMongoCollection<T> collection = await GetCollectionAsync();
            IAsyncCursor<T> result = await collection.FindAsync(filter, findOptions);
            return result;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(FilterDefinition<T> filter, FindOptions<T, T>? findOptions = null)
        {
            IMongoCollection<T> collection = GetCollection();
            IAsyncCursor<T> result = collection.FindSync(filter, findOptions);
            return result;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(Expression<Func<T, bool>> filter, FindOptions<T, T>? findOptions = null)
        {
            IMongoCollection<T> collection = await GetCollectionAsync();
            IAsyncCursor<T> result = await collection.FindAsync(filter);
            return result;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(Expression<Func<T, bool>> filter, FindOptions<T, T>? findOptions = null)
        {
            IMongoCollection<T> collection = GetCollection();
            IAsyncCursor<T> result = collection.FindSync(filter, findOptions);
            return result;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(FilterDefinition<T> filter, SortDefinition<T> sort)
            => await FindCursorAsync(filter, GetFindOptions(sort));
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(FilterDefinition<T> filter, SortDefinition<T> sort)
            => FindCursor(filter, GetFindOptions(sort));
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(Expression<Func<T, bool>> filter, SortDefinition<T> sort)
            => await FindCursorAsync(filter, GetFindOptions(sort));
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(Expression<Func<T, bool>> filter, SortDefinition<T> sort)
            => FindCursor(filter, GetFindOptions(sort));
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort)
            => await FindCursorAsync(filter, GetFindOptions(sort));
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(FilterDefinition<T> filter, Expression<Func<T, object>> sort)
            => FindCursor(filter, GetFindOptions(sort));
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
            => await FindCursorAsync(filter, GetFindOptions(sort, sortOrder));
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
            => FindCursor(filter, GetFindOptions(sort, sortOrder));
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort)
            => await FindCursorAsync(filter, GetFindOptions(sort));
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort)
            => FindCursor(filter, GetFindOptions(sort));
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
            => await FindCursorAsync(filter, GetFindOptions(sort, sortOrder));
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
            => FindCursor(filter, GetFindOptions(sort, sortOrder));
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(FilterModel filterModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await FindCursorAsync(filter);
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(FilterModel filterModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return FindCursor(filter);
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(FilterModel filterModel, SortDefinition<T> sort)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await FindCursorAsync(filter, sort);
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(FilterModel filterModel, SortDefinition<T> sort)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return FindCursor(filter, sort);
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(FilterModel filterModel, Expression<Func<T, object>> sort)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await FindCursorAsync(filter, sort);
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(FilterModel filterModel, Expression<Func<T, object>> sort)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return FindCursor(filter, sort);
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await FindCursorAsync(filter, sort, sortOrder);
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return FindCursor(filter, sort, sortOrder);
        }
    }
}
