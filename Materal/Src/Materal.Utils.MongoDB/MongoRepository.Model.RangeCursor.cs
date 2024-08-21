using Materal.Utils.MongoDB.Extensions;
using MongoDB.Driver;

namespace Materal.Utils.MongoDB
{
    public partial class MongoRepository<T>
    {
        /// <summary>
        /// 获取范围查询选项
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        private static FindOptions<T, T> GetRangeFindOptions(int skip, int take) => new() { Skip = GetSkip(skip), Limit = GetLimit(take) };
        /// <summary>
        /// 获取范围查询选项
        /// </summary>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        private static FindOptions<T, T> GetRangeFindOptions(SortDefinition<T> sort, int skip, int take) => new() { Sort = sort, Skip = GetSkip(skip), Limit = GetLimit(take) };
        /// <summary>
        /// 获取范围查询选项
        /// </summary>
        /// <param name="sortExpression"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        private static FindOptions<T, T> GetRangeFindOptions(Expression<Func<T, object>> sortExpression, int skip, int take) => new() { Sort = GetSortDefinition(sortExpression), Skip = GetSkip(skip), Limit = GetLimit(take) };
        /// <summary>
        /// 获取范围查询选项
        /// </summary>
        /// <param name="sortExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        private static FindOptions<T, T> GetRangeFindOptions(Expression<Func<T, object>> sortExpression, SortOrderEnum sortOrder, int skip, int take) => new() { Sort = GetSortDefinition(sortExpression, sortOrder), Skip = GetSkip(skip), Limit = GetLimit(take) };
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> RangeCursorAsync(FilterDefinition<T> filter, int skip, int take)
            => await FindCursorAsync(filter, GetRangeFindOptions(skip, take));
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> RangeCursor(FilterDefinition<T> filter, int skip, int take)
            => FindCursor(filter, GetRangeFindOptions(skip, take));
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> RangeCursorAsync(Expression<Func<T, bool>> filter, int skip, int take)
            => await FindCursorAsync(filter, GetRangeFindOptions(skip, take));
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> RangeCursor(Expression<Func<T, bool>> filter, int skip, int take)
            => FindCursor(filter, GetRangeFindOptions(skip, take));
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> RangeCursorAsync(FilterDefinition<T> filter, RangeRequestModel rangeRequestModel)
            => await RangeCursorAsync(filter, rangeRequestModel.SkipInt, rangeRequestModel.TakeInt);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> RangeCursor(FilterDefinition<T> filter, RangeRequestModel rangeRequestModel)
            => RangeCursor(filter, rangeRequestModel.SkipInt, rangeRequestModel.TakeInt);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> RangeCursorAsync(Expression<Func<T, bool>> filter, RangeRequestModel rangeRequestModel)
            => await RangeCursorAsync(filter, rangeRequestModel.SkipInt, rangeRequestModel.TakeInt);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> RangeCursor(Expression<Func<T, bool>> filter, RangeRequestModel rangeRequestModel)
            => RangeCursor(filter, rangeRequestModel.SkipInt, rangeRequestModel.TakeInt);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> RangeCursorAsync(FilterDefinition<T> filter, SortDefinition<T> sort, int skip, int take)
            => await FindCursorAsync(filter, GetRangeFindOptions(sort, skip, take));
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> RangeCursor(FilterDefinition<T> filter, SortDefinition<T> sort, int skip, int take)
            => FindCursor(filter, GetRangeFindOptions(sort, skip, take));
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> RangeCursorAsync(Expression<Func<T, bool>> filter, SortDefinition<T> sort, int skip, int take)
            => await FindCursorAsync(filter, GetRangeFindOptions(sort, skip, take));
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> RangeCursor(Expression<Func<T, bool>> filter, SortDefinition<T> sort, int skip, int take)
            => FindCursor(filter, GetRangeFindOptions(sort, skip, take));
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> RangeCursorAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, int skip, int take)
            => await FindCursorAsync(filter, GetRangeFindOptions(sort, skip, take));
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> RangeCursor(FilterDefinition<T> filter, Expression<Func<T, object>> sort, int skip, int take)
            => FindCursor(filter, GetRangeFindOptions(sort, skip, take));
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> RangeCursorAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int skip, int take)
            => await FindCursorAsync(filter, GetRangeFindOptions(sort, sortOrder, skip, take));
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> RangeCursor(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int skip, int take)
            => FindCursor(filter, GetRangeFindOptions(sort, sortOrder, skip, take));
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> RangeCursorAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, int skip, int take)
            => await FindCursorAsync(filter, GetRangeFindOptions(sort, skip, take));
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> RangeCursor(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, int skip, int take)
            => FindCursor(filter, GetRangeFindOptions(sort, skip, take));
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> RangeCursorAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int skip, int take)
            => await FindCursorAsync(filter, GetRangeFindOptions(sort, sortOrder, skip, take));
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> RangeCursor(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int skip, int take)
            => FindCursor(filter, GetRangeFindOptions(sort, sortOrder, skip, take));
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> RangeCursorAsync(FilterDefinition<T> filter, SortDefinition<T> sort, RangeRequestModel rangeRequestModel)
            => await RangeCursorAsync(filter, sort, rangeRequestModel.SkipInt, rangeRequestModel.TakeInt);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> RangeCursor(FilterDefinition<T> filter, SortDefinition<T> sort, RangeRequestModel rangeRequestModel)
            => RangeCursor(filter, sort, rangeRequestModel.SkipInt, rangeRequestModel.TakeInt);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> RangeCursorAsync(Expression<Func<T, bool>> filter, SortDefinition<T> sort, RangeRequestModel rangeRequestModel)
            => await RangeCursorAsync(filter, sort, rangeRequestModel.SkipInt, rangeRequestModel.TakeInt);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> RangeCursor(Expression<Func<T, bool>> filter, SortDefinition<T> sort, RangeRequestModel rangeRequestModel)
            => RangeCursor(filter, sort, rangeRequestModel.SkipInt, rangeRequestModel.TakeInt);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> RangeCursorAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, RangeRequestModel rangeRequestModel)
            => await FindCursorAsync(filter, GetRangeFindOptions(sort, rangeRequestModel.SkipInt, rangeRequestModel.TakeInt));
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> RangeCursor(FilterDefinition<T> filter, Expression<Func<T, object>> sort, RangeRequestModel rangeRequestModel)
            => RangeCursor(filter, sort, rangeRequestModel.SkipInt, rangeRequestModel.TakeInt);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> RangeCursorAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, RangeRequestModel rangeRequestModel)
            => await FindCursorAsync(filter, GetRangeFindOptions(sort, sortOrder, rangeRequestModel.SkipInt, rangeRequestModel.TakeInt));
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> RangeCursor(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, RangeRequestModel rangeRequestModel)
            => FindCursor(filter, GetRangeFindOptions(sort, sortOrder, rangeRequestModel.SkipInt, rangeRequestModel.TakeInt));
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> RangeCursorAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, RangeRequestModel rangeRequestModel)
            => await RangeCursorAsync(filter, sort, rangeRequestModel.SkipInt, rangeRequestModel.TakeInt);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> RangeCursor(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, RangeRequestModel rangeRequestModel)
            => RangeCursor(filter, sort, rangeRequestModel.SkipInt, rangeRequestModel.TakeInt);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> RangeCursorAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, RangeRequestModel rangeRequestModel)
            => await RangeCursorAsync(filter, sort, sortOrder, rangeRequestModel.SkipInt, rangeRequestModel.TakeInt);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> RangeCursor(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, RangeRequestModel rangeRequestModel)
            => RangeCursor(filter, sort, sortOrder, rangeRequestModel.SkipInt, rangeRequestModel.TakeInt);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> RangeCursorAsync(FilterModel filterModel, int skip, int take)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await RangeCursorAsync(filter, skip, take);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> RangeCursor(FilterModel filterModel, int skip, int take)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return RangeCursor(filter, skip, take);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> RangeCursorAsync(FilterModel filterModel, SortDefinition<T> sort, int skip, int take)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await RangeCursorAsync(filter, sort, skip, take);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> RangeCursor(FilterModel filterModel, SortDefinition<T> sort, int skip, int take)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return RangeCursor(filter, sort, skip, take);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> RangeCursorAsync(FilterModel filterModel, Expression<Func<T, object>> sort, int skip, int take)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await RangeCursorAsync(filter, sort, skip, take);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> RangeCursor(FilterModel filterModel, Expression<Func<T, object>> sort, int skip, int take)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return RangeCursor(filter, sort, skip, take);
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
        public virtual async Task<IAsyncCursor<T>> RangeCursorAsync(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int skip, int take)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await RangeCursorAsync(filter, sort, sortOrder, skip, take);
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
        public virtual IAsyncCursor<T> RangeCursor(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int skip, int take)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return RangeCursor(filter, sort, sortOrder, skip, take);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> RangeCursorAsync(FilterModel filterModel, RangeRequestModel rangeRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await RangeCursorAsync(filter, rangeRequestModel);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> RangeCursor(FilterModel filterModel, RangeRequestModel rangeRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return RangeCursor(filter, rangeRequestModel);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> RangeCursorAsync(FilterModel filterModel, SortDefinition<T> sort, RangeRequestModel rangeRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await RangeCursorAsync(filter, sort, rangeRequestModel);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> RangeCursor(FilterModel filterModel, SortDefinition<T> sort, RangeRequestModel rangeRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return RangeCursor(filter, sort, rangeRequestModel);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> RangeCursorAsync(FilterModel filterModel, Expression<Func<T, object>> sort, RangeRequestModel rangeRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await RangeCursorAsync(filter, sort, rangeRequestModel);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> RangeCursor(FilterModel filterModel, Expression<Func<T, object>> sort, RangeRequestModel rangeRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return RangeCursor(filter, sort, rangeRequestModel);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> RangeCursorAsync(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, RangeRequestModel rangeRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await RangeCursorAsync(filter, sort, sortOrder, rangeRequestModel);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> RangeCursor(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, RangeRequestModel rangeRequestModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return RangeCursor(filter, sort, sortOrder, rangeRequestModel);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> RangeCursorAsync(PageRequestModel filterModel, SortDefinition<T> sort)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await RangeCursorAsync(filter, sort, filterModel);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> RangeCursor(PageRequestModel filterModel, SortDefinition<T> sort)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return RangeCursor(filter, sort, filterModel);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> RangeCursorAsync(PageRequestModel filterModel, Expression<Func<T, object>> sort)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await RangeCursorAsync(filter, sort, filterModel);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> RangeCursor(PageRequestModel filterModel, Expression<Func<T, object>> sort)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return RangeCursor(filter, sort, filterModel);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> RangeCursorAsync(PageRequestModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await RangeCursorAsync(filter, sort, sortOrder, filterModel);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> RangeCursor(PageRequestModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return RangeCursor(filter, sort, sortOrder, filterModel);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual async Task<IAsyncCursor<T>> RangeCursorAsync(PageRequestModel filterModel)
        {
            SortDefinition<T>? sort = filterModel.GetSortDefinition<T>();
            return sort is null ? await RangeCursorAsync(filterModel, filterModel) : await RangeCursorAsync(filterModel, sort, filterModel);
        }
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual IAsyncCursor<T> RangeCursor(PageRequestModel filterModel)
        {
            SortDefinition<T>? sort = filterModel.GetSortDefinition<T>();
            return sort is null ? RangeCursor(filterModel, filterModel) : RangeCursor(filterModel, sort, filterModel);
        }
    }
}
