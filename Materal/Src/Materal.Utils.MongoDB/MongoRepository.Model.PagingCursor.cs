using Materal.Utils.MongoDB.Extensions;
using MongoDB.Driver;

namespace Materal.Utils.MongoDB
{
    public partial class MongoRepository<T>
    {
        /// <summary>
        /// 获取分页查询选项
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private static FindOptions<T, T> GetPagingFindOptions(int pageIndex, int pageSize) => new() { Skip = GetSkip(pageIndex, pageSize), Limit = GetLimit(pageSize) };
        /// <summary>
        /// 获取分页查询选项
        /// </summary>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private static FindOptions<T, T> GetPagingFindOptions(SortDefinition<T> sort, int pageIndex, int pageSize) => new() { Sort = sort, Skip = GetSkip(pageIndex, pageSize), Limit = GetLimit(pageSize) };
        /// <summary>
        /// 获取分页查询选项
        /// </summary>
        /// <param name="sortExpression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private static FindOptions<T, T> GetPagingFindOptions(Expression<Func<T, object>> sortExpression, int pageIndex, int pageSize) => new() { Sort = GetSortDefinition(sortExpression), Skip = GetSkip(pageIndex, pageSize), Limit = GetLimit(pageSize) };
        /// <summary>
        /// 获取分页查询选项
        /// </summary>
        /// <param name="sortExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private static FindOptions<T, T> GetPagingFindOptions(Expression<Func<T, object>> sortExpression, SortOrderEnum sortOrder, int pageIndex, int pageSize) => new() { Sort = GetSortDefinition(sortExpression, sortOrder), Skip = GetSkip(pageIndex, pageSize), Limit = GetLimit(pageSize) };
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> PagingCursorAsync(FilterDefinition<T> filter, int pageIndex, int pageSize)
            => await FindCursorAsync(filter, GetPagingFindOptions(pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> PagingCursor(FilterDefinition<T> filter, int pageIndex, int pageSize)
            => FindCursor(filter, GetPagingFindOptions(pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> PagingCursorAsync(Expression<Func<T, bool>> filter, int pageIndex, int pageSize)
            => await FindCursorAsync(filter, GetPagingFindOptions(pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> PagingCursor(Expression<Func<T, bool>> filter, int pageIndex, int pageSize)
            => FindCursor(filter, GetPagingFindOptions(pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> PagingCursorAsync(FilterDefinition<T> filter, PageRequestModel pageRequestModel)
            => await PagingCursorAsync(filter, pageRequestModel.PageIndexInt, pageRequestModel.PageSizeInt);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> PagingCursor(FilterDefinition<T> filter, PageRequestModel pageRequestModel)
            => PagingCursor(filter, pageRequestModel.PageIndexInt, pageRequestModel.PageSizeInt);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> PagingCursorAsync(Expression<Func<T, bool>> filter, PageRequestModel pageRequestModel)
            => await PagingCursorAsync(filter, pageRequestModel.PageIndexInt, pageRequestModel.PageSizeInt);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> PagingCursor(Expression<Func<T, bool>> filter, PageRequestModel pageRequestModel)
            => PagingCursor(filter, pageRequestModel.PageIndexInt, pageRequestModel.PageSizeInt);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> PagingCursorAsync(FilterDefinition<T> filter, SortDefinition<T> sort, int pageIndex, int pageSize)
            => await FindCursorAsync(filter, GetPagingFindOptions(sort, pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> PagingCursor(FilterDefinition<T> filter, SortDefinition<T> sort, int pageIndex, int pageSize)
            => FindCursor(filter, GetPagingFindOptions(sort, pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> PagingCursorAsync(Expression<Func<T, bool>> filter, SortDefinition<T> sort, int pageIndex, int pageSize)
            => await FindCursorAsync(filter, GetPagingFindOptions(sort, pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> PagingCursor(Expression<Func<T, bool>> filter, SortDefinition<T> sort, int pageIndex, int pageSize)
            => FindCursor(filter, GetPagingFindOptions(sort, pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> PagingCursorAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, int pageIndex, int pageSize)
            => await FindCursorAsync(filter, GetPagingFindOptions(sort, pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> PagingCursor(FilterDefinition<T> filter, Expression<Func<T, object>> sort, int pageIndex, int pageSize)
            => FindCursor(filter, GetPagingFindOptions(sort, pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> PagingCursorAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int pageIndex, int pageSize)
            => await FindCursorAsync(filter, GetPagingFindOptions(sort, sortOrder, pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> PagingCursor(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int pageIndex, int pageSize)
            => FindCursor(filter, GetPagingFindOptions(sort, sortOrder, pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> PagingCursorAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, int pageIndex, int pageSize)
            => await FindCursorAsync(filter, GetPagingFindOptions(sort, pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> PagingCursor(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, int pageIndex, int pageSize)
            => FindCursor(filter, GetPagingFindOptions(sort, pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> PagingCursorAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int pageIndex, int pageSize)
            => await FindCursorAsync(filter, GetPagingFindOptions(sort, sortOrder, pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> PagingCursor(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int pageIndex, int pageSize)
            => FindCursor(filter, GetPagingFindOptions(sort, sortOrder, pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> PagingCursorAsync(FilterDefinition<T> filter, SortDefinition<T> sort, PageRequestModel pageRequestModel)
            => await PagingCursorAsync(filter, sort, pageRequestModel.PageIndexInt, pageRequestModel.PageSizeInt);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> PagingCursor(FilterDefinition<T> filter, SortDefinition<T> sort, PageRequestModel pageRequestModel)
            => PagingCursor(filter, sort, pageRequestModel.PageIndexInt, pageRequestModel.PageSizeInt);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> PagingCursorAsync(Expression<Func<T, bool>> filter, SortDefinition<T> sort, PageRequestModel pageRequestModel)
            => await PagingCursorAsync(filter, sort, pageRequestModel.PageIndexInt, pageRequestModel.PageSizeInt);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> PagingCursor(Expression<Func<T, bool>> filter, SortDefinition<T> sort, PageRequestModel pageRequestModel)
            => PagingCursor(filter, sort, pageRequestModel.PageIndexInt, pageRequestModel.PageSizeInt);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> PagingCursorAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, PageRequestModel pageRequestModel)
            => await FindCursorAsync(filter, GetPagingFindOptions(sort, pageRequestModel.PageIndexInt, pageRequestModel.PageSizeInt));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> PagingCursor(FilterDefinition<T> filter, Expression<Func<T, object>> sort, PageRequestModel pageRequestModel)
            => PagingCursor(filter, sort, pageRequestModel.PageIndexInt, pageRequestModel.PageSizeInt);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> PagingCursorAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, PageRequestModel pageRequestModel)
            => await FindCursorAsync(filter, GetPagingFindOptions(sort, sortOrder, pageRequestModel.PageIndexInt, pageRequestModel.PageSizeInt));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> PagingCursor(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, PageRequestModel pageRequestModel)
            => FindCursor(filter, GetPagingFindOptions(sort, sortOrder, pageRequestModel.PageIndexInt, pageRequestModel.PageSizeInt));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> PagingCursorAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, PageRequestModel pageRequestModel)
            => await PagingCursorAsync(filter, sort, pageRequestModel.PageIndexInt, pageRequestModel.PageSizeInt);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> PagingCursor(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, PageRequestModel pageRequestModel)
            => PagingCursor(filter, sort, pageRequestModel.PageIndexInt, pageRequestModel.PageSizeInt);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> PagingCursorAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, PageRequestModel pageRequestModel)
            => await PagingCursorAsync(filter, sort, sortOrder, pageRequestModel.PageIndexInt, pageRequestModel.PageSizeInt);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> PagingCursor(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, PageRequestModel pageRequestModel)
            => PagingCursor(filter, sort, sortOrder, pageRequestModel.PageIndexInt, pageRequestModel.PageSizeInt);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> PagingCursorAsync(FilterModel filterModel, int pageIndex, int pageSize)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await PagingCursorAsync(filter, pageIndex, pageSize);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> PagingCursor(FilterModel filterModel, int pageIndex, int pageSize)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return PagingCursor(filter, pageIndex, pageSize);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> PagingCursorAsync(FilterModel filterModel, SortDefinition<T> sort, int pageIndex, int pageSize)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await PagingCursorAsync(filter, sort, pageIndex, pageSize);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> PagingCursor(FilterModel filterModel, SortDefinition<T> sort, int pageIndex, int pageSize)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return PagingCursor(filter, sort, pageIndex, pageSize);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> PagingCursorAsync(FilterModel filterModel, Expression<Func<T, object>> sort, int pageIndex, int pageSize)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await PagingCursorAsync(filter, sort, pageIndex, pageSize);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> PagingCursor(FilterModel filterModel, Expression<Func<T, object>> sort, int pageIndex, int pageSize)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return PagingCursor(filter, sort, pageIndex, pageSize);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> PagingCursorAsync(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int pageIndex, int pageSize)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await PagingCursorAsync(filter, sort, sortOrder, pageIndex, pageSize);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> PagingCursor(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int pageIndex, int pageSize)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return PagingCursor(filter, sort, sortOrder, pageIndex, pageSize);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> PagingCursorAsync(FilterModel filterModel, PageRequestModel pageRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await PagingCursorAsync(filter, pageRequestModel);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> PagingCursor(FilterModel filterModel, PageRequestModel pageRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return PagingCursor(filter, pageRequestModel);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> PagingCursorAsync(FilterModel filterModel, SortDefinition<T> sort, PageRequestModel pageRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await PagingCursorAsync(filter, sort, pageRequestModel);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> PagingCursor(FilterModel filterModel, SortDefinition<T> sort, PageRequestModel pageRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return PagingCursor(filter, sort, pageRequestModel);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> PagingCursorAsync(FilterModel filterModel, Expression<Func<T, object>> sort, PageRequestModel pageRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await PagingCursorAsync(filter, sort, pageRequestModel);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> PagingCursor(FilterModel filterModel, Expression<Func<T, object>> sort, PageRequestModel pageRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return PagingCursor(filter, sort, pageRequestModel);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> PagingCursorAsync(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, PageRequestModel pageRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await PagingCursorAsync(filter, sort, sortOrder, pageRequestModel);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> PagingCursor(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, PageRequestModel pageRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return PagingCursor(filter, sort, sortOrder, pageRequestModel);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> PagingCursorAsync(PageRequestModel filterModel, SortDefinition<T> sort)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await PagingCursorAsync(filter, sort, filterModel);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> PagingCursor(PageRequestModel filterModel, SortDefinition<T> sort)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return PagingCursor(filter, sort, filterModel);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> PagingCursorAsync(PageRequestModel filterModel, Expression<Func<T, object>> sort)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await PagingCursorAsync(filter, sort, filterModel);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> PagingCursor(PageRequestModel filterModel, Expression<Func<T, object>> sort)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return PagingCursor(filter, sort, filterModel);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> PagingCursorAsync(PageRequestModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await PagingCursorAsync(filter, sort, sortOrder, filterModel);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> PagingCursor(PageRequestModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return PagingCursor(filter, sort, sortOrder, filterModel);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> PagingCursorAsync(PageRequestModel filterModel)
        {
            SortDefinition<T>? sort = filterModel.GetSortDefinition<T>();
            return sort is null ? await PagingCursorAsync(filterModel, filterModel) : await PagingCursorAsync(filterModel, sort, filterModel);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> PagingCursor(PageRequestModel filterModel)
        {
            SortDefinition<T>? sort = filterModel.GetSortDefinition<T>();
            return sort is null ? PagingCursor(filterModel, filterModel) : PagingCursor(filterModel, sort, filterModel);
        }
    }
}
