using Materal.Utils.MongoDB.Extensions;
using MongoDB.Driver;

namespace Materal.Utils.MongoDB
{
    public partial class MongoRepository<T>
    {
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual async Task<long> CountAsync(FilterDefinition<T> filter)
        {
            IMongoCollection<T> collection = await GetCollectionAsync();
            long result = await collection.CountDocumentsAsync(filter);
            return result;
        }
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual long Count(FilterDefinition<T> filter)
        {
            IMongoCollection<T> collection = GetCollection();
            long result = collection.CountDocuments(filter);
            return result;
        }
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual async Task<long> CountAsync(Expression<Func<T, bool>> filter)
        {
            IMongoCollection<T> collection = await GetCollectionAsync();
            long result = await collection.CountDocumentsAsync(filter);
            return result;
        }
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual long Count(Expression<Func<T, bool>> filter)
        {
            IMongoCollection<T> collection = GetCollection();
            long result = collection.CountDocuments(filter);
            return result;
        }
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual async Task<long> CountAsync(FilterModel filterModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await CountAsync(filter);
        }
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual long Count(FilterModel filterModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return Count(filter);
        }
    }
}
