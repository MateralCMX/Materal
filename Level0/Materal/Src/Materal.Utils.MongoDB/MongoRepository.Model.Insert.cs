using MongoDB.Driver;

namespace Materal.Utils.MongoDB
{
    public partial class MongoRepository<T>
    {
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual async Task InsertAsync(T data)
        {
            IMongoCollection<T> collection = await GetCollectionAsync();
            await collection.InsertOneAsync(data);
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual void Insert(T data)
        {
            IMongoCollection<T> collection = GetCollection();
            collection.InsertOne(data);
        }
        /// <summary>
        /// 插入多条数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual async Task InsertAsync(IEnumerable<T> data)
        {
            IMongoCollection<T> collection = await GetCollectionAsync();
            await collection.InsertManyAsync(data);
        }
        /// <summary>
        /// 插入多条数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual void Insert(IEnumerable<T> data)
        {
            IMongoCollection<T> collection = GetCollection();
            collection.InsertMany(data);
        }
    }
}
