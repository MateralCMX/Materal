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
        Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterDefinition<T> filter, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        (List<T> data, RangeModel rangeInfo) Range(FilterDefinition<T> filter, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(Expression<Func<T, bool>> filter, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        (List<T> data, RangeModel rangeInfo) Range(Expression<Func<T, bool>> filter, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterDefinition<T> filter, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        (List<T> data, RangeModel rangeInfo) Range(FilterDefinition<T> filter, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(Expression<Func<T, bool>> filter, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        (List<T> data, RangeModel rangeInfo) Range(Expression<Func<T, bool>> filter, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterDefinition<T> filter, SortDefinition<T> sort, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        (List<T> data, RangeModel rangeInfo) Range(FilterDefinition<T> filter, SortDefinition<T> sort, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(Expression<Func<T, bool>> filter, SortDefinition<T> sort, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        (List<T> data, RangeModel rangeInfo) Range(Expression<Func<T, bool>> filter, SortDefinition<T> sort, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        (List<T> data, RangeModel rangeInfo) Range(FilterDefinition<T> filter, Expression<Func<T, object>> sort, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        (List<T> data, RangeModel rangeInfo) Range(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        (List<T> data, RangeModel rangeInfo) Range(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        (List<T> data, RangeModel rangeInfo) Range(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterDefinition<T> filter, SortDefinition<T> sort, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        (List<T> data, RangeModel rangeInfo) Range(FilterDefinition<T> filter, SortDefinition<T> sort, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(Expression<Func<T, bool>> filter, SortDefinition<T> sort, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        (List<T> data, RangeModel rangeInfo) Range(Expression<Func<T, bool>> filter, SortDefinition<T> sort, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        (List<T> data, RangeModel rangeInfo) Range(FilterDefinition<T> filter, Expression<Func<T, object>> sort, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        (List<T> data, RangeModel rangeInfo) Range(FilterDefinition<T> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        (List<T> data, RangeModel rangeInfo) Range(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        (List<T> data, RangeModel rangeInfo) Range(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterModel filterModel, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        (List<T> data, RangeModel rangeInfo) Range(FilterModel filterModel, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterModel filterModel, SortDefinition<T> sort, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        (List<T> data, RangeModel rangeInfo) Range(FilterModel filterModel, SortDefinition<T> sort, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterModel filterModel, Expression<Func<T, object>> sort, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        (List<T> data, RangeModel rangeInfo) Range(FilterModel filterModel, Expression<Func<T, object>> sort, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        (List<T> data, RangeModel rangeInfo) Range(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, int skip, int take);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterModel filterModel, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        (List<T> data, RangeModel rangeInfo) Range(FilterModel filterModel, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterModel filterModel, SortDefinition<T> sort, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        (List<T> data, RangeModel rangeInfo) Range(FilterModel filterModel, SortDefinition<T> sort, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterModel filterModel, Expression<Func<T, object>> sort, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        (List<T> data, RangeModel rangeInfo) Range(FilterModel filterModel, Expression<Func<T, object>> sort, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        (List<T> data, RangeModel rangeInfo) Range(FilterModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(RangeRequestModel filterModel, SortDefinition<T> sort);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        (List<T> data, RangeModel rangeInfo) Range(RangeRequestModel filterModel, SortDefinition<T> sort);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(RangeRequestModel filterModel, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        (List<T> data, RangeModel rangeInfo) Range(RangeRequestModel filterModel, Expression<Func<T, object>> sort);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(RangeRequestModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        (List<T> data, RangeModel rangeInfo) Range(RangeRequestModel filterModel, Expression<Func<T, object>> sort, SortOrderEnum sortOrder);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        Task<(List<T> data, RangeModel rangeInfo)> RangeAsync(RangeRequestModel filterModel);
        /// <summary>
        /// 查询范围数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        (List<T> data, RangeModel rangeInfo) Range(RangeRequestModel filterModel);
    }
}
