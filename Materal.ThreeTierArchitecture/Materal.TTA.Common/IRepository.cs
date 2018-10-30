using Materal.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Materal.TTA.Common
{
    public interface IRepository<T, in TPrimaryKeyType> where T : class
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
        /// 总数
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IQueryable<T> Where(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IAsyncEnumerable<T> WhereAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        List<T> Find(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<List<T>> FindAsync(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 检索
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Retrieve(TPrimaryKeyType id);
        /// <summary>
        /// 检索
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> RetrieveAsync(TPrimaryKeyType id);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="pageRequestModel">分页请求模型</param>
        /// <returns></returns>
        PageResultModel<T> Paging(Expression<Func<T, bool>> filterExpression, PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="skip">跳过数量</param>
        /// <param name="take">获取数量</param>
        /// <returns></returns>
        PageResultModel<T> Paging(Expression<Func<T, bool>> filterExpression, uint skip, uint take);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="pageRequestModel">分页请求模型</param>
        /// <returns></returns>
        PageResultModel<T> Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, bool>> orderExpression, PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <param name="pageRequestModel">分页请求模型</param>
        /// <returns></returns>
        PageResultModel<T> Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, bool>> orderExpression, SortOrder sortOrder, PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="skip">跳过数量</param>
        /// <param name="take">获取数量</param>
        /// <returns></returns>
        PageResultModel<T> Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, bool>> orderExpression, uint skip, uint take);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <param name="skip">跳过数量</param>
        /// <param name="take">获取数量</param>
        /// <returns></returns>
        PageResultModel<T> Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, bool>> orderExpression, SortOrder sortOrder, uint skip, uint take);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="pageRequestModel">分页请求模型</param>
        /// <returns></returns>
        Task<PageResultModel<T>> PagingAsync(Expression<Func<T, bool>> filterExpression, PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="skip">跳过数量</param>
        /// <param name="take">获取数量</param>
        /// <returns></returns>
        Task<PageResultModel<T>> PagingAsync(Expression<Func<T, bool>> filterExpression, uint skip, uint take);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="pageRequestModel">分页请求模型</param>
        /// <returns></returns>
        Task<PageResultModel<T>> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, bool>> orderExpression, PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <param name="pageRequestModel">分页请求模型</param>
        /// <returns></returns>
        Task<PageResultModel<T>> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, bool>> orderExpression, SortOrder sortOrder, PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="skip">跳过数量</param>
        /// <param name="take">获取数量</param>
        /// <returns></returns>
        Task<PageResultModel<T>> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, bool>> orderExpression, uint skip, uint take);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression">过滤表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <param name="skip">跳过数量</param>
        /// <param name="take">获取数量</param>
        /// <returns></returns>
        Task<PageResultModel<T>> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, bool>> orderExpression, SortOrder sortOrder, uint skip, uint take);
    }
}
