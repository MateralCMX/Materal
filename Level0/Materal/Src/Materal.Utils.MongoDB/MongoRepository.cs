using Materal.Extensions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Materal.Utils.MongoDB
{
    /// <summary>
    /// MongoDB仓储
    /// </summary>
    public class MongoRepository : MongoRepository<BsonDocument>, IMongoRepository
    {
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual async Task InsertAsync<T>(T data)
        {
            IMongoCollection<BsonDocument> collection = await GetCollectionAsync();
            BsonDocument bsonElement = ConvertToBsonDocument(data);
            await collection.InsertOneAsync(bsonElement);
        }
        /// <summary>
        /// 插入多条数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual async Task InsertAsync<T>(IEnumerable<T> data)
        {
            IMongoCollection<BsonDocument> collection = await GetCollectionAsync();
            IEnumerable<BsonDocument> bsonElements = data.Select(m => ConvertToBsonDocument(m));
            await collection.InsertManyAsync(bsonElements);
        }
        /// <summary>
        /// 获取Bson文档
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected virtual BsonDocument ConvertToBsonDocument(object? obj)
        {
            Dictionary<string, object?> dictionary = ConvertToDictionary(obj);
            BsonDocument result = new(dictionary);
            return result;
        }
        /// <summary>
        /// 获取字典
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected virtual Dictionary<string, object?> ConvertToDictionary(object? obj)
        {
            Dictionary<string, object?> result = [];
            if (obj is null) return result;
            Type objType = obj.GetType();
            foreach (PropertyInfo propertyInfo in objType.GetProperties())
            {
                if (!propertyInfo.CanRead || !propertyInfo.CanWrite) continue;
                object? valueObj = propertyInfo.GetValue(obj)?.ToExpandoObject();
                result.TryAdd(propertyInfo.Name, valueObj);
            }
            return result;
        }
    }
}
