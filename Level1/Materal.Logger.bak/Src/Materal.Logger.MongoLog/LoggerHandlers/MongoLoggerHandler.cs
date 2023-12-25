using Materal.Logger.MongoLog.LoggerHandlers.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Materal.Logger.LoggerHandlers
{
    /// <summary>
    /// Mongo日志处理器
    /// </summary>
    /// <param name="loggerRuntime"></param>
    public class MongoLoggerHandler(LoggerRuntime loggerRuntime) : BufferLoggerHandler<MongoLoggerHandlerModel, MongoLoggerTargetConfigModel>(loggerRuntime)
    {
        /// <summary>
        /// 处理数据
        /// </summary>
        /// <param name="datas"></param>
        protected override void HandlerData(MongoLoggerHandlerModel[] datas)
        {
            IGrouping<string, MongoLoggerHandlerModel>[] groupDatas = datas.GroupBy(m => m.ConnectionString).ToArray();
            Parallel.ForEach(groupDatas, data =>
            {
                try
                {
                    MongoClient client = new(data.Key);
                    IGrouping<string, MongoLoggerHandlerModel>[] groupDBName = data.GroupBy(m => m.DBName).ToArray();
                    foreach (IGrouping<string, MongoLoggerHandlerModel> dbNameItem in groupDBName)
                    {
                        IMongoDatabase db = client.GetDatabase(dbNameItem.Key);
                        IGrouping<string, MongoLoggerHandlerModel>[] groupCollection = dbNameItem.GroupBy(m => m.CollectionName).ToArray();
                        if (groupCollection.Length <= 0) continue;
                        IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>(groupCollection.First().Key);
                        IEnumerable<BsonDocument> documents = GetBsonDocument(groupCollection);
                        collection.InsertManyAsync(documents).Wait();
                    }
                }
                catch (Exception exception)
                {
                    LoggerLog.LogError($"日志记录到Mongo[{data.Key}]失败：", exception);
                }
            });
        }
        private List<BsonDocument> GetBsonDocument(IGrouping<string, MongoLoggerHandlerModel>[] data)
        {
            List<BsonDocument> result = [];
            foreach (IGrouping<string, MongoLoggerHandlerModel> item in data)
            {
                result.AddRange(GetBsonDocuments(item));
            }
            return result;
        }
        private static IEnumerable<BsonDocument> GetBsonDocuments(IGrouping<string, MongoLoggerHandlerModel> data)
        {
            IEnumerable<Dictionary<string, object>> keyValuePairs = GetKeyValuePairs(data);
            IEnumerable<BsonDocument> bsonElements = keyValuePairs.Select(m => new BsonDocument(m));
            return bsonElements;
        }

        private static IEnumerable<Dictionary<string, object>> GetKeyValuePairs(IGrouping<string, MongoLoggerHandlerModel> data) => data.Select(m => m.ToDictionary());
    }
}
