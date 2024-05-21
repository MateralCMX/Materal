using MongoDB.Driver;

namespace Materal.Utils.MongoDB
{
    public partial interface IMongoRepository<T>
    {
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> PagingCursorAsync(FilterDefinition<T> filter, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IAsyncCursor<T> PagingCursor(FilterDefinition<T> filter, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> PagingCursorAsync(Expression<Func<T, bool>> filter, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IAsyncCursor<T> PagingCursor(Expression<Func<T, bool>> filter, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> PagingCursorAsync(FilterDefinition<T> filter, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        IAsyncCursor<T> PagingCursor(FilterDefinition<T> filter, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> PagingCursorAsync(Expression<Func<T, bool>> filter, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        IAsyncCursor<T> PagingCursor(Expression<Func<T, bool>> filter, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> PagingCursorAsync(FilterDefinition<T> filter, SortDefinition<T> sort, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IAsyncCursor<T> PagingCursor(FilterDefinition<T> filter, SortDefinition<T> sort, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> PagingCursorAsync(Expression<Func<T, bool>> filter, SortDefinition<T> sort, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IAsyncCursor<T> PagingCursor(Expression<Func<T, bool>> filter, SortDefinition<T> sort, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> PagingCursorAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IAsyncCursor<T> PagingCursor(FilterDefinition<T> filter, Expression<Func<T, object>> sort, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> PagingCursorAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IAsyncCursor<T> PagingCursor(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> PagingCursorAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IAsyncCursor<T> PagingCursor(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> PagingCursorAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IAsyncCursor<T> PagingCursor(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> PagingCursorAsync(FilterDefinition<T> filter, SortDefinition<T> sort, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        IAsyncCursor<T> PagingCursor(FilterDefinition<T> filter, SortDefinition<T> sort, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> PagingCursorAsync(Expression<Func<T, bool>> filter, SortDefinition<T> sort, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        IAsyncCursor<T> PagingCursor(Expression<Func<T, bool>> filter, SortDefinition<T> sort, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> PagingCursorAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        IAsyncCursor<T> PagingCursor(FilterDefinition<T> filter, Expression<Func<T, object>> sort, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> PagingCursorAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        IAsyncCursor<T> PagingCursor(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> PagingCursorAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        IAsyncCursor<T> PagingCursor(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> PagingCursorAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        IAsyncCursor<T> PagingCursor(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> PagingCursorAsync(FilterModel filterModel, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IAsyncCursor<T> PagingCursor(FilterModel filterModel, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> PagingCursorAsync(FilterModel filterModel, SortDefinition<T> sort, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IAsyncCursor<T> PagingCursor(FilterModel filterModel, SortDefinition<T> sort, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> PagingCursorAsync(FilterModel filterModel, Expression<Func<T, object>> sort, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IAsyncCursor<T> PagingCursor(FilterModel filterModel, Expression<Func<T, object>> sort, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> PagingCursorAsync(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IAsyncCursor<T> PagingCursor(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> PagingCursorAsync(FilterModel filterModel, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        IAsyncCursor<T> PagingCursor(FilterModel filterModel, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> PagingCursorAsync(FilterModel filterModel, SortDefinition<T> sort, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        IAsyncCursor<T> PagingCursor(FilterModel filterModel, SortDefinition<T> sort, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> PagingCursorAsync(FilterModel filterModel, Expression<Func<T, object>> sort, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        IAsyncCursor<T> PagingCursor(FilterModel filterModel, Expression<Func<T, object>> sort, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> PagingCursorAsync(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        IAsyncCursor<T> PagingCursor(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> PagingCursorAsync(PageRequestModel filterModel, SortDefinition<T> sort);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        IAsyncCursor<T> PagingCursor(PageRequestModel filterModel, SortDefinition<T> sort);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> PagingCursorAsync(PageRequestModel filterModel, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        IAsyncCursor<T> PagingCursor(PageRequestModel filterModel, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> PagingCursorAsync(PageRequestModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        IAsyncCursor<T> PagingCursor(PageRequestModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> PagingCursorAsync(PageRequestModel filterModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        IAsyncCursor<T> PagingCursor(PageRequestModel filterModel);
    }
}
