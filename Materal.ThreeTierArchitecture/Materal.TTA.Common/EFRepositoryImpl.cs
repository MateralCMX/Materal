using Materal.Common;
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
    public abstract class EFRepositoryImpl<T, TPrimaryKeyType> : IEFRepository<T, TPrimaryKeyType> where T : class, IEntity<TPrimaryKeyType>, new()
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
        protected IQueryable<T> DBQueryable => _isView ? (IQueryable<T>)DBContext.Query<T>() : DBContext.Set<T>();

        /// <summary>
        /// 视图标识
        /// </summary>
        private readonly bool _isView;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbContext"></param>
        protected EFRepositoryImpl(DbContext dbContext)
        {
            Type tType = typeof(T);
            var entityAttribute = tType.GetCustomAttribute<ViewEntityAttribute>(false);
            _isView = entityAttribute != null;
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

        public virtual int Count(Expression<Func<T, bool>> expression)
        {
            return DBQueryable.Count(expression);
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            return await DBQueryable.CountAsync(expression);
        }

        public virtual IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return DBQueryable.Where(expression);
        }

        public virtual IAsyncEnumerable<T> WhereAsync(Expression<Func<T, bool>> expression)
        {
            return DBQueryable.Where(expression).ToAsyncEnumerable();
        }

        public virtual List<T> Find(Expression<Func<T, bool>> expression)
        {
            return Where(expression).ToList();
        }

        public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await Where(expression).ToListAsync();
        }

        public virtual T FirstOrDefault(TPrimaryKeyType id)
        {
            return DBQueryable.FirstOrDefault(m => m.ID.Equals(id));
        }

        public virtual async Task<T> FirstOrDefaultAsync(TPrimaryKeyType id)
        {
            return await DBQueryable.FirstOrDefaultAsync(m => m.ID.Equals(id));
        }

        public T FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return DBQueryable.FirstOrDefault(expression);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await DBQueryable.FirstOrDefaultAsync(expression);
        }

        public virtual PageResultModel<T> Paging(Expression<Func<T, bool>> filterExpression, PageRequestModel pageRequestModel)
        {
            return Paging(filterExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        }

        public virtual PageResultModel<T> Paging(Expression<Func<T, bool>> filterExpression, int pagingIndex, int pagingSize)
        {
            return Paging(filterExpression, m => m.ID, pagingIndex, pagingSize);
        }

        public virtual PageResultModel<T> Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, PageRequestModel pageRequestModel)
        {
            return Paging(filterExpression, orderExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        }

        public virtual PageResultModel<T> Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, PageRequestModel pageRequestModel)
        {
            return Paging(filterExpression, orderExpression, sortOrder, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        }

        public virtual PageResultModel<T> Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, int pagingIndex, int pagingSize)
        {
            return Paging(filterExpression, orderExpression, SortOrder.Ascending, pagingIndex, pagingSize);
        }

        public virtual PageResultModel<T> Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, int pagingIndex, int pagingSize)
        {
            var result = new PageResultModel<T>();
            IQueryable<T> queryable = Where(filterExpression);
            result.PageModel = new PageModel(pagingIndex, pagingSize, queryable.Count());
            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    result.Data = queryable.OrderBy(orderExpression).Skip(result.PageModel.Skip).Take(result.PageModel.Take).ToList();
                    break;
                case SortOrder.Descending:
                    result.Data = queryable.OrderByDescending(orderExpression).Skip(result.PageModel.Skip).Take(result.PageModel.Take).ToList();
                    break;
                default:
                    result.Data = queryable.Skip(result.PageModel.Skip).Take(result.PageModel.Take).ToList();
                    break;
            }
            return result;
        }

        public virtual async Task<PageResultModel<T>> PagingAsync(Expression<Func<T, bool>> filterExpression, PageRequestModel pageRequestModel)
        {
            return await PagingAsync(filterExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        }

        public virtual async Task<PageResultModel<T>> PagingAsync(Expression<Func<T, bool>> filterExpression, int pagingIndex, int pagingSize)
        {
            return await PagingAsync(filterExpression, m => m.ID, pagingIndex, pagingSize);
        }

        public virtual async Task<PageResultModel<T>> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, PageRequestModel pageRequestModel)
        {
            return await PagingAsync(filterExpression, orderExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        }

        public virtual async Task<PageResultModel<T>> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, PageRequestModel pageRequestModel)
        {
            return await PagingAsync(filterExpression, orderExpression, sortOrder, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        }

        public virtual async Task<PageResultModel<T>> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, int pagingIndex, int pagingSize)
        {
            return await PagingAsync(filterExpression, orderExpression, SortOrder.Ascending, pagingIndex, pagingSize);
        }

        public virtual async Task<PageResultModel<T>> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, int pagingIndex, int pagingSize)
        {
            var result = new PageResultModel<T>();
            IQueryable<T> queryable = Where(filterExpression);
            result.PageModel = new PageModel(pagingIndex, pagingSize, queryable.Count());
            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    result.Data = await queryable.OrderBy(orderExpression).Skip(result.PageModel.Skip).Take(result.PageModel.Take).ToListAsync();
                    break;
                case SortOrder.Descending:
                    result.Data = await queryable.OrderByDescending(orderExpression).Skip(result.PageModel.Skip).Take(result.PageModel.Take).ToListAsync();
                    break;
                default:
                    result.Data = await queryable.Skip(result.PageModel.Skip).Take(result.PageModel.Take).ToListAsync();
                    break;
            }
            return result;
        }
    }
}
