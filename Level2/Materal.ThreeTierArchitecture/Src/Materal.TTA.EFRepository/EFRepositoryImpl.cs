using Materal.TTA.Common;
using Materal.Utils.Model;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace Materal.TTA.EFRepository
{
    /// <summary>
    /// EF仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    /// <typeparam name="TDBContext"></typeparam>
    public abstract class EFRepositoryImpl<T, TPrimaryKeyType, TDBContext> : IEFRepository<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>
        where TPrimaryKeyType : struct
        where TDBContext : DbContext
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        protected TDBContext DBContext { get; private set; }
        /// <summary>
        /// 实体对象
        /// </summary>
        protected virtual DbSet<T> DBSet => DBContext.Set<T>();
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbContext"></param>
        protected EFRepositoryImpl(TDBContext dbContext)
        {
            DBContext = dbContext;
        }
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Existed(TPrimaryKeyType id) => DBSet.Any(m => m.ID.Equals(id));
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<bool> ExistedAsync(TPrimaryKeyType id) => await DBSet.AnyAsync(m => m.ID.Equals(id));
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual bool Existed(Expression<Func<T, bool>> expression) => DBSet.Any(expression);
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual async Task<bool> ExistedAsync(Expression<Func<T, bool>> expression) => await DBSet.AnyAsync(expression);
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual bool Existed(FilterModel filterModel) => Existed(filterModel.GetSearchExpression<T>());
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual async Task<bool> ExistedAsync(FilterModel filterModel) => await ExistedAsync(filterModel.GetSearchExpression<T>());
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual int Count(Expression<Func<T, bool>> expression) => DBSet.Count(expression);
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> expression) => await DBSet.CountAsync(expression);
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual int Count(FilterModel filterModel) => Count(filterModel.GetSearchExpression<T>());
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual async Task<int> CountAsync(FilterModel filterModel) => await CountAsync(filterModel.GetSearchExpression<T>());
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual List<T> Find(Expression<Func<T, bool>> expression) => Where(expression).ToList();
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderExpression"></param>
        /// <returns></returns>
        public virtual List<T> Find(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression) => Find(expression, orderExpression, SortOrder.Ascending);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual List<T> Find(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder)
        {
            IQueryable<T> queryable = Where(expression);
            List<T> result = sortOrder switch
            {
                SortOrder.Ascending => queryable.OrderBy(orderExpression).ToList(),
                SortOrder.Descending => queryable.OrderByDescending(orderExpression).ToList(),
                _ => queryable.ToList(),
            };
            return result;
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> expression) => await Where(expression).ToListAsync();
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderExpression"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression) => await FindAsync(expression, orderExpression, SortOrder.Ascending);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder)
        {
            IQueryable<T> queryable = Where(expression);
            List<T> result = sortOrder switch
            {
                SortOrder.Ascending => await queryable.OrderBy(orderExpression).ToListAsync(),
                SortOrder.Descending => await queryable.OrderByDescending(orderExpression).ToListAsync(),
                _ => await queryable.ToListAsync(),
            };
            return result;
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual List<T> Find(FilterModel filterModel) => Find(filterModel.GetSearchExpression<T>());
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="orderExpression"></param>
        /// <returns></returns>
        public virtual List<T> Find(FilterModel filterModel, Expression<Func<T, object>> orderExpression) => Find(filterModel.GetSearchExpression<T>(), orderExpression);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual List<T> Find(FilterModel filterModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder) => Find(filterModel.GetSearchExpression<T>(), orderExpression, sortOrder);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> FindAsync(FilterModel filterModel) => await FindAsync(filterModel.GetSearchExpression<T>());
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="orderExpression"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> FindAsync(FilterModel filterModel, Expression<Func<T, object>> orderExpression) => await FindAsync(filterModel.GetSearchExpression<T>(), orderExpression);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> FindAsync(FilterModel filterModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder) => await FindAsync(filterModel.GetSearchExpression<T>(), orderExpression, sortOrder);
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        public T First(FilterModel filterModel) => FirstOrDefault(filterModel) ?? throw new TTAException("数据不存在");
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        public async Task<T> FirstAsync(FilterModel filterModel) => await FirstOrDefaultAsync(filterModel) ?? throw new TTAException("数据不存在");
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        public T First(Expression<Func<T, bool>> expression) => FirstOrDefault(expression) ?? throw new TTAException("数据不存在");
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        public async Task<T> FirstAsync(Expression<Func<T, bool>> expression) => await FirstOrDefaultAsync(expression) ?? throw new TTAException("数据不存在");
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T? FirstOrDefault(TPrimaryKeyType id) => DBSet.FirstOrDefault(m => m.ID.Equals(id));
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T?> FirstOrDefaultAsync(TPrimaryKeyType id) => await DBSet.FirstOrDefaultAsync(m => m.ID.Equals(id));
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual T? FirstOrDefault(FilterModel filterModel) => FirstOrDefault(filterModel.GetSearchExpression<T>());
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual async Task<T?> FirstOrDefaultAsync(FilterModel filterModel) => await FirstOrDefaultAsync(filterModel.GetSearchExpression<T>());
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual T? FirstOrDefault(Expression<Func<T, bool>> expression) => DBSet.FirstOrDefault(expression);
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression) => await DBSet.FirstOrDefaultAsync(expression);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(PageRequestModel pageRequestModel) => Paging(pageRequestModel.GetSearchExpression<T>(), pageRequestModel);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequestModel"></param>
        /// <param name="orderExpression"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression) => Paging(pageRequestModel.GetSearchExpression<T>(), orderExpression, pageRequestModel);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequestModel"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder) => Paging(pageRequestModel.GetSearchExpression<T>(), orderExpression, sortOrder, pageRequestModel);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(Expression<Func<T, bool>> filterExpression, PageRequestModel pageRequestModel) => Paging(filterExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="pagingIndex"></param>
        /// <param name="pagingSize"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(Expression<Func<T, bool>> filterExpression, int pagingIndex, int pagingSize) => Paging(filterExpression, m => m.ID, pagingIndex, pagingSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, PageRequestModel pageRequestModel) => Paging(filterExpression, orderExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, PageRequestModel pageRequestModel) => Paging(filterExpression, orderExpression, sortOrder, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="pagingIndex"></param>
        /// <param name="pagingSize"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, int pagingIndex, int pagingSize) => Paging(filterExpression, orderExpression, SortOrder.Ascending, pagingIndex, pagingSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pagingIndex"></param>
        /// <param name="pagingSize"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, int pagingIndex, int pagingSize)
        {
            IQueryable<T> queryable = Where(filterExpression);
            var pageModel = new PageModel(pagingIndex, pagingSize, queryable.Count());
            List<T> result = sortOrder switch
            {
                SortOrder.Ascending => queryable.OrderBy(orderExpression).Skip(pageModel.Skip).Take(pageModel.Take).ToList(),
                SortOrder.Descending => queryable.OrderByDescending(orderExpression).Skip(pageModel.Skip).Take(pageModel.Take).ToList(),
                _ => queryable.Skip(pageModel.Skip).Take(pageModel.Take).ToList(),
            };
            return (result, pageModel);
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(PageRequestModel pageRequestModel) => await PagingAsync(pageRequestModel.GetSearchExpression<T>(), pageRequestModel);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequestModel"></param>
        /// <param name="orderExpression"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression) => await PagingAsync(pageRequestModel.GetSearchExpression<T>(), orderExpression, pageRequestModel);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequestModel"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder) => await PagingAsync(pageRequestModel.GetSearchExpression<T>(), orderExpression, sortOrder, pageRequestModel);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(Expression<Func<T, bool>> filterExpression, PageRequestModel pageRequestModel) => await PagingAsync(filterExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="pagingIndex"></param>
        /// <param name="pagingSize"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(Expression<Func<T, bool>> filterExpression, int pagingIndex, int pagingSize) => await PagingAsync(filterExpression, m => m.ID, pagingIndex, pagingSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, PageRequestModel pageRequestModel) => await PagingAsync(filterExpression, orderExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, PageRequestModel pageRequestModel) => await PagingAsync(filterExpression, orderExpression, sortOrder, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="pagingIndex"></param>
        /// <param name="pagingSize"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, int pagingIndex, int pagingSize) => await PagingAsync(filterExpression, orderExpression, SortOrder.Ascending, pagingIndex, pagingSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pagingIndex"></param>
        /// <param name="pagingSize"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, int pagingIndex, int pagingSize)
        {
            IQueryable<T> queryable = Where(filterExpression);
            var pageModel = new PageModel(pagingIndex, pagingSize, queryable.Count());
            List<T> result = sortOrder switch
            {
                SortOrder.Ascending => await queryable.OrderBy(orderExpression).Skip(pageModel.Skip).Take(pageModel.Take).ToListAsync(),
                SortOrder.Descending => await queryable.OrderByDescending(orderExpression).Skip(pageModel.Skip).Take(pageModel.Take).ToListAsync(),
                _ => await queryable.Skip(pageModel.Skip).Take(pageModel.Take).ToListAsync(),
            };
            return (result, pageModel);
        }
        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual IQueryable<T> Where(Expression<Func<T, bool>> expression) => DBSet.Where(expression);
        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual IQueryable<T> Where(FilterModel filterModel) => Where(filterModel.GetSearchExpression<T>());
    }
}