using Materal.Common;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Materal.TTA.MongoDBRepository
{
    public class MongoDBRepositoryImpl<T, TIdentifier> : IMongoDBRepository<T, TIdentifier> where T : MongoEntity<TIdentifier>, new()
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<T> _collection;

        protected MongoDBRepositoryImpl(string connectionString, string dataBaseNameString)
        {
            _mongoDatabase = new MongoClient(connectionString).GetDatabase(dataBaseNameString);
            _collection = _mongoDatabase.GetCollection<T>(typeof(T).Name);
        }

        public int Count(Expression<Func<T, bool>> expression)
        {
            long count = CountLong(expression);
            return count > int.MaxValue ? int.MaxValue : Convert.ToInt32(count);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            long count = await CountLongAsync(expression);
            return count > int.MaxValue ? int.MaxValue : Convert.ToInt32(count);
        }
        public long CountLong(Expression<Func<T, bool>> expression)
        {
            return _collection.CountDocuments(expression);
        }

        public async Task<long> CountLongAsync(Expression<Func<T, bool>> expression)
        {
            return await _collection.CountDocumentsAsync(expression);
        }

        public async Task InsertAsync(T model)
        {
            await _collection.InsertOneAsync(model);
        }

        public void Insert(T model)
        {
            _collection.InsertOne(model);
        }

        public async Task DeleteAsync(TIdentifier id)
        {
            await _collection.DeleteOneAsync(m => m.ID.Equals(id));
        }

        public async Task<long> DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            return (await _collection.DeleteOneAsync(predicate)).DeletedCount;
        }
        public long Delete(Expression<Func<T, bool>> predicate)
        {
            return _collection.DeleteOne(predicate).DeletedCount;
        }

        public async Task SaveAsync(T model)
        {
            await _collection.ReplaceOneAsync(x => x.ID.Equals(model.ID), model, new UpdateOptions
            {
                IsUpsert = true
            });
        }

        public void Save(T model)
        {
            _collection.ReplaceOne(x => x.ID.Equals(model.ID), model, new UpdateOptions
            {
                IsUpsert = true
            });
        }

        public async Task<List<T>> FindAsync(FilterDefinition<T> filterDefinition)
        {
            return await _collection.Find(filterDefinition).ToListAsync();
        }

        public async Task<List<BsonDocument>> FindAsync(FilterDefinition<BsonDocument> filterDefinition)
        {
            return await _mongoDatabase.GetCollection<BsonDocument>(typeof(T).Name).Find(filterDefinition).ToListAsync();
        }

        public async Task InsertManyAsync(IEnumerable<T> model)
        {
            await _collection.InsertManyAsync(model);
        }

        public void InsertMany(IEnumerable<T> model)
        {
            _collection.InsertMany(model);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return Find(expression).AsQueryable();
        }

        public IAsyncEnumerable<T> WhereAsync(Expression<Func<T, bool>> expression)
        {
            return Find(expression).ToAsyncEnumerable();
        }

        public bool Existed(TIdentifier id)
        {
            return CountLong(m => m.ID.Equals(id)) > 0;
        }

        public async Task<bool> ExistedAsync(TIdentifier id)
        {
            return await CountLongAsync(m => m.ID.Equals(id)) > 0;
        }

        public List<T> Find(Expression<Func<T, bool>> expression)
        {
            return _collection.Find(expression).ToList();
        }

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return (await _collection.FindAsync(expression)).ToList();
        }

        public T FirstOrDefault(TIdentifier id)
        {
            return _collection.Find(m => m.ID.Equals(id)).FirstOrDefault();
        }

        public T FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return _collection.Find(expression).FirstOrDefault();
        }

        public async Task<T> FirstOrDefaultAsync(TIdentifier id)
        {
            return (await _collection.FindAsync(m => m.ID.Equals(id))).FirstOrDefault();
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return (await _collection.FindAsync(expression)).FirstOrDefault();
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
