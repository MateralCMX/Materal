using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Services.Models;
using Materal.Utils.Model;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace Materal.BusinessFlow.Abstractions.Repositories
{
    public interface IBaseRepository
    {
    }
    public interface IBaseRepository<T> : IBaseRepository
        where T : class, IBaseDomain
    {
        /// <summary>
        /// 初始化数据库
        /// </summary>
        void InitDB();
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Existing(Guid id);
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> ExistingAsync(Guid id);
        /// <summary>
        /// 第一个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T First(Guid id);
        /// <summary>
        /// 第一个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> FirstAsync(Guid id);
        /// <summary>
        /// 第一个
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        T First(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 第一个
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<T> FirstAsync(Expression<Func<T,bool>> expression);
        /// <summary>
        /// 第一个或默认值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T? FirstOrDefault(Guid id);
        /// <summary>
        /// 第一个或默认值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T?> FirstOrDefaultAsync(Guid id);
        /// <summary>
        /// 第一个或默认值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        T? FirstOrDefault(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 第一个或默认值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        List<T> GetList(IQueryModel? queryModel);
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        Task<List<T>> GetListAsync(IQueryModel? queryModel);
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        List<T> GetList(Expression<Func<T, bool>> filterExpression);
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> filterExpression);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(IQueryModel? queryModel = null);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(IQueryModel? queryModel = null);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="queryModel"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(IQueryModel? queryModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder = SortOrder.Ascending);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="queryModel"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(IQueryModel? queryModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder = SortOrder.Ascending);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(Expression<Func<T, bool>> filterExpression, int pageIndex, int pageSize);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(Expression<Func<T, bool>> filterExpression, int pageIndex, int pageSize);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        (List<T> data, PageModel pageInfo) Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, int pageIndex, int pageSize);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<(List<T> data, PageModel pageInfo)> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, int pageIndex, int pageSize);
    }
}
