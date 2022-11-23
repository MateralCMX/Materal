using Materal.Model;
using Materal.TTA.Common;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace Materal.TTA.EFRepository
{
    public abstract class EFRepositoryImpl<T, TPrimaryKeyType> : IEFRepository<T, TPrimaryKeyType> 
        where T : class, IEntity<TPrimaryKeyType>
        where TPrimaryKeyType : struct
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        protected readonly DbContext DBContext;
        /// <summary>
        /// 实体对象
        /// </summary>
        protected IQueryable<T> DBSet => MateralTTAConfig.EnableTracking ? DBContext.Set<T>() : DBContext.Set<T>().AsNoTracking();
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbContext"></param>
        protected EFRepositoryImpl(DbContext dbContext) => DBContext = dbContext;
        public virtual bool Existed(TPrimaryKeyType id) => DBSet.Any(m => m.ID.Equals(id));
        public virtual async Task<bool> ExistedAsync(TPrimaryKeyType id) => await DBSet.AnyAsync(m => m.ID.Equals(id));
        public virtual bool Existed(Expression<Func<T, bool>> expression) => DBSet.Any(expression);
        public virtual async Task<bool> ExistedAsync(Expression<Func<T, bool>> expression) => await DBSet.AnyAsync(expression);
        public virtual bool Existed(FilterModel filterModel) => Existed(filterModel.GetSearchExpression<T>());
        public virtual async Task<bool> ExistedAsync(FilterModel filterModel) => await ExistedAsync(filterModel.GetSearchExpression<T>());
        public virtual int Count(Expression<Func<T, bool>> expression) => DBSet.Count(expression);
        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> expression) => await DBSet.CountAsync(expression);
        public virtual int Count(FilterModel filterModel) => Count(filterModel.GetSearchExpression<T>());
        public virtual async Task<int> CountAsync(FilterModel filterModel) => await CountAsync(filterModel.GetSearchExpression<T>());
        public virtual IQueryable<T> Where(Expression<Func<T, bool>> expression) => DBSet.Where(expression);
        public virtual IQueryable<T> Where(FilterModel filterModel) => Where(filterModel.GetSearchExpression<T>());
        public virtual List<T> Find(Expression<Func<T, bool>> expression) => Where(expression).ToList();
        public virtual List<T> Find(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression) => Find(expression, orderExpression, SortOrder.Ascending);
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
        public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> expression) => await Where(expression).ToListAsync();
        public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression) => await FindAsync(expression, orderExpression, SortOrder.Ascending);
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
        public virtual List<T> Find(FilterModel filterModel) => Find(filterModel.GetSearchExpression<T>());
        public virtual List<T> Find(FilterModel filterModel, Expression<Func<T, object>> orderExpression) => Find(filterModel.GetSearchExpression<T>(), orderExpression);
        public virtual List<T> Find(FilterModel filterModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder) => Find(filterModel.GetSearchExpression<T>(), orderExpression, sortOrder);
        public virtual async Task<List<T>> FindAsync(FilterModel filterModel) => await FindAsync(filterModel.GetSearchExpression<T>());
        public virtual async Task<List<T>> FindAsync(FilterModel filterModel, Expression<Func<T, object>> orderExpression) => await FindAsync(filterModel.GetSearchExpression<T>(), orderExpression);
        public virtual async Task<List<T>> FindAsync(FilterModel filterModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder) => await FindAsync(filterModel.GetSearchExpression<T>(), orderExpression, sortOrder);
        public virtual T? FirstOrDefault(TPrimaryKeyType id) => DBSet.FirstOrDefault(m => m.ID.Equals(id));
        public virtual async Task<T?> FirstOrDefaultAsync(TPrimaryKeyType id) => await DBSet.FirstOrDefaultAsync(m => m.ID.Equals(id));
        public virtual T? FirstOrDefault(FilterModel filterModel) => FirstOrDefault(filterModel.GetSearchExpression<T>());
        public virtual async Task<T?> FirstOrDefaultAsync(FilterModel filterModel) => await FirstOrDefaultAsync(filterModel.GetSearchExpression<T>());
        public virtual T? FirstOrDefault(Expression<Func<T, bool>> expression) => DBSet.FirstOrDefault(expression);
        public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression) => await DBSet.FirstOrDefaultAsync(expression);
        public virtual (List<T> result, PageModel pageModel) Paging(PageRequestModel pageRequestModel) => Paging(pageRequestModel.GetSearchExpression<T>(), pageRequestModel);
        public virtual (List<T> result, PageModel pageModel) Paging(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression) => Paging(pageRequestModel.GetSearchExpression<T>(), orderExpression, pageRequestModel);
        public virtual (List<T> result, PageModel pageModel) Paging(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder) => Paging(pageRequestModel.GetSearchExpression<T>(), orderExpression, sortOrder, pageRequestModel);
        public virtual (List<T> result, PageModel pageModel) Paging(Expression<Func<T, bool>> filterExpression, PageRequestModel pageRequestModel) => Paging(filterExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        public virtual (List<T> result, PageModel pageModel) Paging(Expression<Func<T, bool>> filterExpression, int pagingIndex, int pagingSize) => Paging(filterExpression, m => m.ID, pagingIndex, pagingSize);
        public virtual (List<T> result, PageModel pageModel) Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, PageRequestModel pageRequestModel) => Paging(filterExpression, orderExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        public virtual (List<T> result, PageModel pageModel) Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, PageRequestModel pageRequestModel) => Paging(filterExpression, orderExpression, sortOrder, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        public virtual (List<T> result, PageModel pageModel) Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, int pagingIndex, int pagingSize) => Paging(filterExpression, orderExpression, SortOrder.Ascending, pagingIndex, pagingSize);
        public virtual (List<T> result, PageModel pageModel) Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, int pagingIndex, int pagingSize)
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
        public virtual async Task<(List<T> result, PageModel pageModel)> PagingAsync(PageRequestModel pageRequestModel) => await PagingAsync(pageRequestModel.GetSearchExpression<T>(), pageRequestModel);
        public virtual async Task<(List<T> result, PageModel pageModel)> PagingAsync(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression) => await PagingAsync(pageRequestModel.GetSearchExpression<T>(), orderExpression, pageRequestModel);
        public virtual async Task<(List<T> result, PageModel pageModel)> PagingAsync(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder) => await PagingAsync(pageRequestModel.GetSearchExpression<T>(), orderExpression, sortOrder, pageRequestModel);
        public virtual async Task<(List<T> result, PageModel pageModel)> PagingAsync(Expression<Func<T, bool>> filterExpression, PageRequestModel pageRequestModel) => await PagingAsync(filterExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        public virtual async Task<(List<T> result, PageModel pageModel)> PagingAsync(Expression<Func<T, bool>> filterExpression, int pagingIndex, int pagingSize) => await PagingAsync(filterExpression, m => m.ID, pagingIndex, pagingSize);
        public virtual async Task<(List<T> result, PageModel pageModel)> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, PageRequestModel pageRequestModel) => await PagingAsync(filterExpression, orderExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        public virtual async Task<(List<T> result, PageModel pageModel)> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, PageRequestModel pageRequestModel) => await PagingAsync(filterExpression, orderExpression, sortOrder, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        public virtual async Task<(List<T> result, PageModel pageModel)> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, int pagingIndex, int pagingSize) => await PagingAsync(filterExpression, orderExpression, SortOrder.Ascending, pagingIndex, pagingSize);
        public virtual async Task<(List<T> result, PageModel pageModel)> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, int pagingIndex, int pagingSize)
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
    }
}