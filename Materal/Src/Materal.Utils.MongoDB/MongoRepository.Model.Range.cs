using Materal.Utils.MongoDB.Extensions;
using MongoDB.Driver;

namespace Materal.Utils.MongoDB
{
    public partial class MongoRepository<T>
    {
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterDefinition<T> filter, int skip, int take)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, skip, take);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            RangeModel rangeInfo = new(skip, take, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) Range(FilterDefinition<T> filter, int skip, int take)
        {
            IAsyncCursor<T> cursor = RangeCursor(filter, skip, take);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            RangeModel rangeInfo = new(skip, take, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(Expression<Func<T, bool>> filter, int skip, int take)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, skip, take);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            RangeModel rangeInfo = new(skip, take, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) Range(Expression<Func<T, bool>> filter, int skip, int take)
        {
            IAsyncCursor<T> cursor = RangeCursor(filter, skip, take);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            RangeModel rangeInfo = new(skip, take, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterDefinition<T> filter, RangeRequestModel rangeRequestModel)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, rangeRequestModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            RangeModel rangeInfo = new(rangeRequestModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) Range(FilterDefinition<T> filter, RangeRequestModel rangeRequestModel)
        {
            IAsyncCursor<T> cursor = RangeCursor(filter, rangeRequestModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            RangeModel rangeInfo = new(rangeRequestModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(Expression<Func<T, bool>> filter, RangeRequestModel rangeRequestModel)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, rangeRequestModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            RangeModel rangeInfo = new(rangeRequestModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) Range(Expression<Func<T, bool>> filter, RangeRequestModel rangeRequestModel)
        {
            IAsyncCursor<T> cursor = RangeCursor(filter, rangeRequestModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            RangeModel rangeInfo = new(rangeRequestModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterDefinition<T> filter, SortDefinition<T> sort, int skip, int take)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, sort, skip, take);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            RangeModel rangeInfo = new(skip, take, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) Range(FilterDefinition<T> filter, SortDefinition<T> sort, int skip, int take)
        {
            IAsyncCursor<T> cursor = RangeCursor(filter, sort, skip, take);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            RangeModel rangeInfo = new(skip, take, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(Expression<Func<T, bool>> filter, SortDefinition<T> sort, int skip, int take)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, sort, skip, take);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            RangeModel rangeInfo = new(skip, take, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) Range(Expression<Func<T, bool>> filter, SortDefinition<T> sort, int skip, int take)
        {
            IAsyncCursor<T> cursor = RangeCursor(filter, sort, skip, take);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            RangeModel rangeInfo = new(skip, take, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, int skip, int take)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, sort, skip, take);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            RangeModel rangeInfo = new(skip, take, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) Range(FilterDefinition<T> filter, Expression<Func<T, object>> sort, int skip, int take)
        {
            IAsyncCursor<T> cursor = RangeCursor(filter, sort, skip, take);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            RangeModel rangeInfo = new(skip, take, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int skip, int take)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, sort, sortOrder, skip, take);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            RangeModel rangeInfo = new(skip, take, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) Range(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int skip, int take)
        {
            IAsyncCursor<T> cursor = RangeCursor(filter, sort, sortOrder, skip, take);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            RangeModel rangeInfo = new(skip, take, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, int skip, int take)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, sort, skip, take);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            RangeModel rangeInfo = new(skip, take, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) Range(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, int skip, int take)
        {
            IAsyncCursor<T> cursor = RangeCursor(filter, sort, skip, take);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            RangeModel rangeInfo = new(skip, take, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int skip, int take)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, sort, sortOrder, skip, take);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            RangeModel rangeInfo = new(skip, take, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) Range(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int skip, int take)
        {
            IAsyncCursor<T> cursor = RangeCursor(filter, sort, sortOrder, skip, take);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            RangeModel rangeInfo = new(skip, take, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterDefinition<T> filter, SortDefinition<T> sort, RangeRequestModel rangeRequestModel)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, sort, rangeRequestModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            RangeModel rangeInfo = new(rangeRequestModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) Range(FilterDefinition<T> filter, SortDefinition<T> sort, RangeRequestModel rangeRequestModel)
        {
            IAsyncCursor<T> cursor = RangeCursor(filter, sort, rangeRequestModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            RangeModel rangeInfo = new(rangeRequestModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(Expression<Func<T, bool>> filter, SortDefinition<T> sort, RangeRequestModel rangeRequestModel)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, sort, rangeRequestModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            RangeModel rangeInfo = new(rangeRequestModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) Range(Expression<Func<T, bool>> filter, SortDefinition<T> sort, RangeRequestModel rangeRequestModel)
        {
            IAsyncCursor<T> cursor = RangeCursor(filter, sort, rangeRequestModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            RangeModel rangeInfo = new(rangeRequestModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, RangeRequestModel rangeRequestModel)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, sort, rangeRequestModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            RangeModel rangeInfo = new(rangeRequestModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) Range(FilterDefinition<T> filter, Expression<Func<T, object>> sort, RangeRequestModel rangeRequestModel)
        {
            IAsyncCursor<T> cursor = RangeCursor(filter, sort, rangeRequestModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            RangeModel rangeInfo = new(rangeRequestModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, RangeRequestModel rangeRequestModel)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, sort, sortOrder, rangeRequestModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            RangeModel rangeInfo = new(rangeRequestModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) Range(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, RangeRequestModel rangeRequestModel)
        {
            IAsyncCursor<T> cursor = RangeCursor(filter, sort, sortOrder, rangeRequestModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            RangeModel rangeInfo = new(rangeRequestModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, RangeRequestModel rangeRequestModel)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, sort, rangeRequestModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            RangeModel rangeInfo = new(rangeRequestModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) Range(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, RangeRequestModel rangeRequestModel)
        {
            IAsyncCursor<T> cursor = RangeCursor(filter, sort, rangeRequestModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            RangeModel rangeInfo = new(rangeRequestModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, RangeRequestModel rangeRequestModel)
        {
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, sort, sortOrder, rangeRequestModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            RangeModel rangeInfo = new(rangeRequestModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) Range(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, RangeRequestModel rangeRequestModel)
        {
            IAsyncCursor<T> cursor = RangeCursor(filter, sort, sortOrder, rangeRequestModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            RangeModel rangeInfo = new(rangeRequestModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterModel filterModel, int skip, int take)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, skip, take);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            RangeModel rangeInfo = new(skip, take, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) Range(FilterModel filterModel, int skip, int take)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = RangeCursor(filter, skip, take);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            RangeModel rangeInfo = new(skip, take, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterModel filterModel, SortDefinition<T> sort, int skip, int take)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, skip, take);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            RangeModel rangeInfo = new(skip, take, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) Range(FilterModel filterModel, SortDefinition<T> sort, int skip, int take)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = RangeCursor(filter, skip, take);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            RangeModel rangeInfo = new(skip, take, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterModel filterModel, Expression<Func<T, object>> sort, int skip, int take)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, skip, take);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            RangeModel rangeInfo = new(skip, take, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) Range(FilterModel filterModel, Expression<Func<T, object>> sort, int skip, int take)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = RangeCursor(filter, skip, take);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            RangeModel rangeInfo = new(skip, take, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int skip, int take)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, skip, take);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            RangeModel rangeInfo = new(skip, take, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) Range(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int skip, int take)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = RangeCursor(filter, skip, take);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            RangeModel rangeInfo = new(skip, take, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterModel filterModel, RangeRequestModel rangeRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, rangeRequestModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            RangeModel rangeInfo = new(rangeRequestModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) Range(FilterModel filterModel, RangeRequestModel rangeRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = RangeCursor(filter, rangeRequestModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            RangeModel rangeInfo = new(rangeRequestModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterModel filterModel, SortDefinition<T> sort, RangeRequestModel rangeRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, sort, rangeRequestModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            RangeModel rangeInfo = new(rangeRequestModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) Range(FilterModel filterModel, SortDefinition<T> sort, RangeRequestModel rangeRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = RangeCursor(filter, sort, rangeRequestModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            RangeModel rangeInfo = new(rangeRequestModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterModel filterModel, Expression<Func<T, object>> sort, RangeRequestModel rangeRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, sort, rangeRequestModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            RangeModel rangeInfo = new(rangeRequestModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) Range(FilterModel filterModel, Expression<Func<T, object>> sort, RangeRequestModel rangeRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = RangeCursor(filter, sort, rangeRequestModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            RangeModel rangeInfo = new(rangeRequestModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, RangeRequestModel rangeRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, sort, sortOrder, rangeRequestModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            RangeModel rangeInfo = new(rangeRequestModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) Range(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, RangeRequestModel rangeRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = RangeCursor(filter, sort, sortOrder, rangeRequestModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            RangeModel rangeInfo = new(rangeRequestModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(RangeRequestModel filterModel, SortDefinition<T> sort)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, sort, filterModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            RangeModel rangeInfo = new(filterModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) Range(RangeRequestModel filterModel, SortDefinition<T> sort)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = RangeCursor(filter, sort, filterModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            RangeModel rangeInfo = new(filterModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(RangeRequestModel filterModel, Expression<Func<T, object>> sort)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, sort, filterModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            RangeModel rangeInfo = new(filterModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) Range(RangeRequestModel filterModel, Expression<Func<T, object>> sort)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = RangeCursor(filter, sort, filterModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            RangeModel rangeInfo = new(filterModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(RangeRequestModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = await RangeCursorAsync(filter, sort, sortOrder, filterModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            RangeModel rangeInfo = new(filterModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) Range(RangeRequestModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = RangeCursor(filter, sort, sortOrder, filterModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            RangeModel rangeInfo = new(filterModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(RangeRequestModel filterModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            SortDefinition<T>? sort = filterModel.GetSortDefinition<T>();
            IAsyncCursor<T> cursor = sort is null ? await RangeCursorAsync(filter, filterModel) : await RangeCursorAsync(filter, sort, filterModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            RangeModel rangeInfo = new(filterModel, dataCount);
            return (data, rangeInfo);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) Range(RangeRequestModel filterModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            SortDefinition<T>? sort = filterModel.GetSortDefinition<T>();
            IAsyncCursor<T> cursor = sort is null ? RangeCursor(filter, filterModel) : RangeCursor(filter, sort, filterModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            RangeModel rangeInfo = new(filterModel, dataCount);
            return (data, rangeInfo);
        }
    }
}
