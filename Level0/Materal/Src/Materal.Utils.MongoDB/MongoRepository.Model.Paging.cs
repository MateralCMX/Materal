using Materal.Utils.MongoDB.Extensions;
using MongoDB.Driver;

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
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterDefinition<T> filter, int pageIndex, int pageSize)
        {
            IAsyncCursor<T> cursor = await PagingCursorAsync(filter, pageIndex, pageSize);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            PageModel pageInfo = new(pageIndex, pageSize, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(FilterDefinition<T> filter, int pageIndex, int pageSize)
        {
            IAsyncCursor<T> cursor = PagingCursor(filter, pageIndex, pageSize);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            PageModel pageInfo = new(pageIndex, pageSize, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(Expression<Func<T, bool>> filter, int pageIndex, int pageSize)
        {
            IAsyncCursor<T> cursor = await PagingCursorAsync(filter, pageIndex, pageSize);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            PageModel pageInfo = new(pageIndex, pageSize, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(Expression<Func<T, bool>> filter, int pageIndex, int pageSize)
        {
            IAsyncCursor<T> cursor = PagingCursor(filter, pageIndex, pageSize);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            PageModel pageInfo = new(pageIndex, pageSize, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterDefinition<T> filter, PageRequestModel pageRequestModel)
        {
            IAsyncCursor<T> cursor = await PagingCursorAsync(filter, pageRequestModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            PageModel pageInfo = new(pageRequestModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(FilterDefinition<T> filter, PageRequestModel pageRequestModel)
        {
            IAsyncCursor<T> cursor = PagingCursor(filter, pageRequestModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            PageModel pageInfo = new(pageRequestModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(Expression<Func<T, bool>> filter, PageRequestModel pageRequestModel)
        {
            IAsyncCursor<T> cursor = await PagingCursorAsync(filter, pageRequestModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            PageModel pageInfo = new(pageRequestModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(Expression<Func<T, bool>> filter, PageRequestModel pageRequestModel)
        {
            IAsyncCursor<T> cursor = PagingCursor(filter, pageRequestModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            PageModel pageInfo = new(pageRequestModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterDefinition<T> filter, SortDefinition<T> sort, int pageIndex, int pageSize)
        {
            IAsyncCursor<T> cursor = await PagingCursorAsync(filter, sort, pageIndex, pageSize);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            PageModel pageInfo = new(pageIndex, pageSize, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(FilterDefinition<T> filter, SortDefinition<T> sort, int pageIndex, int pageSize)
        {
            IAsyncCursor<T> cursor = PagingCursor(filter, sort, pageIndex, pageSize);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            PageModel pageInfo = new(pageIndex, pageSize, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(Expression<Func<T, bool>> filter, SortDefinition<T> sort, int pageIndex, int pageSize)
        {
            IAsyncCursor<T> cursor = await PagingCursorAsync(filter, sort, pageIndex, pageSize);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            PageModel pageInfo = new(pageIndex, pageSize, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(Expression<Func<T, bool>> filter, SortDefinition<T> sort, int pageIndex, int pageSize)
        {
            IAsyncCursor<T> cursor = PagingCursor(filter, sort, pageIndex, pageSize);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            PageModel pageInfo = new(pageIndex, pageSize, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, int pageIndex, int pageSize)
        {
            IAsyncCursor<T> cursor = await PagingCursorAsync(filter, sort, pageIndex, pageSize);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            PageModel pageInfo = new(pageIndex, pageSize, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(FilterDefinition<T> filter, Expression<Func<T, object>> sort, int pageIndex, int pageSize)
        {
            IAsyncCursor<T> cursor = PagingCursor(filter, sort, pageIndex, pageSize);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            PageModel pageInfo = new(pageIndex, pageSize, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int pageIndex, int pageSize)
        {
            IAsyncCursor<T> cursor = await PagingCursorAsync(filter, sort, sortOrder, pageIndex, pageSize);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            PageModel pageInfo = new(pageIndex, pageSize, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int pageIndex, int pageSize)
        {
            IAsyncCursor<T> cursor = PagingCursor(filter, sort, sortOrder, pageIndex, pageSize);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            PageModel pageInfo = new(pageIndex, pageSize, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, int pageIndex, int pageSize)
        {
            IAsyncCursor<T> cursor = await PagingCursorAsync(filter, sort, pageIndex, pageSize);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            PageModel pageInfo = new(pageIndex, pageSize, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, int pageIndex, int pageSize)
        {
            IAsyncCursor<T> cursor = PagingCursor(filter, sort, pageIndex, pageSize);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            PageModel pageInfo = new(pageIndex, pageSize, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int pageIndex, int pageSize)
        {
            IAsyncCursor<T> cursor = await PagingCursorAsync(filter, sort, sortOrder, pageIndex, pageSize);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            PageModel pageInfo = new(pageIndex, pageSize, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int pageIndex, int pageSize)
        {
            IAsyncCursor<T> cursor = PagingCursor(filter, sort, sortOrder, pageIndex, pageSize);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            PageModel pageInfo = new(pageIndex, pageSize, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterDefinition<T> filter, SortDefinition<T> sort, PageRequestModel pageRequestModel)
        {
            IAsyncCursor<T> cursor = await PagingCursorAsync(filter, sort, pageRequestModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            PageModel pageInfo = new(pageRequestModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(FilterDefinition<T> filter, SortDefinition<T> sort, PageRequestModel pageRequestModel)
        {
            IAsyncCursor<T> cursor = PagingCursor(filter, sort, pageRequestModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            PageModel pageInfo = new(pageRequestModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(Expression<Func<T, bool>> filter, SortDefinition<T> sort, PageRequestModel pageRequestModel)
        {
            IAsyncCursor<T> cursor = await PagingCursorAsync(filter, sort, pageRequestModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            PageModel pageInfo = new(pageRequestModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(Expression<Func<T, bool>> filter, SortDefinition<T> sort, PageRequestModel pageRequestModel)
        {
            IAsyncCursor<T> cursor = PagingCursor(filter, sort, pageRequestModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            PageModel pageInfo = new(pageRequestModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, PageRequestModel pageRequestModel)
        {
            IAsyncCursor<T> cursor = await PagingCursorAsync(filter, sort, pageRequestModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            PageModel pageInfo = new(pageRequestModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(FilterDefinition<T> filter, Expression<Func<T, object>> sort, PageRequestModel pageRequestModel)
        {
            IAsyncCursor<T> cursor = PagingCursor(filter, sort, pageRequestModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            PageModel pageInfo = new(pageRequestModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, PageRequestModel pageRequestModel)
        {
            IAsyncCursor<T> cursor = await PagingCursorAsync(filter, sort, sortOrder, pageRequestModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            PageModel pageInfo = new(pageRequestModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, PageRequestModel pageRequestModel)
        {
            IAsyncCursor<T> cursor = PagingCursor(filter, sort, sortOrder, pageRequestModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            PageModel pageInfo = new(pageRequestModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, PageRequestModel pageRequestModel)
        {
            IAsyncCursor<T> cursor = await PagingCursorAsync(filter, sort, pageRequestModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            PageModel pageInfo = new(pageRequestModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, PageRequestModel pageRequestModel)
        {
            IAsyncCursor<T> cursor = PagingCursor(filter, sort, pageRequestModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            PageModel pageInfo = new(pageRequestModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, PageRequestModel pageRequestModel)
        {
            IAsyncCursor<T> cursor = await PagingCursorAsync(filter, sort, sortOrder, pageRequestModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            PageModel pageInfo = new(pageRequestModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, PageRequestModel pageRequestModel)
        {
            IAsyncCursor<T> cursor = PagingCursor(filter, sort, sortOrder, pageRequestModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            PageModel pageInfo = new(pageRequestModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterModel filterModel, int pageIndex, int pageSize)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = await PagingCursorAsync(filter, pageIndex, pageSize);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            PageModel pageInfo = new(pageIndex, pageSize, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(FilterModel filterModel, int pageIndex, int pageSize)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = PagingCursor(filter, pageIndex, pageSize);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            PageModel pageInfo = new(pageIndex, pageSize, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterModel filterModel, SortDefinition<T> sort, int pageIndex, int pageSize)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = await PagingCursorAsync(filter, pageIndex, pageSize);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            PageModel pageInfo = new(pageIndex, pageSize, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(FilterModel filterModel, SortDefinition<T> sort, int pageIndex, int pageSize)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = PagingCursor(filter, pageIndex, pageSize);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            PageModel pageInfo = new(pageIndex, pageSize, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterModel filterModel, Expression<Func<T, object>> sort, int pageIndex, int pageSize)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = await PagingCursorAsync(filter, pageIndex, pageSize);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            PageModel pageInfo = new(pageIndex, pageSize, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(FilterModel filterModel, Expression<Func<T, object>> sort, int pageIndex, int pageSize)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = PagingCursor(filter, pageIndex, pageSize);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            PageModel pageInfo = new(pageIndex, pageSize, dataCount);
            return (data, pageInfo);
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
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int pageIndex, int pageSize)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = await PagingCursorAsync(filter, pageIndex, pageSize);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            PageModel pageInfo = new(pageIndex, pageSize, dataCount);
            return (data, pageInfo);
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
        public virtual (List<T> data, PageModel pageInfo) Paging(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int pageIndex, int pageSize)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = PagingCursor(filter, pageIndex, pageSize);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            PageModel pageInfo = new(pageIndex, pageSize, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterModel filterModel, PageRequestModel pageRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = await PagingCursorAsync(filter, pageRequestModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            PageModel pageInfo = new(pageRequestModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(FilterModel filterModel, PageRequestModel pageRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = PagingCursor(filter, pageRequestModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            PageModel pageInfo = new(pageRequestModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterModel filterModel, SortDefinition<T> sort, PageRequestModel pageRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = await PagingCursorAsync(filter, sort, pageRequestModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            PageModel pageInfo = new(pageRequestModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(FilterModel filterModel, SortDefinition<T> sort, PageRequestModel pageRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = PagingCursor(filter, sort, pageRequestModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            PageModel pageInfo = new(pageRequestModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterModel filterModel, Expression<Func<T, object>> sort, PageRequestModel pageRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = await PagingCursorAsync(filter, sort, pageRequestModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            PageModel pageInfo = new(pageRequestModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(FilterModel filterModel, Expression<Func<T, object>> sort, PageRequestModel pageRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = PagingCursor(filter, sort, pageRequestModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            PageModel pageInfo = new(pageRequestModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, PageRequestModel pageRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = await PagingCursorAsync(filter, sort, sortOrder, pageRequestModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            PageModel pageInfo = new(pageRequestModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, PageRequestModel pageRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = PagingCursor(filter, sort, sortOrder, pageRequestModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            PageModel pageInfo = new(pageRequestModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(PageRequestModel filterModel, SortDefinition<T> sort)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = await PagingCursorAsync(filter, sort, filterModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            PageModel pageInfo = new(filterModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(PageRequestModel filterModel, SortDefinition<T> sort)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = PagingCursor(filter, sort, filterModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            PageModel pageInfo = new(filterModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(PageRequestModel filterModel, Expression<Func<T, object>> sort)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = await PagingCursorAsync(filter, sort, filterModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            PageModel pageInfo = new(filterModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(PageRequestModel filterModel, Expression<Func<T, object>> sort)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = PagingCursor(filter, sort, filterModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            PageModel pageInfo = new(filterModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(PageRequestModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = await PagingCursorAsync(filter, sort, sortOrder, filterModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            PageModel pageInfo = new(filterModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(PageRequestModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            IAsyncCursor<T> cursor = PagingCursor(filter, sort, sortOrder, filterModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            PageModel pageInfo = new(filterModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(PageRequestModel filterModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            SortDefinition<T>? sort = filterModel.GetSortDefinition<T>();
            IAsyncCursor<T> cursor = sort is null ? await PagingCursorAsync(filter, filterModel) : await PagingCursorAsync(filter, sort, filterModel);
            List<T> data = await cursor.ToListAsync();
            long dataCount = await CountAsync(filter);
            PageModel pageInfo = new(filterModel, dataCount);
            return (data, pageInfo);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(PageRequestModel filterModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            SortDefinition<T>? sort = filterModel.GetSortDefinition<T>();
            IAsyncCursor<T> cursor = sort is null ? PagingCursor(filter, filterModel) : PagingCursor(filter, sort, filterModel);
            List<T> data = cursor.ToList();
            long dataCount = Count(filter);
            PageModel pageInfo = new(filterModel, dataCount);
            return (data, pageInfo);
        }
    }
}
