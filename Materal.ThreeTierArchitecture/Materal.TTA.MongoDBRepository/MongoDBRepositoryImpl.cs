using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Materal.Common;
using Materal.TTA.Common;

namespace Materal.TTA.MongoDBRepository
{
    public class MongoDBRepositoryImpl<T, TPrimaryKeyType> : IMongoDBRepository<T, TPrimaryKeyType> where T : class, IEntity<TPrimaryKeyType>, new()
    {
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

        public T FirstOrDefault(TPrimaryKeyType id)
        {
            throw new NotImplementedException();
        }

        public async Task<T> FirstOrDefaultAsync(TPrimaryKeyType id)
        {
            throw new NotImplementedException();
        }

        public T FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public PageResultModel<T> Paging(Expression<Func<T, bool>> filterExpression, PageRequestModel pageRequestModel)
        {
            throw new NotImplementedException();
        }

        public PageResultModel<T> Paging(Expression<Func<T, bool>> filterExpression, int pagingIndex, int pagingSize)
        {
            throw new NotImplementedException();
        }

        public PageResultModel<T> Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, PageRequestModel pageRequestModel)
        {
            throw new NotImplementedException();
        }

        public PageResultModel<T> Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder,
            PageRequestModel pageRequestModel)
        {
            throw new NotImplementedException();
        }

        public PageResultModel<T> Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, int pagingIndex, int pagingSize)
        {
            throw new NotImplementedException();
        }

        public PageResultModel<T> Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, int pagingIndex, int pagingSize)
        {
            throw new NotImplementedException();
        }

        public async Task<PageResultModel<T>> PagingAsync(Expression<Func<T, bool>> filterExpression, PageRequestModel pageRequestModel)
        {
            throw new NotImplementedException();
        }

        public async Task<PageResultModel<T>> PagingAsync(Expression<Func<T, bool>> filterExpression, int pagingIndex, int pagingSize)
        {
            throw new NotImplementedException();
        }

        public async Task<PageResultModel<T>> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, PageRequestModel pageRequestModel)
        {
            throw new NotImplementedException();
        }

        public async Task<PageResultModel<T>> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder,
            PageRequestModel pageRequestModel)
        {
            throw new NotImplementedException();
        }

        public async Task<PageResultModel<T>> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, int pagingIndex, int pagingSize)
        {
            throw new NotImplementedException();
        }

        public async Task<PageResultModel<T>> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, int pagingIndex, int pagingSize)
        {
            throw new NotImplementedException();
        }
    }
}
