using MongoDB.Driver;

namespace Materal.Utils.MongoDB
{
    /// <summary>
    /// MongoDB仓储
    /// </summary>
    public interface IMongoRepository<T, TID> : IMongoRepository<T>
        where T : class, IMongoEntity<TID>, new()
        where TID : struct
    {
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<long> UpdateAsync(T data);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        long Update(T data);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDefinition"></param>
        /// <returns></returns>
        Task<long> UpdateAsync(TID id, UpdateDefinition<T> updateDefinition);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDefinition"></param>
        /// <returns></returns>
        long Update(TID id, UpdateDefinition<T> updateDefinition);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<long> DeleteAsync(TID id);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        long Delete(TID id);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<long> DeleteAsync(T data);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        long Delete(T data);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> FirstAsync(TID id);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T First(TID id);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T?> FirstOrDefaultAsync(TID id);
        /// <summary>
        /// 查询第一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T? FirstOrDefault(TID id);
    }
}
