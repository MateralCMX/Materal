using Materal.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Materal.TTA.Common
{
    public abstract class EFRepositoryImpl<T, TPrimaryKeyType> : IEFRepository<T, TPrimaryKeyType> where T : class, IEntity<TPrimaryKeyType>
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        protected readonly DbContext DBContext;

        /// <summary>
        /// 实体对象
        /// </summary>
        protected DbSet<T> DBSet => DBContext.Set<T>();

        /// <summary>
        /// 视图对象
        /// </summary>
        protected DbQuery<T> DBQuery => DBContext.Query<T>();

        /// <summary>
        /// 数据库对象
        /// </summary>
        protected IQueryable<T> DBQueryable => IsView ? (IQueryable<T>)DBContext.Query<T>() : DBContext.Set<T>();

        /// <summary>
        /// 视图标识
        /// </summary>
        protected readonly bool IsView;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbContext"></param>
        protected EFRepositoryImpl(DbContext dbContext)
        {
            Type tType = typeof(T);
            var entityAttribute = tType.GetCustomAttribute<ViewEntityAttribute>(false);
            IsView = entityAttribute != null;
            DBContext = dbContext;
        }

        public virtual bool Existed(TPrimaryKeyType id)
        {
            return DBQueryable.Any(m => m.ID.Equals(id));
        }

        public virtual async Task<bool> ExistedAsync(TPrimaryKeyType id)
        {
            return await DBQueryable.AnyAsync(m => m.ID.Equals(id));
        }

        public virtual bool Existed(Expression<Func<T, bool>> expression)
        {
            return DBQueryable.Any(expression);
        }

        public virtual async Task<bool> ExistedAsync(Expression<Func<T, bool>> expression)
        {
            return await DBQueryable.AnyAsync(expression);
        }

        public virtual bool Existed(FilterModel filterModel)
        {
            return Existed(filterModel.GetSearchExpression<T>());
        }

        public virtual async Task<bool> ExistedAsync(FilterModel filterModel)
        {
            return await ExistedAsync(filterModel.GetSearchExpression<T>());
        }

        public virtual int Count(Expression<Func<T, bool>> expression)
        {
            return DBQueryable.Count(expression);
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            return await DBQueryable.CountAsync(expression);
        }

        public virtual int Count(FilterModel filterModel)
        {
            return Count(filterModel.GetSearchExpression<T>());
        }

        public virtual async Task<int> CountAsync(FilterModel filterModel)
        {
            return await CountAsync(filterModel.GetSearchExpression<T>());
        }

        public virtual IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return DBQueryable.Where(expression);
        }

        public virtual IAsyncEnumerable<T> WhereAsync(Expression<Func<T, bool>> expression)
        {
            return DBQueryable.Where(expression).ToAsyncEnumerable();
        }

        public virtual IQueryable<T> Where(FilterModel filterModel)
        {
            return Where(filterModel.GetSearchExpression<T>());
        }

        public virtual IAsyncEnumerable<T> WhereAsync(FilterModel filterModel)
        {
            return WhereAsync(filterModel.GetSearchExpression<T>());
        }

        public virtual List<T> Find(Expression<Func<T, bool>> expression)
        {
            return Where(expression).ToList();
        }

        public virtual List<T> Find(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression)
        {
            return Find(expression, orderExpression, SortOrder.Ascending);
        }

        public virtual List<T> Find(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder)
        {
            List<T> result;
            IQueryable<T> queryable = Where(expression);
            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    result = queryable.OrderBy(orderExpression).ToList();
                    break;
                case SortOrder.Descending:
                    result = queryable.OrderByDescending(orderExpression).ToList();
                    break;
                default:
                    result = queryable.ToList();
                    break;
            }
            return result;
        }

        public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await Where(expression).ToListAsync();
        }

        public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> expression, Func<T, object> orderExpression)
        {
            return await FindAsync(expression, orderExpression, SortOrder.Ascending);
        }

        public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> expression, Func<T, object> orderExpression, SortOrder sortOrder)
        {
            List<T> result;
            IAsyncEnumerable<T> queryable = WhereAsync(expression);
            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    result = await queryable.OrderBy(orderExpression).ToList();
                    break;
                case SortOrder.Descending:
                    result = await queryable.OrderByDescending(orderExpression).ToList();
                    break;
                default:
                    result = await queryable.ToList();
                    break;
            }
            return result;
        }

        public virtual List<T> Find(FilterModel filterModel)
        {
            return Find(filterModel.GetSearchExpression<T>());
        }

        public virtual List<T> Find(FilterModel filterModel, Expression<Func<T, object>> orderExpression)
        {
            return Find(filterModel.GetSearchExpression<T>(), orderExpression);
        }

        public virtual List<T> Find(FilterModel filterModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder)
        {
            return Find(filterModel.GetSearchExpression<T>(), orderExpression, sortOrder);
        }

        public virtual async Task<List<T>> FindAsync(FilterModel filterModel)
        {
            return await FindAsync(filterModel.GetSearchExpression<T>());
        }

        public virtual async Task<List<T>> FindAsync(FilterModel filterModel, Func<T, object> orderExpression)
        {
            return await FindAsync(filterModel.GetSearchExpression<T>(), orderExpression);
        }

        public virtual async Task<List<T>> FindAsync(FilterModel filterModel, Func<T, object> orderExpression, SortOrder sortOrder)
        {
            return await FindAsync(filterModel.GetSearchExpression<T>(), orderExpression, sortOrder);
        }

        public virtual T FirstOrDefault(TPrimaryKeyType id)
        {
            return DBQueryable.FirstOrDefault(m => m.ID.Equals(id));
        }

        public virtual async Task<T> FirstOrDefaultAsync(TPrimaryKeyType id)
        {
            return await DBQueryable.FirstOrDefaultAsync(m => m.ID.Equals(id));
        }

        public virtual T FirstOrDefault(FilterModel filterModel)
        {
            return FirstOrDefault(filterModel.GetSearchExpression<T>());
        }

        public virtual async Task<T> FirstOrDefaultAsync(FilterModel filterModel)
        {
            return await FirstOrDefaultAsync(filterModel.GetSearchExpression<T>());
        }

        public virtual T FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return DBQueryable.FirstOrDefault(expression);
        }

        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await DBQueryable.FirstOrDefaultAsync(expression);
        }

        public virtual (List<T> result, PageModel pageModel) Paging(PageRequestModel pageRequestModel)
        {
            return Paging(pageRequestModel.GetSearchExpression<T>(), pageRequestModel);
        }

        public virtual (List<T> result, PageModel pageModel) Paging(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression)
        {
            return Paging(pageRequestModel.GetSearchExpression<T>(), orderExpression, pageRequestModel);
        }

        public virtual (List<T> result, PageModel pageModel) Paging(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression,
            SortOrder sortOrder)
        {
            return Paging(pageRequestModel.GetSearchExpression<T>(), orderExpression, sortOrder, pageRequestModel);
        }

        public virtual (List<T> result, PageModel pageModel) Paging(Expression<Func<T, bool>> filterExpression, PageRequestModel pageRequestModel)
        {
            return Paging(filterExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        }

        public virtual (List<T> result, PageModel pageModel) Paging(Expression<Func<T, bool>> filterExpression, int pagingIndex, int pagingSize)
        {
            return Paging(filterExpression, m => m.ID, pagingIndex, pagingSize);
        }

        public virtual (List<T> result, PageModel pageModel) Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, PageRequestModel pageRequestModel)
        {
            return Paging(filterExpression, orderExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        }

        public virtual (List<T> result, PageModel pageModel) Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, PageRequestModel pageRequestModel)
        {
            return Paging(filterExpression, orderExpression, sortOrder, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        }

        public virtual (List<T> result, PageModel pageModel) Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, int pagingIndex, int pagingSize)
        {
            return Paging(filterExpression, orderExpression, SortOrder.Ascending, pagingIndex, pagingSize);
        }

        public virtual (List<T> result, PageModel pageModel) Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, int pagingIndex, int pagingSize)
        {
            List<T> result;
            IQueryable<T> queryable = Where(filterExpression);
            var pageModel = new PageModel(pagingIndex, pagingSize, queryable.Count());
            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    result = queryable.OrderBy(orderExpression).Skip(pageModel.Skip).Take(pageModel.Take).ToList();
                    break;
                case SortOrder.Descending:
                    result = queryable.OrderByDescending(orderExpression).Skip(pageModel.Skip).Take(pageModel.Take).ToList();
                    break;
                default:
                    result = queryable.Skip(pageModel.Skip).Take(pageModel.Take).ToList();
                    break;
            }
            return (result, pageModel);
        }

        public virtual async Task<(List<T> result, PageModel pageModel)> PagingAsync(PageRequestModel pageRequestModel)
        {
            return await PagingAsync(pageRequestModel.GetSearchExpression<T>(), pageRequestModel);
        }

        public virtual async Task<(List<T> result, PageModel pageModel)> PagingAsync(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression)
        {
            return await PagingAsync(pageRequestModel.GetSearchExpression<T>(), orderExpression, pageRequestModel);
        }

        public virtual async Task<(List<T> result, PageModel pageModel)> PagingAsync(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder)
        {
            return await PagingAsync(pageRequestModel.GetSearchExpression<T>(), orderExpression, sortOrder, pageRequestModel);
        }

        public virtual async Task<(List<T> result, PageModel pageModel)> PagingAsync(Expression<Func<T, bool>> filterExpression, PageRequestModel pageRequestModel)
        {
            return await PagingAsync(filterExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        }

        public virtual async Task<(List<T> result, PageModel pageModel)> PagingAsync(Expression<Func<T, bool>> filterExpression, int pagingIndex, int pagingSize)
        {
            return await PagingAsync(filterExpression, m => m.ID, pagingIndex, pagingSize);
        }

        public virtual async Task<(List<T> result, PageModel pageModel)> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, PageRequestModel pageRequestModel)
        {
            return await PagingAsync(filterExpression, orderExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        }

        public virtual async Task<(List<T> result, PageModel pageModel)> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, PageRequestModel pageRequestModel)
        {
            return await PagingAsync(filterExpression, orderExpression, sortOrder, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        }

        public virtual async Task<(List<T> result, PageModel pageModel)> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, int pagingIndex, int pagingSize)
        {
            return await PagingAsync(filterExpression, orderExpression, SortOrder.Ascending, pagingIndex, pagingSize);
        }

        public virtual async Task<(List<T> result, PageModel pageModel)> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, int pagingIndex, int pagingSize)
        {
            List<T> result;
            IQueryable<T> queryable = Where(filterExpression);
            var pageModel = new PageModel(pagingIndex, pagingSize, queryable.Count());
            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    result = await queryable.OrderBy(orderExpression).Skip(pageModel.Skip).Take(pageModel.Take).ToListAsync();
                    break;
                case SortOrder.Descending:
                    result = await queryable.OrderByDescending(orderExpression).Skip(pageModel.Skip).Take(pageModel.Take).ToListAsync();
                    break;
                default:
                    result = await queryable.Skip(pageModel.Skip).Take(pageModel.Take).ToListAsync();
                    break;
            }
            return (result, pageModel);
        }
    }
}
