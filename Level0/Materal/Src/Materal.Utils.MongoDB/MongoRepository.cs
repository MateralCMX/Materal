using MongoDB.Bson;

namespace Materal.Utils.MongoDB
{
    /// <summary>
    /// MongoDB仓储
    /// </summary>
    public class MongoRepository : MongoRepository<BsonDocument>, IMongoRepository
    {
        //#region Insert
        ///// <summary>
        ///// 插入数据
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public virtual async Task InsertAsync<T>(T data)
        //{
        //    if (data is not IEnumerable enumberable)
        //    {
        //        BsonDocument bsonElement = data.ToBsonObject();
        //        await base.InsertAsync(bsonElement);
        //    }
        //    else
        //    {
        //        List<BsonDocument> bsonElements = [];
        //        foreach (object? item in enumberable)
        //        {
        //            bsonElements.Add(item.ToBsonObject());
        //        }
        //        await base.InsertAsync(bsonElements);
        //    }
        //}
        ///// <summary>
        ///// 插入数据
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public virtual void Insert<T>(T data)
        //{
        //    BsonDocument bsonElement = data.ToBsonObject();
        //    base.Insert(bsonElement);
        //}
        ///// <summary>
        ///// 插入多条数据
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public virtual async Task InsertAsync<T>(IEnumerable<T> data)
        //{
        //    IEnumerable<BsonDocument> bsonElements = data.Select(m => m.ToBsonObject());
        //    await base.InsertAsync(bsonElements);
        //}
        ///// <summary>
        ///// 插入多条数据
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public virtual void Insert<T>(IEnumerable<T> data)
        //{
        //    IEnumerable<BsonDocument> bsonElements = data.Select(m => m.ToBsonObject());
        //    base.Insert(bsonElements);
        //}
        //#endregion
        //#region Update
        ///// <summary>
        ///// 更新数据
        ///// </summary>
        ///// <param name="filter"></param>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public virtual async Task<long> UpdateAsync<T>(FilterDefinition<BsonDocument> filter, T data)
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
        //public virtual long Update<T>(FilterDefinition<BsonDocument> filter, T data)
        //{
        //    BsonDocument bsonElement = data.ToBsonObject();
        //    return base.Update(filter, bsonElement);
        //}
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
        //#endregion
    }
}
