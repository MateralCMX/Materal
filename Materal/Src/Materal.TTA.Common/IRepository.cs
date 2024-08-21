namespace Materal.TTA.Common
{
    /// <summary>
    /// 仓储
    /// </summary>
    public interface IRepository
    {
    }
    /// <summary>
    /// 仓储
    /// </summary>
    /// <typeparam name="TEntity">实体对象</typeparam>
    public interface IRepository<TEntity> : IRepository
        where TEntity : class, IEntity
    {
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        bool Existed(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        Task<bool> ExistedAsync(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <returns></returns>
        bool Existed(FilterModel filterModel);
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <returns></returns>
        Task<bool> ExistedAsync(FilterModel filterModel);
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <returns></returns>
        int Count(FilterModel filterModel);
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <returns></returns>
        Task<int> CountAsync(FilterModel filterModel);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        List<TEntity> Find(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <returns></returns>
        List<TEntity> Find(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderExpression);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <returns></returns>
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderExpression);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <returns></returns>
        List<TEntity> Find(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <returns></returns>
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <returns></returns>
        List<TEntity> Find(FilterModel filterModel);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <returns></returns>
        Task<List<TEntity>> FindAsync(FilterModel filterModel);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <returns></returns>
        List<TEntity> Find(FilterModel filterModel, Expression<Func<TEntity, object>> orderExpression);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <returns></returns>
        Task<List<TEntity>> FindAsync(FilterModel filterModel, Expression<Func<TEntity, object>> orderExpression);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <returns></returns>
        List<TEntity> Find(FilterModel filterModel, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <returns></returns>
        Task<List<TEntity>> FindAsync(FilterModel filterModel, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder);
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <returns></returns>
        TEntity First(FilterModel filterModel);
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <returns></returns>
        Task<TEntity> FirstAsync(FilterModel filterModel);
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        TEntity First(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <returns></returns>
        TEntity? FirstOrDefault(FilterModel filterModel);
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <returns></returns>
        Task<TEntity?> FirstOrDefaultAsync(FilterModel filterModel);
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequestModel">分页查询请求模型</param>
        /// <returns></returns>
        (List<TEntity> data, PageModel pageInfo) Paging(PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequestModel">分页查询请求模型</param>
        /// <returns></returns>
        Task<(List<TEntity> data, PageModel pageInfo)> PagingAsync(PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequestModel">分页查询请求模型</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <returns></returns>
        (List<TEntity> data, PageModel pageInfo) Paging(PageRequestModel pageRequestModel, Expression<Func<TEntity, object>> orderExpression);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequestModel">分页查询请求模型</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <returns></returns>
        Task<(List<TEntity> data, PageModel pageInfo)> PagingAsync(PageRequestModel pageRequestModel, Expression<Func<TEntity, object>> orderExpression);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequestModel">分页查询请求模型</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <returns></returns>
        (List<TEntity> data, PageModel pageInfo) Paging(PageRequestModel pageRequestModel, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequestModel">分页查询请求模型</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <returns></returns>
        Task<(List<TEntity> data, PageModel pageInfo)> PagingAsync(PageRequestModel pageRequestModel, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="pageRequestModel">分页查询请求模型</param>
        /// <returns></returns>
        (List<TEntity> data, PageModel pageInfo) Paging(Expression<Func<TEntity, bool>> filterExpression, PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="pageRequestModel">分页查询请求模型</param>
        /// <returns></returns>
        Task<(List<TEntity> data, PageModel pageInfo)> PagingAsync(Expression<Func<TEntity, bool>> filterExpression, PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <returns></returns>
        (List<TEntity> data, PageModel pageInfo) Paging(Expression<Func<TEntity, bool>> filterExpression, long pageIndex, long pageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <returns></returns>
        Task<(List<TEntity> data, PageModel pageInfo)> PagingAsync(Expression<Func<TEntity, bool>> filterExpression, long pageIndex, long pageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="pageRequestModel">分页查询请求模型</param>
        /// <returns></returns>
        (List<TEntity> data, PageModel pageInfo) Paging(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="pageRequestModel">分页查询请求模型</param>
        /// <returns></returns>
        Task<(List<TEntity> data, PageModel pageInfo)> PagingAsync(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <param name="pageRequestModel">分页查询请求模型</param>
        /// <returns></returns>
        (List<TEntity> data, PageModel pageInfo) Paging(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder, PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <param name="pageRequestModel">分页查询请求模型</param>
        /// <returns></returns>
        Task<(List<TEntity> data, PageModel pageInfo)> PagingAsync(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder, PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <returns></returns>
        (List<TEntity> data, PageModel pageInfo) Paging(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, long pageIndex, long pageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <returns></returns>
        Task<(List<TEntity> data, PageModel pageInfo)> PagingAsync(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, long pageIndex, long pageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <returns></returns>
        (List<TEntity> data, PageModel pageInfo) Paging(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder, long pageIndex, long pageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <returns></returns>
        Task<(List<TEntity> data, PageModel pageInfo)> PagingAsync(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder, long pageIndex, long pageSize);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="rangeRequestModel">范围查询请求模型</param>
        /// <returns></returns>
        (List<TEntity> data, RangeModel rangeInfo) Range(RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="rangeRequestModel">范围查询请求模型</param>
        /// <returns></returns>
        Task<(List<TEntity> data, RangeModel rangeInfo)> RangeAsync(RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="rangeRequestModel">范围查询请求模型</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <returns></returns>
        (List<TEntity> data, RangeModel rangeInfo) Range(RangeRequestModel rangeRequestModel, Expression<Func<TEntity, object>> orderExpression);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="rangeRequestModel">范围查询请求模型</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <returns></returns>
        Task<(List<TEntity> data, RangeModel rangeInfo)> RangeAsync(RangeRequestModel rangeRequestModel, Expression<Func<TEntity, object>> orderExpression);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="rangeRequestModel">范围查询请求模型</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <returns></returns>
        (List<TEntity> data, RangeModel rangeInfo) Range(RangeRequestModel rangeRequestModel, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="rangeRequestModel">范围查询请求模型</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <returns></returns>
        Task<(List<TEntity> data, RangeModel rangeInfo)> RangeAsync(RangeRequestModel rangeRequestModel, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="rangeRequestModel">范围查询请求模型</param>
        /// <returns></returns>
        (List<TEntity> data, RangeModel rangeInfo) Range(Expression<Func<TEntity, bool>> filterExpression, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="rangeRequestModel">范围查询请求模型</param>
        /// <returns></returns>
        Task<(List<TEntity> data, RangeModel rangeInfo)> RangeAsync(Expression<Func<TEntity, bool>> filterExpression, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="skip">页数</param>
        /// <param name="take">每页显示数量</param>
        /// <returns></returns>
        (List<TEntity> data, RangeModel rangeInfo) Range(Expression<Func<TEntity, bool>> filterExpression, long skip, long take);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="skip">页数</param>
        /// <param name="take">每页显示数量</param>
        /// <returns></returns>
        Task<(List<TEntity> data, RangeModel rangeInfo)> RangeAsync(Expression<Func<TEntity, bool>> filterExpression, long skip, long take);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="rangeRequestModel">范围查询请求模型</param>
        /// <returns></returns>
        (List<TEntity> data, RangeModel rangeInfo) Range(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="rangeRequestModel">范围查询请求模型</param>
        /// <returns></returns>
        Task<(List<TEntity> data, RangeModel rangeInfo)> RangeAsync(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <param name="rangeRequestModel">范围查询请求模型</param>
        /// <returns></returns>
        (List<TEntity> data, RangeModel rangeInfo) Range(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <param name="rangeRequestModel">范围查询请求模型</param>
        /// <returns></returns>
        Task<(List<TEntity> data, RangeModel rangeInfo)> RangeAsync(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder, RangeRequestModel rangeRequestModel);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="skip">页数</param>
        /// <param name="take">每页显示数量</param>
        /// <returns></returns>
        (List<TEntity> data, RangeModel rangeInfo) Range(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, long skip, long take);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="skip">页数</param>
        /// <param name="take">每页显示数量</param>
        /// <returns></returns>
        Task<(List<TEntity> data, RangeModel rangeInfo)> RangeAsync(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, long skip, long take);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <param name="skip">页数</param>
        /// <param name="take">每页显示数量</param>
        /// <returns></returns>
        (List<TEntity> data, RangeModel rangeInfo) Range(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder, long skip, long take);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <param name="skip">页数</param>
        /// <param name="take">每页显示数量</param>
        /// <returns></returns>
        Task<(List<TEntity> data, RangeModel rangeInfo)> RangeAsync(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder, long skip, long take);
    }
    /// <summary>
    /// 仓储
    /// </summary>
    /// <typeparam name="TEntity">实体对象</typeparam>
    /// <typeparam name="TPrimaryKeyType">主键类型</typeparam>
    public interface IRepository<TEntity, in TPrimaryKeyType> : IRepository<TEntity>
        where TEntity : class, IEntity<TPrimaryKeyType>
        where TPrimaryKeyType : struct
    {
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns></returns>
        bool Existed(TPrimaryKeyType id);
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns></returns>
        Task<bool> ExistedAsync(TPrimaryKeyType id);
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity First(TPrimaryKeyType id);
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> FirstAsync(TPrimaryKeyType id);
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity? FirstOrDefault(TPrimaryKeyType id);
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity?> FirstOrDefaultAsync(TPrimaryKeyType id);
    }
}
