using MongoDB.Driver;

namespace Materal.Utils.MongoDB
{
    /// <summary>
    /// MongoDB仓储
    /// </summary>
    public partial interface IMongoRepository<T> where T : class, new()
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        string ConnectionString { get; set; }
        /// <summary>
        /// 数据库名称
        /// </summary>
        string DatabaseName { get; set; }
        /// <summary>
        /// 集合名称
        /// </summary>
        string CollectionName { get; set; }
        /// <summary>
        /// 集合选项
        /// </summary>
        CreateCollectionOptions? CollectionOptions { get; set; }
        /// <summary>
        /// 获取集合
        /// </summary>
        /// <returns></returns>
        Task<IMongoCollection<T>> GetCollectionAsync();
        /// <summary>
        /// 获取集合
        /// </summary>
        /// <returns></returns>
        IMongoCollection<T> GetCollection();
    }
}
