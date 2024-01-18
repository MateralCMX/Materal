using MongoDB.Driver;

namespace Materal.Utils.MongoDB
{
    /// <summary>
    /// MongoDB仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TID"></typeparam>
    public class MongoRepository<T, TID> : MongoRepository<T>, IMongoRepository<T, TID>
        where T : class, IMongoEntity<TID>, new()
        where TID : struct
    {
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task<long> UpdateAsync(T data)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(m => m.ID, data.ID);
            return base.UpdateAsync(filter, data);
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public long Update(T data)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(m => m.ID, data.ID);
            return Update(filter, data);
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDefinition"></param>
        /// <returns></returns>
        public async Task<long> UpdateAsync(TID id, UpdateDefinition<T> updateDefinition)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(m => m.ID, id);
            return await UpdateOneAsync(filter, updateDefinition);
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDefinition"></param>
        /// <returns></returns>
        public long Update(TID id, UpdateDefinition<T> updateDefinition)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(m => m.ID, id);
            return UpdateOne(filter, updateDefinition);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<long> DeleteAsync(TID id)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(m => m.ID, id);
            return await DeleteOneAsync(filter);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public long Delete(TID id)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(m => m.ID, id);
            return DeleteOne(filter);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<long> DeleteAsync(T data)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(m => m.ID, data.ID);
            return await DeleteOneAsync(filter);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public long Delete(T data)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(m => m.ID, data.ID);
            return DeleteOne(filter);
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> FirstAsync(TID id)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(m => m.ID, id);
            return await FirstAsync(filter);
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T First(TID id)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(m => m.ID, id);
            return First(filter);
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T?> FirstOrDefaultAsync(TID id)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(m => m.ID, id);
            return await FirstOrDefaultAsync(filter);
        }
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T? FirstOrDefault(TID id)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(m => m.ID, id);
            return FirstOrDefault(filter);
        }
    }
}
