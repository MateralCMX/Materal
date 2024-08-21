using MongoDB.Bson;
using MongoDB.Driver;

namespace Materal.Utils.MongoDB
{
    /// <summary>
    /// MongoDB仓储
    /// </summary>
    public interface IMongoRepository : IMongoRepository<BsonDocument>
    {
        #region Insert
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task InsertObjectAsync(object data);
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        void InsertObject(object data);
        #endregion
        #region Update
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<long> UpdateObjectAsync(FilterDefinition<BsonDocument> filter, object data);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        long UpdateObject(FilterDefinition<BsonDocument> filter, object data);
        #endregion
    }
}
