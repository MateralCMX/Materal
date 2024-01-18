using Materal.Utils.MongoDB;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Materal.Logger.MongoLogger
{
    /// <summary>
    /// Mongo日志写入器
    /// </summary>
    public class MongoLoggerWriter : BatchLoggerWriter<MongoLoggerWriterModel, MongoLoggerTargetConfig>, ILoggerWriter
    {
        private readonly IMongoRepository _mongoRepository;
        /// <summary>
        /// 构造方法
        /// </summary>
        public MongoLoggerWriter(MongoLoggerTargetConfig targetConfig, IMongoRepository mongoRepository) : base(targetConfig)
        {
            _mongoRepository = mongoRepository;
            _mongoRepository.CollectionOptions = new() { TimeSeriesOptions = new TimeSeriesOptions("CreateTime") };
        }
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public override async Task WriteBatchLoggerAsync(MongoLoggerWriterModel[] models)
        {
            try
            {
                IGrouping<string, MongoLoggerWriterModel>[] groupDatas = models.GroupBy(m => m.ConnectionString).ToArray();
                foreach (IGrouping<string, MongoLoggerWriterModel> data in groupDatas)
                {
                    try
                    {
                        _mongoRepository.ConnectionString = data.Key;
                        IGrouping<string, MongoLoggerWriterModel>[] groupDBName = data.GroupBy(m => m.DBName).ToArray();
                        foreach (IGrouping<string, MongoLoggerWriterModel> dbNameItem in groupDBName)
                        {
                            _mongoRepository.DatabaseName = dbNameItem.Key;
                            IGrouping<string, MongoLoggerWriterModel>[] groupCollection = dbNameItem.GroupBy(m => m.CollectionName).ToArray();
                            if (groupCollection.Length <= 0) continue;
                            string collectionName = groupCollection.First().Key;
                            _mongoRepository.CollectionName = collectionName;
                            IEnumerable<BsonDocument> documents = GetBsonDocument(groupCollection);
                            await _mongoRepository.InsertAsync(documents);
                        }
                    }
                    catch (Exception exception)
                    {
                        LoggerHost.LoggerLog?.LogError($"日志记录到Mongo[{data.Key}]失败：", exception);
                    }
                }
                await Task.CompletedTask;
            }
            catch (Exception exception)
            {
                LoggerHost.LoggerLog?.LogError($"日志记录到Mongo失败：", exception);
            }
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
