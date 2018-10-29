using Materal.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.Common
{
    public abstract class EntityFrameworkRepositoryImpl<T, TPrimaryKeyType> : IEntityFrameworkRepository<T, TPrimaryKeyType>
    {
        protected readonly DbContext dbContext;
        public bool Existed(TPrimaryKeyType id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistedAsync(TPrimaryKeyType id)
        {
            throw new NotImplementedException();
        }

        public int Count(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<T> WhereAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public List<T> Find(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public T Retrieve(TPrimaryKeyType id)
        {
            throw new NotImplementedException();
        }

        public async Task<T> RetrieveAsync(TPrimaryKeyType id)
        {
            throw new NotImplementedException();
        }

        public PageResultModel<T> Paging(Expression<Func<T, bool>> filterExpression, PageRequestModel pageRequestModel)
        {
            throw new NotImplementedException();
        }

        public PageResultModel<T> Paging(Expression<Func<T, bool>> filterExpression, uint skip, uint take)
        {
            throw new NotImplementedException();
        }

        public PageResultModel<T> Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, bool>> orderExpression, PageRequestModel pageRequestModel)
        {
            throw new NotImplementedException();
        }

        public PageResultModel<T> Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, bool>> orderExpression, SortOrder sortOrder,
            PageRequestModel pageRequestModel)
        {
            throw new NotImplementedException();
        }

        public PageResultModel<T> Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, bool>> orderExpression, uint skip, uint take)
        {
            throw new NotImplementedException();
        }

        public PageResultModel<T> Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, bool>> orderExpression, SortOrder sortOrder, uint skip,
            uint take)
        {
            throw new NotImplementedException();
        }

        public async Task<PageResultModel<T>> PagingAsync(Expression<Func<T, bool>> filterExpression, PageRequestModel pageRequestModel)
        {
            throw new NotImplementedException();
        }

        public async Task<PageResultModel<T>> PagingAsync(Expression<Func<T, bool>> filterExpression, uint skip, uint take)
        {
            throw new NotImplementedException();
        }

        public async Task<PageResultModel<T>> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, bool>> orderExpression, PageRequestModel pageRequestModel)
        {
            throw new NotImplementedException();
        }

        public async Task<PageResultModel<T>> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, bool>> orderExpression, SortOrder sortOrder,
            PageRequestModel pageRequestModel)
        {
            throw new NotImplementedException();
        }

        public async Task<PageResultModel<T>> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, bool>> orderExpression, uint skip, uint take)
        {
            throw new NotImplementedException();
        }

        public async Task<PageResultModel<T>> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, bool>> orderExpression, SortOrder sortOrder, uint skip, uint take)
        {
            throw new NotImplementedException();
        }
    }
}
