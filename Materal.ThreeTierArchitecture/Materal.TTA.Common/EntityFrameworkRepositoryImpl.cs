using Materal.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Materal.TTA.Common
{
    public class EntityFrameworkRepositoryImpl<T1, TPrimaryKeyType> : IEntityFrameworkRepository<T1, TPrimaryKeyType>
    {
        public bool Existed(TPrimaryKeyType id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistedAsync(TPrimaryKeyType id)
        {
            throw new NotImplementedException();
        }

        public int Count(Expression<Func<T1, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CountAsync(Expression<Func<T1, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T1> Where(Expression<Func<T1, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<T1> WhereAsync(Expression<Func<T1, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public List<T1> Find(Expression<Func<T1, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T1>> FindAsync(Expression<Func<T1, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public T1 Retrieve(TPrimaryKeyType id)
        {
            throw new NotImplementedException();
        }

        public async Task<T1> RetrieveAsync(TPrimaryKeyType id)
        {
            throw new NotImplementedException();
        }

        public PageResultModel<T1> Paging(Expression<Func<T1, bool>> filterExpression, PageRequestModel pageRequestModel)
        {
            throw new NotImplementedException();
        }

        public PageResultModel<T1> Paging(Expression<Func<T1, bool>> filterExpression, uint skip, uint take)
        {
            throw new NotImplementedException();
        }

        public PageResultModel<T1> Paging(Expression<Func<T1, bool>> filterExpression, Expression<Func<T1, bool>> orderExpression, PageRequestModel pageRequestModel)
        {
            throw new NotImplementedException();
        }

        public PageResultModel<T1> Paging(Expression<Func<T1, bool>> filterExpression, Expression<Func<T1, bool>> orderExpression, SortOrder sortOrder,
            PageRequestModel pageRequestModel)
        {
            throw new NotImplementedException();
        }

        public PageResultModel<T1> Paging(Expression<Func<T1, bool>> filterExpression, Expression<Func<T1, bool>> orderExpression, uint skip, uint take)
        {
            throw new NotImplementedException();
        }

        public PageResultModel<T1> Paging(Expression<Func<T1, bool>> filterExpression, Expression<Func<T1, bool>> orderExpression, SortOrder sortOrder, uint skip,
            uint take)
        {
            throw new NotImplementedException();
        }

        public async Task<PageResultModel<T1>> PagingAsync(Expression<Func<T1, bool>> filterExpression, PageRequestModel pageRequestModel)
        {
            throw new NotImplementedException();
        }

        public async Task<PageResultModel<T1>> PagingAsync(Expression<Func<T1, bool>> filterExpression, uint skip, uint take)
        {
            throw new NotImplementedException();
        }

        public async Task<PageResultModel<T1>> PagingAsync(Expression<Func<T1, bool>> filterExpression, Expression<Func<T1, bool>> orderExpression, PageRequestModel pageRequestModel)
        {
            throw new NotImplementedException();
        }

        public async Task<PageResultModel<T1>> PagingAsync(Expression<Func<T1, bool>> filterExpression, Expression<Func<T1, bool>> orderExpression, SortOrder sortOrder,
            PageRequestModel pageRequestModel)
        {
            throw new NotImplementedException();
        }

        public async Task<PageResultModel<T1>> PagingAsync(Expression<Func<T1, bool>> filterExpression, Expression<Func<T1, bool>> orderExpression, uint skip, uint take)
        {
            throw new NotImplementedException();
        }

        public async Task<PageResultModel<T1>> PagingAsync(Expression<Func<T1, bool>> filterExpression, Expression<Func<T1, bool>> orderExpression, SortOrder sortOrder, uint skip, uint take)
        {
            throw new NotImplementedException();
        }
    }
}
