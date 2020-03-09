using Materal.Model;
using Materal.TTA.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Materal.TTA.EFRepository
{
    public interface IEFRepository<T, in TPrimaryKeyType> : IRepository<T, TPrimaryKeyType> where T : class, IEntity<TPrimaryKeyType>
    {

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns></returns>
        Task<bool> ExistedAsync(TPrimaryKeyType id);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        Task<bool> ExistedAsync(Expression<Func<T, bool>> expression);
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
        Task<int> CountAsync(Expression<Func<T, bool>> expression);

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
        Task<List<T>> FindAsync(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <returns></returns>
        Task<List<T>> FindAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <returns></returns>
        Task<List<T>> FindAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <returns></returns>
        Task<List<T>> FindAsync(FilterModel filterModel);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <returns></returns>
        Task<List<T>> FindAsync(FilterModel filterModel, Expression<Func<T, object>> orderExpression);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <returns></returns>
        Task<List<T>> FindAsync(FilterModel filterModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder);
        /// <summary>
        /// 检索
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> FirstOrDefaultAsync(TPrimaryKeyType id);
        /// <summary>
        /// 检索
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <returns></returns>
        Task<T> FirstOrDefaultAsync(FilterModel filterModel);
        /// <summary>
        /// 检索
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        [Obsolete("请使用Where(expression).ToListAsync()")]
        IQueryable<T> WhereAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <returns></returns>
        [Obsolete("请使用Where(expression).ToListAsync()")]
        IQueryable<T> WhereAsync(FilterModel filterModel);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageRequestModel">分页请求模型</param>
        /// <returns></returns>
        Task<(List<T> result, PageModel pageModel)> PagingAsync(PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageRequestModel">分页请求模型</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <returns></returns>
        Task<(List<T> result, PageModel pageModel)> PagingAsync(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageRequestModel">分页请求模型</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <returns></returns>
        Task<(List<T> result, PageModel pageModel)> PagingAsync(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="pageRequestModel">分页请求模型</param>
        /// <returns></returns>
        Task<(List<T> result, PageModel pageModel)> PagingAsync(Expression<Func<T, bool>> filterExpression, PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="pagingIndex">页数</param>
        /// <param name="pagingSize">每页显示数量</param>
        /// <returns></returns>
        Task<(List<T> result, PageModel pageModel)> PagingAsync(Expression<Func<T, bool>> filterExpression, int pagingIndex, int pagingSize);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="pageRequestModel">分页请求模型</param>
        /// <returns></returns>
        Task<(List<T> result, PageModel pageModel)> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <param name="pageRequestModel">分页请求模型</param>
        /// <returns></returns>
        Task<(List<T> result, PageModel pageModel)> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="pagingIndex">页数</param>
        /// <param name="pagingSize">每页显示数量</param>
        /// <returns></returns>
        Task<(List<T> result, PageModel pageModel)> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, int pagingIndex, int pagingSize);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <param name="pagingIndex">页数</param>
        /// <param name="pagingSize">每页显示数量</param>
        /// <returns></returns>
        Task<(List<T> result, PageModel pageModel)> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, int pagingIndex, int pagingSize);
    }
}
