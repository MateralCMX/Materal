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
    public abstract class EntityFrameworkRepositoryImpl<T, TPrimaryKeyType> : IEntityFrameworkRepository<T, TPrimaryKeyType> where T : class
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
        protected IQueryable<T> DBQueryable => _isView ? (IQueryable<T>) DBContext.Query<T>() : DBContext.Set<T>();

        /// <summary>
        /// 视图标识
        /// </summary>
        private readonly bool _isView;
        /// <summary>
        /// 主键名称
        /// </summary>
        private readonly string _primaryKeyName;
        /// <summary>
        /// 主键属性信息
        /// </summary>
        private readonly PropertyInfo _primaryKeyInfo;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbContext"></param>
        protected EntityFrameworkRepositoryImpl(DbContext dbContext)
        {
            Type tType = typeof(T);
            var entityAttribute = tType.GetCustomAttribute<EntityAttribute>(false);
            if (entityAttribute == null) throw new MateralException("该实体未标记为EntityAttribute");
            DBContext = dbContext;
            _isView = entityAttribute.IsView;
            _primaryKeyName = entityAttribute.PrimaryKeyName;
            _primaryKeyInfo = tType.GetProperty(_primaryKeyName);
        }
        public virtual bool Existed(TPrimaryKeyType id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<bool> ExistedAsync(TPrimaryKeyType id)
        {
            throw new NotImplementedException();
        }

        public virtual int Count(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public virtual IAsyncEnumerable<T> WhereAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public virtual List<T> Find(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public virtual T Retrieve(TPrimaryKeyType id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<T> RetrieveAsync(TPrimaryKeyType id)
        {
            throw new NotImplementedException();
        }

        public virtual PageResultModel<T> Paging(Expression<Func<T, bool>> filterExpression, PageRequestModel pageRequestModel)
        {
            throw new NotImplementedException();
        }

        public virtual PageResultModel<T> Paging(Expression<Func<T, bool>> filterExpression, uint skip, uint take)
        {
            throw new NotImplementedException();
        }

        public virtual PageResultModel<T> Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, bool>> orderExpression, PageRequestModel pageRequestModel)
        {
            throw new NotImplementedException();
        }

        public virtual PageResultModel<T> Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, bool>> orderExpression, SortOrder sortOrder,
            PageRequestModel pageRequestModel)
        {
            throw new NotImplementedException();
        }

        public virtual PageResultModel<T> Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, bool>> orderExpression, uint skip, uint take)
        {
            throw new NotImplementedException();
        }

        public virtual PageResultModel<T> Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, bool>> orderExpression, SortOrder sortOrder, uint skip,
            uint take)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<PageResultModel<T>> PagingAsync(Expression<Func<T, bool>> filterExpression, PageRequestModel pageRequestModel)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<PageResultModel<T>> PagingAsync(Expression<Func<T, bool>> filterExpression, uint skip, uint take)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<PageResultModel<T>> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, bool>> orderExpression, PageRequestModel pageRequestModel)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<PageResultModel<T>> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, bool>> orderExpression, SortOrder sortOrder,
            PageRequestModel pageRequestModel)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<PageResultModel<T>> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, bool>> orderExpression, uint skip, uint take)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<PageResultModel<T>> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, bool>> orderExpression, SortOrder sortOrder, uint skip, uint take)
        {
            throw new NotImplementedException();
        }
    }
}
