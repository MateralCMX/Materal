using Materal.Model;
using Materal.TTA.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Materal.TTA.EFRepository
{
    public interface IEFSubordinateRepository<T, in TPrimaryKeyType> : IEFRepository<T, TPrimaryKeyType> where T : class, IEntity<TPrimaryKeyType>
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
        /// <param name="orderExpression">排序表达式</param>
        /// <returns></returns>
        List<T> FindFromSubordinate(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <returns></returns>
        List<T> FindFromSubordinate(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder);
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
        Task<List<T>> FindFromSubordinateAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <returns></returns>
        Task<List<T>> FindFromSubordinateAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder);
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
        /// <param name="orderExpression">排序表达式</param>
        /// <returns></returns>
        List<T> FindFromSubordinate(FilterModel filterModel, Expression<Func<T, object>> orderExpression);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <returns></returns>
        List<T> FindFromSubordinate(FilterModel filterModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder);
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
        Task<List<T>> FindFromSubordinateAsync(FilterModel filterModel, Expression<Func<T, object>> orderExpression);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <returns></returns>
        Task<List<T>> FindFromSubordinateAsync(FilterModel filterModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder);
        /// <summary>
        /// 检索
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T FirstOrDefaultFromSubordinate(TPrimaryKeyType id);
        /// <summary>
        /// 检索
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> FirstOrDefaultFromSubordinateAsync(TPrimaryKeyType id);
        /// <summary>
        /// 检索
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <returns></returns>
        T FirstOrDefaultFromSubordinate(FilterModel filterModel);
        /// <summary>
        /// 检索
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <returns></returns>
        Task<T> FirstOrDefaultFromSubordinateAsync(FilterModel filterModel);
        /// <summary>
        /// 检索
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        T FirstOrDefaultFromSubordinate(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 检索
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<T> FirstOrDefaultFromSubordinateAsync(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageRequestModel">分页请求模型</param>
        /// <returns></returns>
        (List<T> result, PageModel pageModel) PagingFromSubordinate(PageRequestModel pageRequestModel);

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageRequestModel">分页请求模型</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <returns></returns>
        (List<T> result, PageModel pageModel) PagingFromSubordinate(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageRequestModel">分页请求模型</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <returns></returns>
        (List<T> result, PageModel pageModel) PagingFromSubordinate(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="pageRequestModel">分页请求模型</param>
        /// <returns></returns>
        (List<T> result, PageModel pageModel) PagingFromSubordinate(Expression<Func<T, bool>> filterExpression, PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="pagingIndex">页数</param>
        /// <param name="pagingSize">每页显示数量</param>
        /// <returns></returns>
        (List<T> result, PageModel pageModel) PagingFromSubordinate(Expression<Func<T, bool>> filterExpression, int pagingIndex, int pagingSize);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="pageRequestModel">分页请求模型</param>
        /// <returns></returns>
        (List<T> result, PageModel pageModel) PagingFromSubordinate(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <param name="pageRequestModel">分页请求模型</param>
        /// <returns></returns>
        (List<T> result, PageModel pageModel) PagingFromSubordinate(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, PageRequestModel pageRequestModel);

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="pagingIndex">页数</param>
        /// <param name="pagingSize">每页显示数量</param>
        /// <returns></returns>
        (List<T> result, PageModel pageModel) PagingFromSubordinate(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, int pagingIndex, int pagingSize);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <param name="pagingIndex">页数</param>
        /// <param name="pagingSize">每页显示数量</param>
        /// <returns></returns>
        (List<T> result, PageModel pageModel) PagingFromSubordinate(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, int pagingIndex, int pagingSize);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageRequestModel">分页请求模型</param>
        /// <returns></returns>
        Task<(List<T> result, PageModel pageModel)> PagingFromSubordinateAsync(PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageRequestModel">分页请求模型</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <returns></returns>
        Task<(List<T> result, PageModel pageModel)> PagingFromSubordinateAsync(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageRequestModel">分页请求模型</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <returns></returns>
        Task<(List<T> result, PageModel pageModel)> PagingFromSubordinateAsync(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="pageRequestModel">分页请求模型</param>
        /// <returns></returns>
        Task<(List<T> result, PageModel pageModel)> PagingFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="pagingIndex">页数</param>
        /// <param name="pagingSize">每页显示数量</param>
        /// <returns></returns>
        Task<(List<T> result, PageModel pageModel)> PagingFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, int pagingIndex, int pagingSize);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="pageRequestModel">分页请求模型</param>
        /// <returns></returns>
        Task<(List<T> result, PageModel pageModel)> PagingFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <param name="pageRequestModel">分页请求模型</param>
        /// <returns></returns>
        Task<(List<T> result, PageModel pageModel)> PagingFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="pagingIndex">页数</param>
        /// <param name="pagingSize">每页显示数量</param>
        /// <returns></returns>
        Task<(List<T> result, PageModel pageModel)> PagingFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, int pagingIndex, int pagingSize);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <param name="pagingIndex">页数</param>
        /// <param name="pagingSize">每页显示数量</param>
        /// <returns></returns>
        Task<(List<T> result, PageModel pageModel)> PagingFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, int pagingIndex, int pagingSize);
    }
}
