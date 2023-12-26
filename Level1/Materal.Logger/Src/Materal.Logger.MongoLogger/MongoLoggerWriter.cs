using MongoDB.Bson;
using MongoDB.Driver;

namespace Materal.Logger.MongoLogger
{
    /// <summary>
    /// Mongo日志写入器
    /// </summary>
    public class MongoLoggerWriter(MongoLoggerTargetConfig targetConfig) : BatchLoggerWriter<MongoLoggerWriterModel, MongoLoggerTargetConfig>(targetConfig), ILoggerWriter
    {
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public override async Task WriteBatchLoggerAsync(MongoLoggerWriterModel[] models)
        {
            IGrouping<string, MongoLoggerWriterModel>[] groupDatas = models.GroupBy(m => m.ConnectionString).ToArray();
            Parallel.ForEach(groupDatas, data =>
            {
                try
                {
                    MongoClient client = new(data.Key);
                    IGrouping<string, MongoLoggerWriterModel>[] groupDBName = data.GroupBy(m => m.DBName).ToArray();
                    foreach (IGrouping<string, MongoLoggerWriterModel> dbNameItem in groupDBName)
                    {
                        IMongoDatabase db = client.GetDatabase(dbNameItem.Key);
                        IGrouping<string, MongoLoggerWriterModel>[] groupCollection = dbNameItem.GroupBy(m => m.CollectionName).ToArray();
                        if (groupCollection.Length <= 0) continue;
                        IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>(groupCollection.First().Key);
                        IEnumerable<BsonDocument> documents = GetBsonDocument(groupCollection);
                        collection.InsertManyAsync(documents).Wait();
                    }
                }
                catch (Exception exception)
                {
                    LoggerHost.LoggerLog?.LogError($"日志记录到Mongo[{data.Key}]失败：", exception);
                }
            });
            await Task.CompletedTask;
        }
        private List<BsonDocument> GetBsonDocument(IGrouping<string, MongoLoggerWriterModel>[] data)
        {
            List<BsonDocument> result = [];
            foreach (IGrouping<string, MongoLoggerWriterModel> item in data)
            {
                result.AddRange(GetBsonDocuments(item));
            }
            return result;
        }
        private static IEnumerable<BsonDocument> GetBsonDocuments(IGrouping<string, MongoLoggerWriterModel> data)
        {
            IEnumerable<Dictionary<string, object>> keyValuePairs = GetKeyValuePairs(data);
            IEnumerable<BsonDocument> bsonElements = keyValuePairs.Select(m => new BsonDocument(m));
            return bsonElements;
        }
        private static IEnumerable<Dictionary<string, object>> GetKeyValuePairs(IGrouping<string, MongoLoggerWriterModel> data) => data.Select(m => m.ToDictionary());
    }
}
