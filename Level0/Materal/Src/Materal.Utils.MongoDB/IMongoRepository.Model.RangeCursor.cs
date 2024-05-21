using MongoDB.Driver;

namespace Materal.Utils.MongoDB
{
    public partial interface IMongoRepository<T>
    {
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> RangeCursorAsync(FilterDefinition<T> filter, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        IAsyncCursor<T> RangeCursor(FilterDefinition<T> filter, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> RangeCursorAsync(Expression<Func<T, bool>> filter, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        IAsyncCursor<T> RangeCursor(Expression<Func<T, bool>> filter, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> RangeCursorAsync(FilterDefinition<T> filter, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        IAsyncCursor<T> RangeCursor(FilterDefinition<T> filter, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> RangeCursorAsync(Expression<Func<T, bool>> filter, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        IAsyncCursor<T> RangeCursor(Expression<Func<T, bool>> filter, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> RangeCursorAsync(FilterDefinition<T> filter, SortDefinition<T> sort, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        IAsyncCursor<T> RangeCursor(FilterDefinition<T> filter, SortDefinition<T> sort, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> RangeCursorAsync(Expression<Func<T, bool>> filter, SortDefinition<T> sort, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        IAsyncCursor<T> RangeCursor(Expression<Func<T, bool>> filter, SortDefinition<T> sort, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> RangeCursorAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        IAsyncCursor<T> RangeCursor(FilterDefinition<T> filter, Expression<Func<T, object>> sort, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> RangeCursorAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        IAsyncCursor<T> RangeCursor(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> RangeCursorAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        IAsyncCursor<T> RangeCursor(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> RangeCursorAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        IAsyncCursor<T> RangeCursor(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> RangeCursorAsync(FilterDefinition<T> filter, SortDefinition<T> sort, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        IAsyncCursor<T> RangeCursor(FilterDefinition<T> filter, SortDefinition<T> sort, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> RangeCursorAsync(Expression<Func<T, bool>> filter, SortDefinition<T> sort, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        IAsyncCursor<T> RangeCursor(Expression<Func<T, bool>> filter, SortDefinition<T> sort, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> RangeCursorAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        IAsyncCursor<T> RangeCursor(FilterDefinition<T> filter, Expression<Func<T, object>> sort, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> RangeCursorAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        IAsyncCursor<T> RangeCursor(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> RangeCursorAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        IAsyncCursor<T> RangeCursor(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> RangeCursorAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        IAsyncCursor<T> RangeCursor(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> RangeCursorAsync(FilterModel filterModel, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        IAsyncCursor<T> RangeCursor(FilterModel filterModel, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> RangeCursorAsync(FilterModel filterModel, SortDefinition<T> sort, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        IAsyncCursor<T> RangeCursor(FilterModel filterModel, SortDefinition<T> sort, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> RangeCursorAsync(FilterModel filterModel, Expression<Func<T, object>> sort, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        IAsyncCursor<T> RangeCursor(FilterModel filterModel, Expression<Func<T, object>> sort, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> RangeCursorAsync(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        IAsyncCursor<T> RangeCursor(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> RangeCursorAsync(FilterModel filterModel, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        IAsyncCursor<T> RangeCursor(FilterModel filterModel, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> RangeCursorAsync(FilterModel filterModel, SortDefinition<T> sort, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        IAsyncCursor<T> RangeCursor(FilterModel filterModel, SortDefinition<T> sort, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> RangeCursorAsync(FilterModel filterModel, Expression<Func<T, object>> sort, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        IAsyncCursor<T> RangeCursor(FilterModel filterModel, Expression<Func<T, object>> sort, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> RangeCursorAsync(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        IAsyncCursor<T> RangeCursor(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> RangeCursorAsync(PageRequestModel filterModel, SortDefinition<T> sort);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        IAsyncCursor<T> RangeCursor(PageRequestModel filterModel, SortDefinition<T> sort);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> RangeCursorAsync(PageRequestModel filterModel, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        IAsyncCursor<T> RangeCursor(PageRequestModel filterModel, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> RangeCursorAsync(PageRequestModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        IAsyncCursor<T> RangeCursor(PageRequestModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> RangeCursorAsync(PageRequestModel filterModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        IAsyncCursor<T> RangeCursor(PageRequestModel filterModel);
    }
}
