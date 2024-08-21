using Materal.Utils.MongoDB.Extensions;
using MongoDB.Driver;

namespace Materal.Utils.MongoDB
{
    public partial class MongoRepository<T>
    {
        #region 删除多条
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual async Task<long> DeleteAsync(FilterDefinition<T> filter)
        {
            IMongoCollection<T> collection = await GetCollectionAsync();
            DeleteResult result = await collection.DeleteManyAsync(filter);
            if (!result.IsAcknowledged) throw new MongoUtilException($"删除失败");
            return result.DeletedCount;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual long Delete(FilterDefinition<T> filter)
        {
            IMongoCollection<T> collection = GetCollection();
            DeleteResult result = collection.DeleteMany(filter);
            if (!result.IsAcknowledged) throw new MongoUtilException($"删除失败");
            return result.DeletedCount;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual async Task<long> DeleteAsync(Expression<Func<T, bool>> filter)
        {
            IMongoCollection<T> collection = await GetCollectionAsync();
            DeleteResult result = await collection.DeleteManyAsync(filter);
            if (!result.IsAcknowledged) throw new MongoUtilException($"删除失败");
            return result.DeletedCount;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual long Delete(Expression<Func<T, bool>> filter)
        {
            IMongoCollection<T> collection = GetCollection();
            DeleteResult result = collection.DeleteMany(filter);
            if (!result.IsAcknowledged) throw new MongoUtilException($"删除失败");
            return result.DeletedCount;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual async Task<long> DeleteAsync(FilterModel filterModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await DeleteAsync(filter);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual long Delete(FilterModel filterModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return Delete(filter);
        }
        /// <summary>
        /// 清空数据
        /// </summary>
        /// <returns></returns>
        public virtual async Task<long> ClearAsync() => await DeleteAsync(Builders<T>.Filter.Empty);
        /// <summary>
        /// 清空数据
        /// </summary>
        /// <returns></returns>
        public virtual long Clear() => Delete(Builders<T>.Filter.Empty);
        #endregion
        #region 删除一条
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual async Task<long> DeleteOneAsync(FilterDefinition<T> filter)
        {
            IMongoCollection<T> collection = await GetCollectionAsync();
            DeleteResult result = await collection.DeleteOneAsync(filter);
            if (!result.IsAcknowledged) throw new MongoUtilException($"删除失败");
            return result.DeletedCount;
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual long DeleteOne(FilterDefinition<T> filter)
        {
            IMongoCollection<T> collection = GetCollection();
            DeleteResult result = collection.DeleteOne(filter);
            if (!result.IsAcknowledged) throw new MongoUtilException($"删除失败");
            return result.DeletedCount;
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual async Task<long> DeleteOneAsync(Expression<Func<T, bool>> filter)
        {
            IMongoCollection<T> collection = await GetCollectionAsync();
            DeleteResult result = await collection.DeleteOneAsync(filter);
            if (!result.IsAcknowledged) throw new MongoUtilException($"删除失败");
            return result.DeletedCount;
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual long DeleteOne(Expression<Func<T, bool>> filter)
        {
            IMongoCollection<T> collection = GetCollection();
            DeleteResult result = collection.DeleteOne(filter);
            if (!result.IsAcknowledged) throw new MongoUtilException($"删除失败");
            return result.DeletedCount;
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual async Task<long> DeleteOneAsync(FilterModel filterModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await DeleteOneAsync(filter);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual long DeleteOne(FilterModel filterModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return DeleteOne(filter);
        }
        #endregion
    }
}
