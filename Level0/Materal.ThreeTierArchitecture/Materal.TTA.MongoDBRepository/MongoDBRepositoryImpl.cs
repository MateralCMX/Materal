using Materal.Abstractions;
using Materal.Utils.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace Materal.TTA.MongoDBRepository
{
    public class MongoDBRepositoryImpl<T, TPrimaryKeyType> : IMongoDBRepository<T, TPrimaryKeyType>
        where T : MongoEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
    {
        protected readonly IMongoDatabase MongoDatabase;
        protected IMongoCollection<T>? Collection;
        protected IMongoCollection<BsonDocument>? BsonCollection;

        protected MongoDBRepositoryImpl(string connectionString, string dataBaseNameString, string? collectionNameString = null)
        {
            if (string.IsNullOrEmpty(collectionNameString)) collectionNameString = typeof(T).Name;
            MongoDatabase = new MongoClient(connectionString).GetDatabase(dataBaseNameString);
            SelectCollection(collectionNameString);
        }
        public void SelectCollection(string collectionNameString)
        {
            Collection = MongoDatabase.GetCollection<T>(collectionNameString);
            BsonCollection = MongoDatabase.GetCollection<BsonDocument>(collectionNameString);
        }
        public virtual async Task<bool> ExistedAsync(FilterModel filterModel) => await ExistedAsync(filterModel.GetSearchExpression<T>());
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
        public virtual int Count(FilterModel filterModel) => Count(filterModel.GetSearchExpression<T>());
        public virtual async Task<int> CountAsync(FilterModel filterModel) => await CountAsync(filterModel.GetSearchExpression<T>());

        public virtual long CountLong(Expression<Func<T, bool>> expression)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            return Collection.CountDocuments(expression);
        }

        public virtual async Task<long> CountLongAsync(Expression<Func<T, bool>> expression)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            return await Collection.CountDocumentsAsync(expression);
        }

        public virtual async Task InsertAsync(T model)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            await Collection.InsertOneAsync(model);
        }

        public virtual void Insert(T model)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            Collection.InsertOne(model);
        }

        public virtual async Task InsertManyAsync(List<T> models)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            await Collection.InsertManyAsync(models);
        }
        public virtual void InsertMany(List<T> models)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            Collection.InsertMany(models);
        }

        public void Delete(T model)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            Collection.DeleteOne(m => m.ID.Equals(model.ID));
        }

        public async Task DeleteAsync(T model)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            await Collection.DeleteOneAsync(m => m.ID.Equals(model.ID));
        }

        public void Delete(TPrimaryKeyType id)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            Collection.DeleteOne(m => m.ID.Equals(id));
        }

        public virtual async Task DeleteAsync(TPrimaryKeyType id)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            await Collection.DeleteOneAsync(m => m.ID.Equals(id));
        }

        public virtual async Task<long> DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            return (await Collection.DeleteOneAsync(predicate)).DeletedCount;
        }
        public virtual long Delete(Expression<Func<T, bool>> predicate)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            return Collection.DeleteOne(predicate).DeletedCount;
        }
        public virtual async Task<long> DeleteManyAsync(Expression<Func<T, bool>> predicate)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            return (await Collection.DeleteManyAsync(predicate)).DeletedCount;
        }
        public virtual long DeleteMany(Expression<Func<T, bool>> predicate)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            return Collection.DeleteMany(predicate).DeletedCount;
        }

        public async Task<long> DeleteManyAsync(List<T> models)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            TPrimaryKeyType[] allIDs = models.Select(m => m.ID).ToArray();
            return (await Collection.DeleteManyAsync(m=> allIDs.Contains(m.ID))).DeletedCount;
        }

        public long DeleteMany(List<T> models)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            TPrimaryKeyType[] allIDs = models.Select(m => m.ID).ToArray();
            return Collection.DeleteMany(m => allIDs.Contains(m.ID)).DeletedCount;
        }

        public async Task SaveManyAsync(List<T> models)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            var writeModels = new List<WriteModel<T>>();
            foreach (T model in models)
            {
                FilterDefinition<T> filter = Builders<T>.Filter.Eq(m => m.ID, model.ID);
                writeModels.Add(new ReplaceOneModel<T>(filter, model) { IsUpsert = true });
            }
            await Collection.BulkWriteAsync(writeModels);
        }

        public void SaveMany(List<T> models)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            var writeModels = new List<WriteModel<T>>();
            foreach (T model in models)
            {
                FilterDefinition<T> filter = Builders<T>.Filter.Eq(m=>m.ID, model.ID);
                writeModels.Add(new ReplaceOneModel<T>(filter, model) { IsUpsert = true });
            }
            Collection.BulkWrite(writeModels);
        }

        public virtual async Task SaveAsync(T model)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            await Collection.ReplaceOneAsync(m=> m.ID.Equals(model.ID), model, new ReplaceOptions
            {
                IsUpsert = true
            });
        }

        public virtual void Save(T model)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            Collection.ReplaceOne(m => m.ID.Equals(model.ID), model, new ReplaceOptions
            {
                IsUpsert = true
            });
        }

        public List<T> Find(FilterDefinition<T> filterDefinition)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            return Collection.Find(filterDefinition).ToList();
        }

        public IFindFluent<BsonDocument, BsonDocument> Find(FilterDefinition<BsonDocument> filterDefinition)
        {
            if (BsonCollection == null) throw new MateralException("未选择集合");
            return BsonCollection.Find(filterDefinition);
        }

        public virtual async Task<List<T>> FindAsync(FilterDefinition<T> filterDefinition)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            return await Collection.Find(filterDefinition).ToListAsync();
        }

        public virtual Task<IAsyncCursor<BsonDocument>> FindAsync(FilterDefinition<BsonDocument> filterDefinition)
        {
            if (BsonCollection == null) throw new MateralException("未选择集合");
            return BsonCollection.FindAsync(filterDefinition);
        }

        public virtual async Task InsertManyAsync(IEnumerable<T> model)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            await Collection.InsertManyAsync(model);
        }

        public virtual void InsertMany(IEnumerable<T> model)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            Collection.InsertMany(model);
        }

        public Task<IAsyncCursor<T>> FindDocumentAsync(FilterDefinition<T> filterDefinition)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            return Collection.FindAsync(filterDefinition);
        }

        public IFindFluent<T, T> FindDocument(FilterDefinition<T> filterDefinition)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            return Collection.Find(filterDefinition);
        }
        public IFindFluent<T, T> FindDocument(Expression<Func<T, bool>> expression)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            return Collection.Find(expression);
        }
        public Task<IAsyncCursor<T>> FindDocumentAsync(Expression<Func<T, bool>> expression)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            return Collection.FindAsync(expression);
        }
        public IFindFluent<T, T> FindDocument(FilterModel filterModel) => FindDocument(filterModel.GetSearchExpression<T>());
        public Task<IAsyncCursor<T>> FindDocumentAsync(FilterModel filterModel) => FindDocumentAsync(filterModel.GetSearchExpression<T>());
        public (List<T> result, PageModel pageModel) Paging(FilterDefinition<T> filterDefinition, PageRequestModel pageRequestModel) => Paging(filterDefinition, pageRequestModel.PageIndex, pageRequestModel.PageSize, pageRequestModel.Skip, pageRequestModel.Take);
        public (List<T> result, PageModel pageModel) Paging(FilterDefinition<T> filterDefinition, SortDefinition<T> sortDefinition, PageRequestModel pageRequestModel) => Paging(filterDefinition, sortDefinition, pageRequestModel.PageIndex, pageRequestModel.PageSize, pageRequestModel.Skip, pageRequestModel.Take);
        public (List<T> result, PageModel pageModel) Paging(FilterDefinition<T> filterDefinition, int pageIndex, int pageSize, int skip, int take) => Paging(filterDefinition, null, pageIndex, pageSize, skip, take);
        public (List<T> result, PageModel pageModel) Paging(FilterDefinition<T> filterDefinition, SortDefinition<T>? sortDefinition, int pageIndex, int pageSize, int skip, int take)
        {
            IFindFluent<T, T> find = Collection.Find(filterDefinition);
            if (sortDefinition != null)
            {
                find = find.Sort(sortDefinition);
            }
            var pageModel = new PageModel(pageIndex, pageSize, Convert.ToInt32(find.CountDocuments()));
            List<T> result = find.Skip(skip).Limit(take).ToList();
            return (result, pageModel);
        }
        public virtual IQueryable<T> Where(Expression<Func<T, bool>> expression) => Find(expression).AsQueryable();
        public virtual IQueryable<T> Where(FilterModel filterModel) => Where(filterModel.GetSearchExpression<T>());
        public virtual bool Existed(TPrimaryKeyType id) => CountLong(m => m.ID.Equals(id)) > 0;
        public virtual async Task<bool> ExistedAsync(TPrimaryKeyType id) => await CountLongAsync(m => m.ID.Equals(id)) > 0;
        public virtual bool Existed(Expression<Func<T, bool>> expression) => CountLong(expression) > 0;
        public virtual async Task<bool> ExistedAsync(Expression<Func<T, bool>> expression) => await CountLongAsync(expression) > 0;
        public virtual bool Existed(FilterModel filterModel) => Existed(filterModel.GetSearchExpression<T>());
        public virtual List<T> Find(Expression<Func<T, bool>> expression) => FindDocument(expression).ToList();
        public virtual List<T> Find(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression) => Find(expression, orderExpression, SortOrder.Ascending);
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
        public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> expression) => (await FindDocumentAsync(expression)).ToList();
        public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> expression, Func<T, object> orderExpression) => await FindAsync(expression, orderExpression, SortOrder.Ascending);
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
        public virtual List<T> Find(FilterModel filterModel) => Find(filterModel.GetSearchExpression<T>());
        public virtual List<T> Find(FilterModel filterModel, Expression<Func<T, object>> orderExpression) => Find(filterModel.GetSearchExpression<T>(), orderExpression);
        public virtual List<T> Find(FilterModel filterModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder) => Find(filterModel.GetSearchExpression<T>(), orderExpression, sortOrder);
        public virtual async Task<List<T>> FindAsync(FilterModel filterModel) => await FindAsync(filterModel.GetSearchExpression<T>());
        public virtual async Task<List<T>> FindAsync(FilterModel filterModel, Func<T, object> orderExpression) => await FindAsync(filterModel.GetSearchExpression<T>(), orderExpression);
        public virtual async Task<List<T>> FindAsync(FilterModel filterModel, Func<T, object> orderExpression, SortOrder sortOrder) => await FindAsync(filterModel.GetSearchExpression<T>(), orderExpression, sortOrder);
        public virtual T FirstOrDefault(TPrimaryKeyType id)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            return Collection.Find(m => m.ID.Equals(id)).FirstOrDefault();
        }
        public virtual async Task<T> FirstOrDefaultAsync(FilterModel filterModel) => await FirstOrDefaultAsync(filterModel.GetSearchExpression<T>());
        public virtual T FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            return Collection.Find(expression).FirstOrDefault();
        }
        public virtual async Task<T> FirstOrDefaultAsync(TPrimaryKeyType id)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            return (await Collection.FindAsync(m => m.ID.Equals(id))).FirstOrDefault();
        }
        public virtual T FirstOrDefault(FilterModel filterModel) => FirstOrDefault(filterModel.GetSearchExpression<T>());
        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            if (Collection == null) throw new MateralException("未选择集合");
            return (await Collection.FindAsync(expression)).FirstOrDefault();
        }
        public virtual (List<T> result, PageModel pageModel) Paging(PageRequestModel pageRequestModel) => Paging(pageRequestModel.GetSearchExpression<T>(), pageRequestModel);
        public virtual (List<T> result, PageModel pageModel) Paging(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression) => Paging(pageRequestModel.GetSearchExpression<T>(), orderExpression, pageRequestModel);
        public virtual (List<T> result, PageModel pageModel) Paging(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder) => Paging(pageRequestModel.GetSearchExpression<T>(), orderExpression, sortOrder, pageRequestModel);
        public virtual (List<T> result, PageModel pageModel) Paging(Expression<Func<T, bool>> filterExpression, PageRequestModel pageRequestModel) => Paging(filterExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        public virtual (List<T> result, PageModel pageModel) Paging(Expression<Func<T, bool>> filterExpression, int pagingIndex, int pagingSize) => Paging(filterExpression, m => m.ID, pagingIndex, pagingSize);
        public virtual (List<T> result, PageModel pageModel) Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, PageRequestModel pageRequestModel) => Paging(filterExpression, orderExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        public virtual (List<T> result, PageModel pageModel) Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, PageRequestModel pageRequestModel) => Paging(filterExpression, orderExpression, sortOrder, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        public virtual (List<T> result, PageModel pageModel) Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, int pagingIndex, int pagingSize) => Paging(filterExpression, orderExpression, SortOrder.Ascending, pagingIndex, pagingSize);
        public virtual  (List<T> result, PageModel pageModel) Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, int pagingIndex, int pagingSize)
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

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
