using Materal.Utils.MongoDB.Extensions;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;

namespace Materal.Utils.MongoDB
{
    /// <summary>
    /// MongoDB仓储
    /// </summary>
    public class MongoRepository : MongoRepository<BsonDocument>, IMongoRepository
    {
        #region Insert
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual async Task InsertObjectAsync(object data)
        {
            if (data is IEnumerable enumberable)
            {
                IEnumerable<BsonDocument> bsonElements = enumberable.ToBsonObjects();
                await base.InsertAsync(bsonElements);
            }
            else
            {
                BsonDocument bsonElement = data.ToBsonObject();
                await base.InsertAsync(bsonElement);
            }
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual void InsertObject(object data)
        {
            if (data is IEnumerable enumberable)
            {
                IEnumerable<BsonDocument> bsonElements = enumberable.ToBsonObjects();
                base.Insert(bsonElements);
            }
            else
            {
                BsonDocument bsonElement = data.ToBsonObject();
                base.Insert(bsonElement);
            }
        }
        #endregion
        #region Update
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual async Task<long> UpdateObjectAsync(FilterDefinition<BsonDocument> filter, object data)
        {
            BsonDocument bsonElement = data.ToBsonObject();
            return await base.UpdateAsync(filter, bsonElement);
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual long UpdateObject(FilterDefinition<BsonDocument> filter, object data)
        {
            BsonDocument bsonElement = data.ToBsonObject();
            return base.Update(filter, bsonElement);
        }
        ///// <summary>
        ///// 更新数据
        ///// </summary>
        ///// <param name="filter"></param>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public virtual async Task<long> UpdateAsync<T>(Expression<Func<BsonDocument, bool>> filter, T data)
        //{
        //    BsonDocument bsonElement = data.ToBsonObject();
        //    return await base.UpdateAsync(filter, bsonElement);
        //}
        ///// <summary>
        ///// 更新数据
        ///// </summary>
        ///// <param name="filter"></param>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public virtual long Update<T>(Expression<Func<BsonDocument, bool>> filter, T data)
        //{
        //    BsonDocument bsonElement = data.ToBsonObject();
        //    return base.Update(filter, bsonElement);
        //}
        ///// <summary>
        ///// 更新数据
        ///// </summary>
        ///// <param name="filterModel"></param>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public virtual async Task<long> UpdateAsync<T>(FilterModel filterModel, T data)
        //{
        //    BsonDocument bsonElement = data.ToBsonObject();
        //    return await base.UpdateAsync(filterModel, bsonElement);
        //}
        ///// <summary>
        ///// 更新数据
        ///// </summary>
        ///// <param name="filterModel"></param>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public virtual long Update<T>(FilterModel filterModel, T data)
        //{
        //    BsonDocument bsonElement = data.ToBsonObject();
        //    return base.Update(filterModel, bsonElement);
        //}
        #endregion
    }
}
