using Materal.Utils.Enums;
using Materal.Utils.Model;
using Materal.Utils.MongoDB.Extensions;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Materal.Utils.MongoDB
{
    public partial class MongoRepository<T>
    {
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(FilterDefinition<T> filter, long pageIndex, long pageSize)
            => await FindCursorAsync(filter, GetFindOptions(pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(FilterDefinition<T> filter, long pageIndex, long pageSize)
            => FindCursor(filter, GetFindOptions(pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(Expression<Func<T, bool>> filter, long pageIndex, long pageSize)
            => await FindCursorAsync(filter, GetFindOptions(pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(Expression<Func<T, bool>> filter, long pageIndex, long pageSize)
            => FindCursor(filter, GetFindOptions(pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(FilterDefinition<T> filter, PageRequestModel pageRequestModel)
            => await FindCursorAsync(filter, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(FilterDefinition<T> filter, PageRequestModel pageRequestModel)
            => FindCursor(filter, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(Expression<Func<T, bool>> filter, PageRequestModel pageRequestModel)
            => await FindCursorAsync(filter, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(Expression<Func<T, bool>> filter, PageRequestModel pageRequestModel)
            => FindCursor(filter, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(FilterDefinition<T> filter, SortDefinition<T> sort, long pageIndex, long pageSize)
            => await FindCursorAsync(filter, GetFindOptions(sort, pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(FilterDefinition<T> filter, SortDefinition<T> sort, long pageIndex, long pageSize)
            => FindCursor(filter, GetFindOptions(sort, pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(Expression<Func<T, bool>> filter, SortDefinition<T> sort, long pageIndex, long pageSize)
            => await FindCursorAsync(filter, GetFindOptions(sort, pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(Expression<Func<T, bool>> filter, SortDefinition<T> sort, long pageIndex, long pageSize)
            => FindCursor(filter, GetFindOptions(sort, pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, long pageIndex, long pageSize)
            => await FindCursorAsync(filter, GetFindOptions(sort, pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(FilterDefinition<T> filter, Expression<Func<T, object>> sort, long pageIndex, long pageSize)
            => FindCursor(filter, GetFindOptions(sort, pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, long pageIndex, long pageSize)
            => await FindCursorAsync(filter, GetFindOptions(sort, sortOrder, pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, long pageIndex, long pageSize)
            => FindCursor(filter, GetFindOptions(sort, sortOrder, pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, long pageIndex, long pageSize)
            => await FindCursorAsync(filter, GetFindOptions(sort, pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, long pageIndex, long pageSize)
            => FindCursor(filter, GetFindOptions(sort, pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, long pageIndex, long pageSize)
            => await FindCursorAsync(filter, GetFindOptions(sort, sortOrder, pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, long pageIndex, long pageSize)
            => FindCursor(filter, GetFindOptions(sort, sortOrder, pageIndex, pageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(FilterDefinition<T> filter, SortDefinition<T> sort, PageRequestModel pageRequestModel)
            => await FindCursorAsync(filter, sort, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(FilterDefinition<T> filter, SortDefinition<T> sort, PageRequestModel pageRequestModel)
            => FindCursor(filter, sort, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(Expression<Func<T, bool>> filter, SortDefinition<T> sort, PageRequestModel pageRequestModel)
            => await FindCursorAsync(filter, sort, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(Expression<Func<T, bool>> filter, SortDefinition<T> sort, PageRequestModel pageRequestModel)
            => FindCursor(filter, sort, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, PageRequestModel pageRequestModel)
            => await FindCursorAsync(filter, GetFindOptions(sort, pageRequestModel.PageIndex, pageRequestModel.PageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(FilterDefinition<T> filter, Expression<Func<T, object>> sort, PageRequestModel pageRequestModel)
            => FindCursor(filter, sort, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, PageRequestModel pageRequestModel)
            => await FindCursorAsync(filter, GetFindOptions(sort, sortOrder, pageRequestModel.PageIndex, pageRequestModel.PageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, PageRequestModel pageRequestModel)
            => FindCursor(filter, GetFindOptions(sort, sortOrder, pageRequestModel.PageIndex, pageRequestModel.PageSize));
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, PageRequestModel pageRequestModel)
            => await FindCursorAsync(filter, sort, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, PageRequestModel pageRequestModel)
            => FindCursor(filter, sort, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, PageRequestModel pageRequestModel)
            => await FindCursorAsync(filter, sort, sortOrder, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, PageRequestModel pageRequestModel)
            => FindCursor(filter, sort, sortOrder, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(FilterModel filterModel, long pageIndex, long pageSize)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await FindCursorAsync(filter, pageIndex, pageSize);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(FilterModel filterModel, long pageIndex, long pageSize)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return FindCursor(filter, pageIndex, pageSize);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(FilterModel filterModel, SortDefinition<T> sort, long pageIndex, long pageSize)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await FindCursorAsync(filter, sort, pageIndex, pageSize);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(FilterModel filterModel, SortDefinition<T> sort, long pageIndex, long pageSize)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return FindCursor(filter, sort, pageIndex, pageSize);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(FilterModel filterModel, Expression<Func<T, object>> sort, long pageIndex, long pageSize)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await FindCursorAsync(filter, sort, pageIndex, pageSize);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(FilterModel filterModel, Expression<Func<T, object>> sort, long pageIndex, long pageSize)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return FindCursor(filter, sort, pageIndex, pageSize);
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
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, long pageIndex, long pageSize)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await FindCursorAsync(filter, sort, sortOrder, pageIndex, pageSize);
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
        public virtual IAsyncCursor<T> FindCursor(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, long pageIndex, long pageSize)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return FindCursor(filter, sort, sortOrder, pageIndex, pageSize);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(FilterModel filterModel, PageRequestModel pageRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await FindCursorAsync(filter, pageRequestModel);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(FilterModel filterModel, PageRequestModel pageRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return FindCursor(filter, pageRequestModel);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(FilterModel filterModel, SortDefinition<T> sort, PageRequestModel pageRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await FindCursorAsync(filter, sort, pageRequestModel);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(FilterModel filterModel, SortDefinition<T> sort, PageRequestModel pageRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return FindCursor(filter, sort, pageRequestModel);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(FilterModel filterModel, Expression<Func<T, object>> sort, PageRequestModel pageRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await FindCursorAsync(filter, sort, pageRequestModel);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(FilterModel filterModel, Expression<Func<T, object>> sort, PageRequestModel pageRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return FindCursor(filter, sort, pageRequestModel);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, PageRequestModel pageRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await FindCursorAsync(filter, sort, sortOrder, pageRequestModel);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, PageRequestModel pageRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return FindCursor(filter, sort, sortOrder, pageRequestModel);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(PageRequestModel filterModel, SortDefinition<T> sort)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await FindCursorAsync(filter, sort, filterModel);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(PageRequestModel filterModel, SortDefinition<T> sort)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return FindCursor(filter, sort, filterModel);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(PageRequestModel filterModel, Expression<Func<T, object>> sort)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await FindCursorAsync(filter, sort, filterModel);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(PageRequestModel filterModel, Expression<Func<T, object>> sort)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return FindCursor(filter, sort, filterModel);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(PageRequestModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await FindCursorAsync(filter, sort, sortOrder, filterModel);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(PageRequestModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return FindCursor(filter, sort, sortOrder, filterModel);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> FindCursorAsync(PageRequestModel filterModel)
        {
            SortDefinition<T>? sort = filterModel.GetSortDefinition<T>();
            return sort is null ? await FindCursorAsync(filterModel, filterModel) : await FindCursorAsync(filterModel, sort, filterModel);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> FindCursor(PageRequestModel filterModel)
        {
            SortDefinition<T>? sort = filterModel.GetSortDefinition<T>();
            return sort is null ? FindCursor(filterModel, filterModel) : FindCursor(filterModel, sort, filterModel);
        }
    }
}
