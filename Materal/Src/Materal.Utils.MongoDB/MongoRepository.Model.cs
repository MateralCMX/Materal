using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Materal.Utils.MongoDB
{
    /// <summary>
    /// MongoDB仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class MongoRepository<T> : IMongoRepository<T>
        where T : class, new()
    {
        static MongoRepository()
        {
            BsonSerializer.TryRegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            IDiscriminatorConvention objectDiscriminatorConvention = BsonSerializer.LookupDiscriminatorConvention(typeof(object));
            ObjectSerializer objectSerializer = new(objectDiscriminatorConvention, GuidRepresentation.Standard);
            BsonSerializer.TryRegisterSerializer(objectSerializer);
        }
        /// <summary>
        /// 连接字符串
        /// </summary>
        public virtual string ConnectionString { get; set; } = string.Empty;
        /// <summary>
        /// 数据库名称
        /// </summary>
        public virtual string DatabaseName { get; set; } = string.Empty;
        /// <summary>
        /// 集合名称
        /// </summary>
        public virtual string CollectionName { get; set; } = string.Empty;
        /// <summary>
        /// 集合选项
        /// </summary>
        public virtual CreateCollectionOptions? CollectionOptions { get; set; }
        /// <summary>
        /// 获取集合
        /// </summary>
        /// <returns></returns>
        /// <exception cref="MongoUtilException"></exception>
        public virtual async Task<IMongoCollection<T>> GetCollectionAsync()
        {
            if (string.IsNullOrWhiteSpace(ConnectionString)) throw new MongoUtilException($"链接字符串为空");
            if (string.IsNullOrWhiteSpace(DatabaseName)) throw new MongoUtilException($"数据库名称为空");
            if (string.IsNullOrWhiteSpace(CollectionName)) throw new MongoUtilException($"集合名称为空");
            MongoClient client;
            try
            {
                client = new(ConnectionString);
            }
            catch (Exception ex)
            {
                throw new MongoUtilException($"链接数据库失败", ex);
            }
            IMongoDatabase db = client.GetDatabase(DatabaseName);
            IAsyncCursor<string> collectionNames = await db.ListCollectionNamesAsync(new ListCollectionNamesOptions
            {
                Filter = new BsonDocument("name", CollectionName)
            });
            if (!collectionNames.Any())
            {
                await db.CreateCollectionAsync(CollectionName, CollectionOptions);
            }
            IMongoCollection<T> collection = db.GetCollection<T>(CollectionName);
            return collection;
        }
        /// <summary>
        /// 获取集合
        /// </summary>
        /// <returns></returns>
        /// <exception cref="MongoUtilException"></exception>
        public virtual IMongoCollection<T> GetCollection()
        {
            if (string.IsNullOrWhiteSpace(ConnectionString)) throw new MongoUtilException($"链接字符串为空");
            if (string.IsNullOrWhiteSpace(DatabaseName)) throw new MongoUtilException($"数据库名称为空");
            if (string.IsNullOrWhiteSpace(CollectionName)) throw new MongoUtilException($"集合名称为空");
            MongoClient client;
            try
            {
                client = new(ConnectionString);
            }
            catch (Exception ex)
            {
                throw new MongoUtilException($"链接数据库失败", ex);
            }
            IMongoDatabase db = client.GetDatabase(DatabaseName);
            IAsyncCursor<string> collectionNames = db.ListCollectionNames(new ListCollectionNamesOptions
            {
                Filter = new BsonDocument("name", CollectionName)
            });
            if (!collectionNames.Any())
            {
                db.CreateCollection(CollectionName, CollectionOptions);
            }
            IMongoCollection<T> collection = db.GetCollection<T>(CollectionName);
            return collection;
        }
        #region 私有方法
        /// <summary>
        /// 获取跳过数量
        /// </summary>
        /// <param name="skip"></param>
        /// <returns></returns>
        private static int GetSkip(int skip) => skip < 0 ? 0 : skip;
        /// <summary>
        /// 获取跳过数量
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private static int GetSkip(int pageIndex, int pageSize)
        {
            int truePageIndex = (pageIndex - PageRequestModel.PageStartNumberInt);
            return truePageIndex < 0 ? 0 : truePageIndex * pageSize;
        }
        /// <summary>
        /// 获取得到数量
        /// </summary>
        /// <param name="pageSizeOrTake"></param>
        /// <returns></returns>
        private static int GetLimit(int pageSizeOrTake) => pageSizeOrTake <= 0 ? 1 : pageSizeOrTake;
        /// <summary>
        /// 获取排序
        /// </summary>
        /// <param name="sortExpression"></param>
        /// <returns></returns>
        private static SortDefinition<T>? GetSortDefinition(Expression<Func<T, object>> sortExpression)
            => GetSortDefinition(sortExpression, SortOrderEnum.Ascending);
        /// <summary>
        /// 获取排序
        /// </summary>
        /// <param name="sortExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        private static SortDefinition<T>? GetSortDefinition(Expression<Func<T, object>> sortExpression, SortOrderEnum sortOrder) => sortOrder switch
        {
            SortOrderEnum.Ascending => Builders<T>.Sort.Ascending(sortExpression),
            SortOrderEnum.Descending => Builders<T>.Sort.Descending(sortExpression),
            _ => null
        };
        /// <summary>
        /// 获取查询选项
        /// </summary>
        /// <param name="sort"></param>
        /// <returns></returns>
        private static FindOptions<T, T> GetFindOptions(SortDefinition<T> sort) => new() { Sort = sort };
        /// <summary>
        /// 获取查询选项
        /// </summary>
        /// <param name="sortExpression"></param>
        /// <returns></returns>
        private static FindOptions<T, T> GetFindOptions(Expression<Func<T, object>> sortExpression) => new() { Sort = GetSortDefinition(sortExpression) };
        /// <summary>
        /// 获取查询选项
        /// </summary>
        /// <param name="sortExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        private static FindOptions<T, T> GetFindOptions(Expression<Func<T, object>> sortExpression, SortOrderEnum sortOrder) => new() { Sort = GetSortDefinition(sortExpression, sortOrder) };
        #endregion
    }
}
