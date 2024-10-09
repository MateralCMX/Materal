using System.Data;

namespace Materal.TTA.Common
{
    /// <summary>
    /// 公共仓储实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class CommonRepositoryImpl<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual bool Existed(Expression<Func<TEntity, bool>> expression) => Count(expression) > 0;
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual async Task<bool> ExistedAsync(Expression<Func<TEntity, bool>> expression) => await CountAsync(expression) > 0;
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual bool Existed(FilterModel filterModel) => Existed(filterModel.GetSearchExpression<TEntity>());
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual async Task<bool> ExistedAsync(FilterModel filterModel) => await ExistedAsync(filterModel.GetSearchExpression<TEntity>());
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public abstract int Count(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> expression) => Task.FromResult(Count(expression));
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual int Count(FilterModel filterModel) => Count(filterModel.GetSearchExpression<TEntity>());
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual async Task<int> CountAsync(FilterModel filterModel) => await CountAsync(filterModel.GetSearchExpression<TEntity>());
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public abstract List<TEntity> Find(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderExpression"></param>
        /// <returns></returns>
        public virtual List<TEntity> Find(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderExpression) => Find(expression, orderExpression, SortOrderEnum.Ascending);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public abstract List<TEntity> Find(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public abstract Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderExpression"></param>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderExpression) => await FindAsync(expression, orderExpression, SortOrderEnum.Ascending);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder)
            => Task.FromResult(Find(expression, orderExpression, sortOrder));
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual List<TEntity> Find(FilterModel filterModel) => Find(filterModel.GetSearchExpression<TEntity>());
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="orderExpression"></param>
        /// <returns></returns>
        public virtual List<TEntity> Find(FilterModel filterModel, Expression<Func<TEntity, object>> orderExpression) => Find(filterModel.GetSearchExpression<TEntity>(), orderExpression);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual List<TEntity> Find(FilterModel filterModel, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder) => Find(filterModel.GetSearchExpression<TEntity>(), orderExpression, sortOrder);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> FindAsync(FilterModel filterModel) => await FindAsync(filterModel.GetSearchExpression<TEntity>());
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="orderExpression"></param>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> FindAsync(FilterModel filterModel, Expression<Func<TEntity, object>> orderExpression) => await FindAsync(filterModel.GetSearchExpression<TEntity>(), orderExpression);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> FindAsync(FilterModel filterModel, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder) => await FindAsync(filterModel.GetSearchExpression<TEntity>(), orderExpression, sortOrder);
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        public virtual TEntity First(FilterModel filterModel) => FirstOrDefault(filterModel) ?? throw new TTAException("数据不存在");
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        public virtual async Task<TEntity> FirstAsync(FilterModel filterModel) => await FirstOrDefaultAsync(filterModel) ?? throw new TTAException("数据不存在");
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        public virtual TEntity First(Expression<Func<TEntity, bool>> expression) => FirstOrDefault(expression) ?? throw new TTAException("数据不存在");
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        public virtual async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression) => await FirstOrDefaultAsync(expression) ?? throw new TTAException("数据不存在");
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual TEntity? FirstOrDefault(FilterModel filterModel) => FirstOrDefault(filterModel.GetSearchExpression<TEntity>());
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual async Task<TEntity?> FirstOrDefaultAsync(FilterModel filterModel) => await FirstOrDefaultAsync(filterModel.GetSearchExpression<TEntity>());
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> expression) => Paging(expression, 1, 1).data.FirstOrDefault();
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression) => Task.FromResult(FirstOrDefault(expression));
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual (List<TEntity> data, PageModel pageInfo) Paging(PageRequestModel pageRequestModel)
        {
            Expression<Func<TEntity, object>>? orderExpression = pageRequestModel.GetSortExpression<TEntity>();
            SortOrderEnum sortOrder = pageRequestModel.IsAsc ? SortOrderEnum.Ascending : SortOrderEnum.Descending;
            Expression<Func<TEntity, bool>> filterExpression = pageRequestModel.GetSearchExpression<TEntity>();
            if (orderExpression == null)
            {
                return Paging(filterExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
            }
            else
            {
                return Paging(filterExpression, orderExpression, sortOrder, pageRequestModel.PageIndex, pageRequestModel.PageSize);
            }
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual (List<TEntity> data, PageModel pageInfo) Paging(Expression<Func<TEntity, bool>> filterExpression, PageRequestModel pageRequestModel)
        {
            Expression<Func<TEntity, object>>? orderExpression = pageRequestModel.GetSortExpression<TEntity>();
            SortOrderEnum sortOrder = pageRequestModel.IsAsc ? SortOrderEnum.Ascending : SortOrderEnum.Descending;
            if (orderExpression == null)
            {
                return Paging(filterExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
            }
            else
            {
                return Paging(filterExpression, orderExpression, sortOrder, pageRequestModel.PageIndex, pageRequestModel.PageSize);
            }
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequestModel"></param>
        /// <param name="orderExpression"></param>
        /// <returns></returns>
        public virtual (List<TEntity> data, PageModel pageInfo) Paging(PageRequestModel pageRequestModel, Expression<Func<TEntity, object>> orderExpression)
            => Paging(pageRequestModel.GetSearchExpression<TEntity>(), orderExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequestModel"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual (List<TEntity> data, PageModel pageInfo) Paging(PageRequestModel pageRequestModel, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder)
            => Paging(pageRequestModel.GetSearchExpression<TEntity>(), orderExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public abstract (List<TEntity> data, PageModel pageInfo) Paging(Expression<Func<TEntity, bool>> filterExpression, long pageIndex, long pageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual (List<TEntity> data, PageModel pageInfo) Paging(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, PageRequestModel pageRequestModel)
            => Paging(filterExpression, orderExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual (List<TEntity> data, PageModel pageInfo) Paging(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder, PageRequestModel pageRequestModel)
            => Paging(filterExpression, orderExpression, sortOrder, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual (List<TEntity> data, PageModel pageInfo) Paging(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, long pageIndex, long pageSize)
            => Paging(filterExpression, orderExpression, SortOrderEnum.Ascending, pageIndex, pageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public abstract (List<TEntity> data, PageModel pageInfo) Paging(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder, long pageIndex, long pageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<TEntity> data, PageModel pageInfo)> PagingAsync(PageRequestModel pageRequestModel)
        {
            Expression<Func<TEntity, object>>? orderExpression = pageRequestModel.GetSortExpression<TEntity>();
            SortOrderEnum sortOrder = pageRequestModel.IsAsc ? SortOrderEnum.Ascending : SortOrderEnum.Descending;
            Expression<Func<TEntity, bool>> filterExpression = pageRequestModel.GetSearchExpression<TEntity>();
            if (orderExpression == null)
            {
                return await PagingAsync(filterExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
            }
            else
            {
                return await PagingAsync(filterExpression, orderExpression, sortOrder, pageRequestModel.PageIndex, pageRequestModel.PageSize);
            }
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequestModel"></param>
        /// <param name="orderExpression"></param>
        /// <returns></returns>
        public virtual async Task<(List<TEntity> data, PageModel pageInfo)> PagingAsync(PageRequestModel pageRequestModel, Expression<Func<TEntity, object>> orderExpression)
            => await PagingAsync(pageRequestModel.GetSearchExpression<TEntity>(), orderExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequestModel"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual async Task<(List<TEntity> data, PageModel pageInfo)> PagingAsync(PageRequestModel pageRequestModel, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder)
            => await PagingAsync(pageRequestModel.GetSearchExpression<TEntity>(), orderExpression, sortOrder, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<TEntity> data, PageModel pageInfo)> PagingAsync(Expression<Func<TEntity, bool>> filterExpression, PageRequestModel pageRequestModel)
        {
            Expression<Func<TEntity, object>>? orderExpression = pageRequestModel.GetSortExpression<TEntity>();
            SortOrderEnum sortOrder = pageRequestModel.IsAsc ? SortOrderEnum.Ascending : SortOrderEnum.Descending;
            if (orderExpression == null)
            {
                return await PagingAsync(filterExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
            }
            else
            {
                return await PagingAsync(filterExpression, orderExpression, sortOrder, pageRequestModel.PageIndex, pageRequestModel.PageSize);
            }
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public abstract Task<(List<TEntity> data, PageModel pageInfo)> PagingAsync(Expression<Func<TEntity, bool>> filterExpression, long pageIndex, long pageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<TEntity> data, PageModel pageInfo)> PagingAsync(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, PageRequestModel pageRequestModel)
            => await PagingAsync(filterExpression, orderExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<TEntity> data, PageModel pageInfo)> PagingAsync(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder, PageRequestModel pageRequestModel)
            => await PagingAsync(filterExpression, orderExpression, sortOrder, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<(List<TEntity> data, PageModel pageInfo)> PagingAsync(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, long pageIndex, long pageSize)
            => await PagingAsync(filterExpression, orderExpression, SortOrderEnum.Ascending, pageIndex, pageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual Task<(List<TEntity> data, PageModel pageInfo)> PagingAsync(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder, long pageIndex, long pageSize)
            => Task.FromResult(Paging(filterExpression, orderExpression, sortOrder, pageIndex, pageSize));
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual (List<TEntity> data, RangeModel rangeInfo) Range(RangeRequestModel rangeRequestModel)
        {
            Expression<Func<TEntity, object>>? orderExpression = rangeRequestModel.GetSortExpression<TEntity>();
            SortOrderEnum sortOrder = rangeRequestModel.IsAsc ? SortOrderEnum.Ascending : SortOrderEnum.Descending;
            Expression<Func<TEntity, bool>> filterExpression = rangeRequestModel.GetSearchExpression<TEntity>();
            if (orderExpression == null)
            {
                return Range(filterExpression, rangeRequestModel.Skip, rangeRequestModel.Take);
            }
            else
            {
                return Range(filterExpression, orderExpression, sortOrder, rangeRequestModel.Skip, rangeRequestModel.Take);
            }
        }
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual (List<TEntity> data, RangeModel rangeInfo) Range(Expression<Func<TEntity, bool>> filterExpression, RangeRequestModel rangeRequestModel)
        {
            Expression<Func<TEntity, object>>? orderExpression = rangeRequestModel.GetSortExpression<TEntity>();
            SortOrderEnum sortOrder = rangeRequestModel.IsAsc ? SortOrderEnum.Ascending : SortOrderEnum.Descending;
            if (orderExpression == null)
            {
                return Range(filterExpression, rangeRequestModel.Skip, rangeRequestModel.Take);
            }
            else
            {
                return Range(filterExpression, orderExpression, sortOrder, rangeRequestModel.Skip, rangeRequestModel.Take);
            }
        }
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="rangeRequestModel"></param>
        /// <param name="orderExpression"></param>
        /// <returns></returns>
        public virtual (List<TEntity> data, RangeModel rangeInfo) Range(RangeRequestModel rangeRequestModel, Expression<Func<TEntity, object>> orderExpression)
            => Range(rangeRequestModel.GetSearchExpression<TEntity>(), orderExpression, rangeRequestModel.Skip, rangeRequestModel.Take);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="rangeRequestModel"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual (List<TEntity> data, RangeModel rangeInfo) Range(RangeRequestModel rangeRequestModel, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder)
            => Range(rangeRequestModel.GetSearchExpression<TEntity>(), orderExpression, rangeRequestModel.Skip, rangeRequestModel.Take);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public abstract (List<TEntity> data, RangeModel rangeInfo) Range(Expression<Func<TEntity, bool>> filterExpression, long skip, long take);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual (List<TEntity> data, RangeModel rangeInfo) Range(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, RangeRequestModel rangeRequestModel)
            => Range(filterExpression, orderExpression, rangeRequestModel.Skip, rangeRequestModel.Take);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual (List<TEntity> data, RangeModel rangeInfo) Range(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder, RangeRequestModel rangeRequestModel)
            => Range(filterExpression, orderExpression, sortOrder, rangeRequestModel.Skip, rangeRequestModel.Take);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual (List<TEntity> data, RangeModel rangeInfo) Range(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, long skip, long take)
            => Range(filterExpression, orderExpression, SortOrderEnum.Ascending, skip, take);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public abstract (List<TEntity> data, RangeModel rangeInfo) Range(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder, long skip, long take);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<TEntity> data, RangeModel rangeInfo)> RangeAsync(RangeRequestModel rangeRequestModel)
        {
            Expression<Func<TEntity, object>>? orderExpression = rangeRequestModel.GetSortExpression<TEntity>();
            SortOrderEnum sortOrder = rangeRequestModel.IsAsc ? SortOrderEnum.Ascending : SortOrderEnum.Descending;
            Expression<Func<TEntity, bool>> filterExpression = rangeRequestModel.GetSearchExpression<TEntity>();
            if (orderExpression == null)
            {
                return await RangeAsync(filterExpression, rangeRequestModel.Skip, rangeRequestModel.Take);
            }
            else
            {
                return await RangeAsync(filterExpression, orderExpression, sortOrder, rangeRequestModel.Skip, rangeRequestModel.Take);
            }
        }
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="rangeRequestModel"></param>
        /// <param name="orderExpression"></param>
        /// <returns></returns>
        public virtual async Task<(List<TEntity> data, RangeModel rangeInfo)> RangeAsync(RangeRequestModel rangeRequestModel, Expression<Func<TEntity, object>> orderExpression)
            => await RangeAsync(rangeRequestModel.GetSearchExpression<TEntity>(), orderExpression, rangeRequestModel.Skip, rangeRequestModel.Take);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="rangeRequestModel"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual async Task<(List<TEntity> data, RangeModel rangeInfo)> RangeAsync(RangeRequestModel rangeRequestModel, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder)
            => await RangeAsync(rangeRequestModel.GetSearchExpression<TEntity>(), orderExpression, sortOrder, rangeRequestModel.Skip, rangeRequestModel.Take);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<TEntity> data, RangeModel rangeInfo)> RangeAsync(Expression<Func<TEntity, bool>> filterExpression, RangeRequestModel rangeRequestModel)
        {
            Expression<Func<TEntity, object>>? orderExpression = rangeRequestModel.GetSortExpression<TEntity>();
            SortOrderEnum sortOrder = rangeRequestModel.IsAsc ? SortOrderEnum.Ascending : SortOrderEnum.Descending;
            if (orderExpression == null)
            {
                return await RangeAsync(filterExpression, rangeRequestModel.Skip, rangeRequestModel.Take);
            }
            else
            {
                return await RangeAsync(filterExpression, orderExpression, sortOrder, rangeRequestModel.Skip, rangeRequestModel.Take);
            }
        }
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public abstract Task<(List<TEntity> data, RangeModel rangeInfo)> RangeAsync(Expression<Func<TEntity, bool>> filterExpression, long skip, long take);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<TEntity> data, RangeModel rangeInfo)> RangeAsync(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, RangeRequestModel rangeRequestModel)
            => await RangeAsync(filterExpression, orderExpression, rangeRequestModel.Skip, rangeRequestModel.Take);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<TEntity> data, RangeModel rangeInfo)> RangeAsync(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder, RangeRequestModel rangeRequestModel)
            => await RangeAsync(filterExpression, orderExpression, sortOrder, rangeRequestModel.Skip, rangeRequestModel.Take);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<(List<TEntity> data, RangeModel rangeInfo)> RangeAsync(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, long skip, long take)
            => await RangeAsync(filterExpression, orderExpression, SortOrderEnum.Ascending, skip, take);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual Task<(List<TEntity> data, RangeModel rangeInfo)> RangeAsync(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder, long skip, long take)
            => Task.FromResult(Range(filterExpression, orderExpression, sortOrder, skip, take));
        /// <summary>
        /// 获得排序属性名称
        /// </summary>
        /// <param name="orderExpression"></param>
        /// <returns></returns>
        protected string? GetSortPropertyName(Expression<Func<TEntity, object>> orderExpression)
        {
            Expression propertyExpression = orderExpression.Body;
            if (propertyExpression is UnaryExpression unaryExpression)
            {
                propertyExpression = unaryExpression.Operand;
            }
            if (propertyExpression is not MemberExpression memberExpression) return null;
            return memberExpression.Member.Name;
        }
    }
    /// <summary>
    /// 公共仓储实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public abstract class CommonRepositoryImpl<TEntity, TPrimaryKeyType> : CommonRepositoryImpl<TEntity>, IRepository<TEntity, TPrimaryKeyType>
        where TEntity : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
    {
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Existed(TPrimaryKeyType id) => Existed(m => m.ID.Equals(id));
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task<bool> ExistedAsync(TPrimaryKeyType id) => Task.FromResult(Existed(id));
        /// <summary>
        /// 获得第一条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity First(TPrimaryKeyType id) => First(m => m.ID.Equals(id));
        /// <summary>
        /// 获得第一条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FirstAsync(TPrimaryKeyType id) => Task.FromResult(First(id));
        /// <summary>
        /// 获得第一条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity? FirstOrDefault(TPrimaryKeyType id) => FirstOrDefault(m => m.ID.Equals(id));
        /// <summary>
        /// 获得第一条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task<TEntity?> FirstOrDefaultAsync(TPrimaryKeyType id) => Task.FromResult(FirstOrDefault(id));
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public override List<TEntity> Find(Expression<Func<TEntity, bool>> expression) => Find(expression, m => m.ID, SortOrderEnum.Ascending);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public override async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression) => await FindAsync(expression, m => m.ID, SortOrderEnum.Ascending);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public override (List<TEntity> data, PageModel pageInfo) Paging(Expression<Func<TEntity, bool>> filterExpression, long pageIndex, long pageSize)
            => Paging(filterExpression, m => m.ID, pageIndex, pageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public override async Task<(List<TEntity> data, PageModel pageInfo)> PagingAsync(Expression<Func<TEntity, bool>> filterExpression, long pageIndex, long pageSize)
            => await PagingAsync(filterExpression, m => m.ID, pageIndex, pageSize);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public override (List<TEntity> data, RangeModel rangeInfo) Range(Expression<Func<TEntity, bool>> filterExpression, long skip, long take)
            => Range(filterExpression, m => m.ID, skip, take);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public override async Task<(List<TEntity> data, RangeModel rangeInfo)> RangeAsync(Expression<Func<TEntity, bool>> filterExpression, long skip, long take)
            => await RangeAsync(filterExpression, m => m.ID, skip, take);
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        protected abstract string GetConnectionString();
        /// <summary>
        /// 获取连接
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        protected abstract IDbConnection GetConnection(string connectionString);
        /// <summary>
        /// 获取连接
        /// </summary>
        /// <returns></returns>
        protected virtual IDbConnection GetConnection()
        {
            string connectionString = GetConnectionString();
            return GetConnection(connectionString);
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="tSql"></param>
        /// <returns></returns>
        protected virtual void ExcuteSql(string tSql)
            => ExcuteSql(tSql, null, null);
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="tSql"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        protected virtual void ExcuteSql(string tSql, ICollection<IDataParameter> sqlParameters)
            => ExcuteSql(tSql, sqlParameters, null);
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="tSql"></param>
        /// <param name="onHandler"></param>
        /// <returns></returns>
        protected virtual void ExcuteSql(string tSql, Action<IDataReader> onHandler)
            => ExcuteSql(tSql, null, onHandler);
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="tSql"></param>
        /// <param name="sqlParameters"></param>
        /// <param name="onHandler"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        protected virtual void ExcuteSql(string tSql, ICollection<IDataParameter>? sqlParameters, Action<IDataReader>? onHandler)
        {
            using IDbConnection sqlConnection = GetConnection();
            try
            {
                sqlConnection.Open();
                IDbCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = tSql;
                if (sqlParameters is not null && sqlParameters.Count > 0)
                {
                    foreach (IDataParameter sqlParameter in sqlParameters)
                    {
                        sqlCommand.Parameters.Add(sqlParameter);
                    }
                }
                IDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    onHandler?.Invoke(sqlDataReader);
                }
                sqlDataReader.Close();
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        /// <summary>
        /// 执行查询SQL语句
        /// </summary>
        /// <param name="tSql"></param>
        /// <returns></returns>
        protected virtual List<TEntity> ExcuteQuerySql(string tSql)
            => ExcuteQuerySql(tSql, null, null);
        /// <summary>
        /// 执行查询SQL语句
        /// </summary>
        /// <param name="tSql"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        protected virtual List<TEntity> ExcuteQuerySql(string tSql, ICollection<IDataParameter> sqlParameters)
            => ExcuteQuerySql(tSql, sqlParameters, null);
        /// <summary>
        /// 执行查询SQL语句
        /// </summary>
        /// <param name="tSql"></param>
        /// <param name="onHandler"></param>
        /// <returns></returns>
        protected virtual List<TEntity> ExcuteQuerySql(string tSql, Func<IDataReader, TEntity> onHandler)
            => ExcuteQuerySql(tSql, null, onHandler);
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="tSql"></param>
        /// <param name="sqlParameters"></param>
        /// <param name="onHandler"></param>
        /// <returns></returns>
        protected virtual List<TEntity> ExcuteQuerySql(string tSql, ICollection<IDataParameter>? sqlParameters, Func<IDataReader, TEntity>? onHandler)
        {
            List<TEntity> result = [];
            ExcuteSql(tSql, sqlParameters, dr =>
            {
                if (onHandler is null)
                {
                    result.Add(BindData<TEntity>(dr));
                }
                else
                {
                    result.Add(onHandler.Invoke(dr));
                }
            });
            return result;
        }
        /// <summary>
        /// 执行非查询SQL语句
        /// </summary>
        /// <param name="tSql"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        protected virtual int ExecuteNonQuery(string tSql, ICollection<IDataParameter> sqlParameters)
        {
            using IDbConnection sqlConnection = GetConnection();
            try
            {
                sqlConnection.Open();
                IDbCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = tSql;
                if (sqlParameters is not null && sqlParameters.Count > 0)
                {
                    foreach (IDataParameter sqlParameter in sqlParameters)
                    {
                        sqlCommand.Parameters.Add(sqlParameter);
                    }
                }
                int result = sqlCommand.ExecuteNonQuery();
                return result;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="sqlDataReader"></param>
        /// <returns></returns>
        protected virtual TModel BindData<TModel>(IDataReader sqlDataReader)
            where TModel : new()
        {
            PropertyInfo[] propertyInfos = typeof(TModel).GetProperties();
            TModel result = new();
            MethodInfo getFieldValuemethodInfo = sqlDataReader.GetType().GetMethod("GetFieldValue") ?? throw new TTAException("获取字段值方法获取失败");
            for (int i = 0; i < sqlDataReader.FieldCount; i++)
            {
                string name = sqlDataReader.GetName(i);
                PropertyInfo? propertyInfo = propertyInfos.FirstOrDefault(m => m.Name == name);
                if (propertyInfo is null) continue;
                object? value;
                if (!sqlDataReader.IsDBNull(i))
                {
                    Type propertyType = propertyInfo.PropertyType;
                    if (propertyType.IsGenericType && propertyType.GenericTypeArguments.Length == 1 && propertyType == typeof(Nullable<>).MakeGenericType(propertyType.GenericTypeArguments[0]))
                    {
                        propertyType = propertyType.GenericTypeArguments[0];
                    }
                    value = getFieldValuemethodInfo.MakeGenericMethod(propertyType).Invoke(sqlDataReader, [i]);
                }
                else
                {
                    value = null;
                }
                propertyInfo.SetValue(result, value);
            }
            return result;
        }
        /// <summary>
        /// 绑定列表
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="sqlDataReader"></param>
        /// <param name="onHandler"></param>
        /// <returns></returns>
        protected virtual List<TModel> BindList<TModel>(IDataReader sqlDataReader, Func<IDataReader, TModel>? onHandler = null)
            where TModel : new()
        {
            List<TModel> result = [];
            while (sqlDataReader.Read())
            {
                if (onHandler is null)
                {
                    result.Add(BindData<TModel>(sqlDataReader));
                }
                else
                {
                    result.Add(onHandler.Invoke(sqlDataReader));
                }
            }
            sqlDataReader.Close();
            return result;
        }
    }
}
