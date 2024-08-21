using Materal.Utils.MongoDB.Extensions;
using MongoDB.Driver;

namespace Materal.Utils.MongoDB
{
    public partial class MongoRepository<T>
    {
        #region 替换
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual async Task<long> UpdateAsync(FilterDefinition<T> filter, T data)
        {
            IMongoCollection<T> collection = await GetCollectionAsync();
            ReplaceOneResult result = await collection.ReplaceOneAsync(filter, data);
            if (!result.IsAcknowledged) throw new MongoUtilException($"更新失败");
            return result.ModifiedCount;
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual long Update(FilterDefinition<T> filter, T data)
        {
            IMongoCollection<T> collection = GetCollection();
            ReplaceOneResult result = collection.ReplaceOne(filter, data);
            if (!result.IsAcknowledged) throw new MongoUtilException($"更新失败");
            return result.ModifiedCount;
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual async Task<long> UpdateAsync(Expression<Func<T, bool>> filter, T data)
        {
            IMongoCollection<T> collection = await GetCollectionAsync();
            ReplaceOneResult result = await collection.ReplaceOneAsync(filter, data);
            if (!result.IsAcknowledged) throw new MongoUtilException($"更新失败");
            return result.ModifiedCount;
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual long Update(Expression<Func<T, bool>> filter, T data)
        {
            IMongoCollection<T> collection = GetCollection();
            ReplaceOneResult result = collection.ReplaceOne(filter, data);
            if (!result.IsAcknowledged) throw new MongoUtilException($"更新失败");
            return result.ModifiedCount;
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual async Task<long> UpdateAsync(FilterModel filterModel, T data)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await UpdateAsync(filter, data);
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual long Update(FilterModel filterModel, T data)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return Update(filter, data);
        }
        #endregion
        #region 更新多条数据
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public virtual async Task<long> UpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            IMongoCollection<T> collection = await GetCollectionAsync();
            UpdateResult result = await collection.UpdateManyAsync(filter, update);
            if (!result.IsAcknowledged) throw new MongoUtilException($"更新失败");
            return result.ModifiedCount;
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public virtual long Update(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            IMongoCollection<T> collection = GetCollection();
            UpdateResult result = collection.UpdateMany(filter, update);
            if (!result.IsAcknowledged) throw new MongoUtilException($"更新失败");
            return result.ModifiedCount;
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public virtual async Task<long> UpdateAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update)
        {
            IMongoCollection<T> collection = await GetCollectionAsync();
            UpdateResult result = await collection.UpdateManyAsync(filter, update);
            if (!result.IsAcknowledged) throw new MongoUtilException($"更新失败");
            return result.ModifiedCount;
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public virtual long Update(Expression<Func<T, bool>> filter, UpdateDefinition<T> update)
        {
            IMongoCollection<T> collection = GetCollection();
            UpdateResult result = collection.UpdateMany(filter, update);
            if (!result.IsAcknowledged) throw new MongoUtilException($"更新失败");
            return result.ModifiedCount;
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public virtual async Task<long> UpdateAsync(FilterModel filterModel, UpdateDefinition<T> update)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await UpdateAsync(filter, update);
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public virtual long Update(FilterModel filterModel, UpdateDefinition<T> update)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return Update(filter, update);
        }
        #endregion
        #region 更新一条数据
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public virtual async Task<long> UpdateOneAsync(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            IMongoCollection<T> collection = await GetCollectionAsync();
            UpdateResult result = await collection.UpdateOneAsync(filter, update);
            if (!result.IsAcknowledged) throw new MongoUtilException($"更新失败");
            return result.ModifiedCount;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public virtual long UpdateOne(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            IMongoCollection<T> collection = GetCollection();
            UpdateResult result = collection.UpdateOne(filter, update);
            if (!result.IsAcknowledged) throw new MongoUtilException($"更新失败");
            return result.ModifiedCount;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public virtual async Task<long> UpdateOneAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update)
        {
            IMongoCollection<T> collection = await GetCollectionAsync();
            UpdateResult result = await collection.UpdateOneAsync(filter, update);
            if (!result.IsAcknowledged) throw new MongoUtilException($"更新失败");
            return result.ModifiedCount;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public virtual long UpdateOne(Expression<Func<T, bool>> filter, UpdateDefinition<T> update)
        {
            IMongoCollection<T> collection = GetCollection();
            UpdateResult result = collection.UpdateOne(filter, update);
            if (!result.IsAcknowledged) throw new MongoUtilException($"更新失败");
            return result.ModifiedCount;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public virtual async Task<long> UpdateOneAsync(FilterModel filterModel, UpdateDefinition<T> update)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await UpdateOneAsync(filter, update);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public virtual long UpdateOne(FilterModel filterModel, UpdateDefinition<T> update)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return UpdateOne(filter, update);
        }
        #endregion
    }
}
