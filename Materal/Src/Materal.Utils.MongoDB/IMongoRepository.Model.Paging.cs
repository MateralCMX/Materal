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
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterDefinition<T> filter, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(FilterDefinition<T> filter, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(Expression<Func<T, bool>> filter, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(Expression<Func<T, bool>> filter, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterDefinition<T> filter, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(FilterDefinition<T> filter, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(Expression<Func<T, bool>> filter, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(Expression<Func<T, bool>> filter, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterDefinition<T> filter, SortDefinition<T> sort, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(FilterDefinition<T> filter, SortDefinition<T> sort, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(Expression<Func<T, bool>> filter, SortDefinition<T> sort, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(Expression<Func<T, bool>> filter, SortDefinition<T> sort, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(FilterDefinition<T> filter, Expression<Func<T, object>> sort, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterDefinition<T> filter, SortDefinition<T> sort, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(FilterDefinition<T> filter, SortDefinition<T> sort, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(Expression<Func<T, bool>> filter, SortDefinition<T> sort, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(Expression<Func<T, bool>> filter, SortDefinition<T> sort, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(FilterDefinition<T> filter, Expression<Func<T, object>> sort, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterModel filterModel, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(FilterModel filterModel, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterModel filterModel, SortDefinition<T> sort, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(FilterModel filterModel, SortDefinition<T> sort, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterModel filterModel, Expression<Func<T, object>> sort, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(FilterModel filterModel, Expression<Func<T, object>> sort, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int pageIndex, int pageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterModel filterModel, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(FilterModel filterModel, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterModel filterModel, SortDefinition<T> sort, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(FilterModel filterModel, SortDefinition<T> sort, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterModel filterModel, Expression<Func<T, object>> sort, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(FilterModel filterModel, Expression<Func<T, object>> sort, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, PageRequestModel pageRequestModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(PageRequestModel filterModel, SortDefinition<T> sort);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(PageRequestModel filterModel, SortDefinition<T> sort);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(PageRequestModel filterModel, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(PageRequestModel filterModel, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(PageRequestModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(PageRequestModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(PageRequestModel filterModel);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(PageRequestModel filterModel);
    }
}
