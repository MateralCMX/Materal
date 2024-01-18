namespace Materal.TTA.Common
{
    /// <summary>
    /// 读写分离仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISubordinateRepository<T> : IRepository<T>
        where T : class, IEntity
    {
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        bool ExistedFromSubordinate(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        Task<bool> ExistedFromSubordinateAsync(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <returns></returns>
        bool ExistedFromSubordinate(FilterModel filterModel);
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <returns></returns>
        Task<bool> ExistedFromSubordinateAsync(FilterModel filterModel);
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        int CountFromSubordinate(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        Task<int> CountFromSubordinateAsync(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <returns></returns>
        int CountFromSubordinate(FilterModel filterModel);
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <returns></returns>
        Task<int> CountFromSubordinateAsync(FilterModel filterModel);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        List<T> FindFromSubordinate(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        Task<List<T>> FindFromSubordinateAsync(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <returns></returns>
        List<T> FindFromSubordinate(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <returns></returns>
        Task<List<T>> FindFromSubordinateAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <returns></returns>
        List<T> FindFromSubordinate(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression, SortOrderEnum sortOrder);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <returns></returns>
        Task<List<T>> FindFromSubordinateAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression, SortOrderEnum sortOrder);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <returns></returns>
        List<T> FindFromSubordinate(FilterModel filterModel);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <returns></returns>
        Task<List<T>> FindFromSubordinateAsync(FilterModel filterModel);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <returns></returns>
        List<T> FindFromSubordinate(FilterModel filterModel, Expression<Func<T, object>> orderExpression);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <returns></returns>
        Task<List<T>> FindFromSubordinateAsync(FilterModel filterModel, Expression<Func<T, object>> orderExpression);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <returns></returns>
        List<T> FindFromSubordinate(FilterModel filterModel, Expression<Func<T, object>> orderExpression, SortOrderEnum sortOrder);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <returns></returns>
        Task<List<T>> FindFromSubordinateAsync(FilterModel filterModel, Expression<Func<T, object>> orderExpression, SortOrderEnum sortOrder);
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <returns></returns>
        T FirstFromSubordinate(FilterModel filterModel);
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <returns></returns>
        Task<T> FirstFromSubordinateAsync(FilterModel filterModel);
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        T FirstFromSubordinate(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<T> FirstFromSubordinateAsync(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <returns></returns>
        T? FirstOrDefaultFromSubordinate(FilterModel filterModel);
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <returns></returns>
        Task<T?> FirstOrDefaultFromSubordinateAsync(FilterModel filterModel);
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        T? FirstOrDefaultFromSubordinate(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<T?> FirstOrDefaultFromSubordinateAsync(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequestModel">分页查询请求模型</param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) PagingFromSubordinate(PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequestModel">分页查询请求模型</param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingFromSubordinateAsync(PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequestModel">分页查询请求模型</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) PagingFromSubordinate(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequestModel">分页查询请求模型</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingFromSubordinateAsync(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequestModel">分页查询请求模型</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) PagingFromSubordinate(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression, SortOrderEnum sortOrder);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequestModel">分页查询请求模型</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingFromSubordinateAsync(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression, SortOrderEnum sortOrder);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="pageRequestModel">分页查询请求模型</param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) PagingFromSubordinate(Expression<Func<T, bool>> filterExpression, PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="pageRequestModel">分页查询请求模型</param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) PagingFromSubordinate(Expression<Func<T, bool>> filterExpression, long pageIndex, long pageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, long pageIndex, long pageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="pageRequestModel">分页查询请求模型</param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) PagingFromSubordinate(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="pageRequestModel">分页查询请求模型</param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <param name="pageRequestModel">分页查询请求模型</param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) PagingFromSubordinate(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrderEnum sortOrder, PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <param name="pageRequestModel">分页查询请求模型</param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrderEnum sortOrder, PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) PagingFromSubordinate(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, long pageIndex, long pageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, long pageIndex, long pageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) PagingFromSubordinate(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrderEnum sortOrder, long pageIndex, long pageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrderEnum sortOrder, long pageIndex, long pageSize);
    }
    /// <summary>
    /// 读写分离仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public interface ISubordinateRepository<T, in TPrimaryKeyType> : ISubordinateRepository<T>, IRepository<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>
        where TPrimaryKeyType : struct
    {
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns></returns>
        bool ExistedFromSubordinate(TPrimaryKeyType id);
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns></returns>
        Task<bool> ExistedFromSubordinateAsync(TPrimaryKeyType id);
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T? FirstOrDefaultFromSubordinate(TPrimaryKeyType id);
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T?> FirstOrDefaultFromSubordinateAsync(TPrimaryKeyType id);
    }
}
