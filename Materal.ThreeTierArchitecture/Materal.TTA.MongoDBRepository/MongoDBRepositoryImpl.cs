using Materal.Model;
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
        private IMongoCollection<T> _collection;
        private IMongoCollection<BsonDocument> _bsonCollection;

        protected MongoDBRepositoryImpl(string connectionString, string dataBaseNameString, string collectionNameString = null)
        {
            if (string.IsNullOrEmpty(collectionNameString)) collectionNameString = typeof(T).Name;
            _mongoDatabase = new MongoClient(connectionString).GetDatabase(dataBaseNameString);
            SelectCollection(collectionNameString);
        }

        public void SelectCollection(string collectionNameString)
        {
            _collection = _mongoDatabase.GetCollection<T>(collectionNameString);
            _bsonCollection = _mongoDatabase.GetCollection<BsonDocument>(collectionNameString);
        }

        public virtual async Task<bool> ExistedAsync(FilterModel filterModel)
        {
            return await ExistedAsync(filterModel.GetSearchExpression<T>());
        }

        public virtual int Count(Expression<Func<T, bool>> expression)
        {
            long count = CountLong(expression);
            return count > int.MaxValue ? int.MaxValue : Convert.ToInt32(count);
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            long count = await CountLongAsync(expression);
            return count > int.MaxValue ? int.MaxValue : Convert.ToInt32(count);
        }

        public virtual int Count(FilterModel filterModel)
        {
            return Count(filterModel.GetSearchExpression<T>());
        }

        public virtual async Task<int> CountAsync(FilterModel filterModel)
        {
            return await CountAsync(filterModel.GetSearchExpression<T>());
        }

        public virtual long CountLong(Expression<Func<T, bool>> expression)
        {
            return _collection.CountDocuments(expression);
        }

        public virtual async Task<long> CountLongAsync(Expression<Func<T, bool>> expression)
        {
            return await _collection.CountDocumentsAsync(expression);
        }

        public virtual async Task InsertAsync(T model)
        {
            await _collection.InsertOneAsync(model);
        }

        public virtual void Insert(T model)
        {
            _collection.InsertOne(model);
        }

        public virtual async Task DeleteAsync(TIdentifier id)
        {
            await _collection.DeleteOneAsync(m => m.ID.Equals(id));
        }

        public virtual async Task<long> DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            return (await _collection.DeleteOneAsync(predicate)).DeletedCount;
        }
        public virtual long Delete(Expression<Func<T, bool>> predicate)
        {
            return _collection.DeleteOne(predicate).DeletedCount;
        }
        public virtual async Task<long> DeleteManyAsync(Expression<Func<T, bool>> predicate)
        {
            return (await _collection.DeleteManyAsync(predicate)).DeletedCount;
        }
        public virtual long DeleteMany(Expression<Func<T, bool>> predicate)
        {
            return _collection.DeleteMany(predicate).DeletedCount;
        }

        public virtual async Task SaveAsync(T model)
        {
            await _collection.ReplaceOneAsync(x => x.ID.Equals(model.ID), model, new UpdateOptions
            {
                IsUpsert = true
            });
        }

        public virtual void Save(T model)
        {
            _collection.ReplaceOne(x => x.ID.Equals(model.ID), model, new UpdateOptions
            {
                IsUpsert = true
            });
        }

        public List<T> Find(FilterDefinition<T> filterDefinition)
        {
            return _collection.Find(filterDefinition).ToList();
        }

        public IFindFluent<BsonDocument, BsonDocument> Find(FilterDefinition<BsonDocument> filterDefinition)
        {
            return _bsonCollection.Find(filterDefinition);
        }

        public virtual async Task<List<T>> FindAsync(FilterDefinition<T> filterDefinition)
        {
            return await _collection.Find(filterDefinition).ToListAsync();
        }

        public virtual Task<IAsyncCursor<BsonDocument>> FindAsync(FilterDefinition<BsonDocument> filterDefinition)
        {
            return _bsonCollection.FindAsync(filterDefinition);
        }

        public virtual async Task InsertManyAsync(IEnumerable<T> model)
        {
            await _collection.InsertManyAsync(model);
        }

        public virtual void InsertMany(IEnumerable<T> model)
        {
            _collection.InsertMany(model);
        }

        public Task<IAsyncCursor<T>> FindDocumentAsync(FilterDefinition<T> filterDefinition)
        {
            return _collection.FindAsync(filterDefinition);
        }

        public IFindFluent<T, T> FindDocument(Expression<Func<T, bool>> expression)
        {
            return _collection.Find(expression);
        }
        public Task<IAsyncCursor<T>> FindDocumentAsync(Expression<Func<T, bool>> expression)
        {
            return _collection.FindAsync(expression);
        }

        public IFindFluent<T, T> FindDocument(FilterModel filterModel)
        {
            return FindDocument(filterModel.GetSearchExpression<T>());
        }

        public Task<IAsyncCursor<T>> FindDocumentAsync(FilterModel filterModel)
        {
            return FindDocumentAsync(filterModel.GetSearchExpression<T>());
        }

        public virtual IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return Find(expression).AsQueryable();
        }

        public virtual IAsyncEnumerable<T> WhereAsync(Expression<Func<T, bool>> expression)
        {
            return Find(expression).ToAsyncEnumerable();
        }

        public virtual IQueryable<T> Where(FilterModel filterModel)
        {
            return Where(filterModel.GetSearchExpression<T>());
        }

        public virtual IAsyncEnumerable<T> WhereAsync(FilterModel filterModel)
        {
            return WhereAsync(filterModel.GetSearchExpression<T>());
        }

        public virtual bool Existed(TIdentifier id)
        {
            return CountLong(m => m.ID.Equals(id)) > 0;
        }

        public virtual async Task<bool> ExistedAsync(TIdentifier id)
        {
            return await CountLongAsync(m => m.ID.Equals(id)) > 0;
        }

        public virtual bool Existed(Expression<Func<T, bool>> expression)
        {
            return CountLong(expression) > 0;
        }

        public virtual async Task<bool> ExistedAsync(Expression<Func<T, bool>> expression)
        {
            return await CountLongAsync(expression) > 0;
        }

        public virtual bool Existed(FilterModel filterModel)
        {
            return Existed(filterModel.GetSearchExpression<T>());
        }

        public virtual List<T> Find(Expression<Func<T, bool>> expression)
        {
            return FindDocument(expression).ToList();
        }

        public virtual List<T> Find(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression)
        {
            return Find(expression, orderExpression, SortOrder.Ascending);
        }

        public virtual List<T> Find(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder)
        {
            List<T> result = Find(expression);
            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    result = result.OrderBy(orderExpression.Compile()).ToList();
                    break;
                case SortOrder.Descending:
                    result = result.OrderByDescending(orderExpression.Compile()).ToList();
                    break;
            }
            return result;
        }

        public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return (await FindDocumentAsync(expression)).ToList();
        }

        public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> expression, Func<T, object> orderExpression)
        {
            return await FindAsync(expression, orderExpression, SortOrder.Ascending);
        }

        public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> expression, Func<T, object> orderExpression, SortOrder sortOrder)
        {
            List<T> result = await FindAsync(expression);
            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    result = result.OrderBy(orderExpression).ToList();
                    break;
                case SortOrder.Descending:
                    result = result.OrderByDescending(orderExpression).ToList();
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

        public virtual T FirstOrDefault(TIdentifier id)
        {
            return _collection.Find(m => m.ID.Equals(id)).FirstOrDefault();
        }

        public virtual async Task<T> FirstOrDefaultAsync(FilterModel filterModel)
        {
            return await FirstOrDefaultAsync(filterModel.GetSearchExpression<T>());
        }

        public virtual T FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return _collection.Find(expression).FirstOrDefault();
        }

        public virtual async Task<T> FirstOrDefaultAsync(TIdentifier id)
        {
            return (await _collection.FindAsync(m => m.ID.Equals(id))).FirstOrDefault();
        }

        public virtual T FirstOrDefault(FilterModel filterModel)
        {
            return FirstOrDefault(filterModel.GetSearchExpression<T>());
        }

        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return (await _collection.FindAsync(expression)).FirstOrDefault();
        }

        public virtual (List<T> result, PageModel pageModel) Paging(PageRequestModel pageRequestModel)
        {
            return Paging(pageRequestModel.GetSearchExpression<T>(), pageRequestModel);
        }

        public virtual (List<T> result, PageModel pageModel) Paging(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression)
        {
            return Paging(pageRequestModel.GetSearchExpression<T>(), orderExpression, pageRequestModel);
        }

        public virtual (List<T> result, PageModel pageModel) Paging(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder)
        {
            return Paging(pageRequestModel.GetSearchExpression<T>(), orderExpression, sortOrder, pageRequestModel);
        }

        public virtual  (List<T> result, PageModel pageModel) Paging(Expression<Func<T, bool>> filterExpression, PageRequestModel pageRequestModel)
        {
            return Paging(filterExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        }

        public virtual  (List<T> result, PageModel pageModel) Paging(Expression<Func<T, bool>> filterExpression, int pagingIndex, int pagingSize)
        {
            return Paging(filterExpression, m => m.ID, pagingIndex, pagingSize);
        }

        public virtual  (List<T> result, PageModel pageModel) Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, PageRequestModel pageRequestModel)
        {
            return Paging(filterExpression, orderExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        }

        public virtual  (List<T> result, PageModel pageModel) Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, PageRequestModel pageRequestModel)
        {
            return Paging(filterExpression, orderExpression, sortOrder, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        }

        public virtual  (List<T> result, PageModel pageModel) Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, int pagingIndex, int pagingSize)
        {
            return Paging(filterExpression, orderExpression, SortOrder.Ascending, pagingIndex, pagingSize);
        }

        public virtual  (List<T> result, PageModel pageModel) Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, int pagingIndex, int pagingSize)
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

        public virtual  async Task<(List<T> result, PageModel pageModel)> PagingAsync(Expression<Func<T, bool>> filterExpression, PageRequestModel pageRequestModel)
        {
            return await PagingAsync(filterExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        }

        public virtual  async Task<(List<T> result, PageModel pageModel)> PagingAsync(Expression<Func<T, bool>> filterExpression, int pagingIndex, int pagingSize)
        {
            return await PagingAsync(filterExpression, m => m.ID, pagingIndex, pagingSize);
        }

        public virtual  async Task<(List<T> result, PageModel pageModel)> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, PageRequestModel pageRequestModel)
        {
            return await PagingAsync(filterExpression, orderExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        }

        public virtual  async Task<(List<T> result, PageModel pageModel)> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, PageRequestModel pageRequestModel)
        {
            return await PagingAsync(filterExpression, orderExpression, sortOrder, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        }

        public virtual  async Task<(List<T> result, PageModel pageModel)> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, int pagingIndex, int pagingSize)
        {
            return await PagingAsync(filterExpression, orderExpression, SortOrder.Ascending, pagingIndex, pagingSize);
        }

        public virtual  async Task<(List<T> result, PageModel pageModel)> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, int pagingIndex, int pagingSize)
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
