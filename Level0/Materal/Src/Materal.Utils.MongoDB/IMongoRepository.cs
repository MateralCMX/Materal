using MongoDB.Bson;

namespace Materal.Utils.MongoDB
{
    /// <summary>
    /// MongoDB仓储
    /// </summary>
    public interface IMongoRepository : IMongoRepository<BsonDocument>
    {
        //#region Insert
        ///// <summary>
        ///// 插入数据
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //Task InsertAsync<T>(T data);
        ///// <summary>
        ///// 插入数据
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //void Insert<T>(T data);
        ///// <summary>
        ///// 插入多条数据
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //Task InsertAsync<T>(IEnumerable<T> data);
        ///// <summary>
        ///// 插入多条数据
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //void Insert<T>(IEnumerable<T> data);
        //#endregion
        //#region Update
        ///// <summary>
        ///// 更新数据
        ///// </summary>
        ///// <param name="filter"></param>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //Task<long> UpdateAsync<T>(FilterDefinition<BsonDocument> filter, T data);
        ///// <summary>
        ///// 更新数据
        ///// </summary>
        ///// <param name="filter"></param>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //long Update<T>(FilterDefinition<BsonDocument> filter, T data);
        ///// <summary>
        ///// 更新数据
        ///// </summary>
        ///// <param name="filter"></param>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //Task<long> UpdateAsync<T>(Expression<Func<BsonDocument, bool>> filter, T data);
        ///// <summary>
        ///// 更新数据
        ///// </summary>
        ///// <param name="filter"></param>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //long Update<T>(Expression<Func<BsonDocument, bool>> filter, T data);
        ///// <summary>
        ///// 更新数据
        ///// </summary>
        ///// <param name="filterModel"></param>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //Task<long> UpdateAsync<T>(FilterModel filterModel, T data);
        ///// <summary>
        ///// 更新数据
        ///// </summary>
        ///// <param name="filterModel"></param>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //long Update<T>(FilterModel filterModel, T data);
        //#endregion
    }
}
