using Materal.Logger.BatchLogger;
using Materal.Utils.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Materal.Logger.MongoLogger
{
    /// <summary>
    /// Mongo日志写入器
    /// </summary>
    public class MongoLoggerWriter : BatchLoggerWriter<MongoLoggerTargetOptions>
    {
        private readonly IMongoRepository _mongoRepository;
        private readonly ILoggerInfo _loggerInfo;
        /// <summary>
        /// 构造方法
        /// </summary>
        public MongoLoggerWriter(IOptionsMonitor<LoggerOptions> options, IMongoRepository mongoRepository, ILoggerInfo loggerInfo) : base(options, loggerInfo)
        {
            _loggerInfo = loggerInfo;
            _mongoRepository = mongoRepository;
            _mongoRepository.CollectionOptions = new() { TimeSeriesOptions = new TimeSeriesOptions("CreateTime") };
        }
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="batchLogs"></param>
        /// <returns></returns>
        public override async Task LogAsync(BatchLog<MongoLoggerTargetOptions>[] batchLogs)
        {
            MongoLog[] mongoLogs = batchLogs.Select(m => new MongoLog(m, Options.CurrentValue)).ToArray();
            try
            {
                IGrouping<string, MongoLog>[] groupDatas = mongoLogs.GroupBy(m => m.ConnectionString).ToArray();
                foreach (IGrouping<string, MongoLog> data in groupDatas)
                {
                    try
                    {
                        _mongoRepository.ConnectionString = data.Key;
                        IGrouping<string, MongoLog>[] groupDBName = data.GroupBy(m => m.DBName).ToArray();
                        foreach (IGrouping<string, MongoLog> dbNameItem in groupDBName)
                        {
                            _mongoRepository.DatabaseName = dbNameItem.Key;
                            IGrouping<string, MongoLog>[] groupCollection = dbNameItem.GroupBy(m => m.CollectionName).ToArray();
                            foreach (IGrouping<string, MongoLog> collection in groupCollection)
                            {
                                _mongoRepository.CollectionName = collection.Key;
                                IEnumerable<BsonDocument> documents = GetBsonDocuments(collection);
                                await _mongoRepository.InsertAsync(documents);
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        _loggerInfo.LogError($"日志记录到Mongo[{data.Key}]失败：", exception);
                    }
                }
            }
            catch (Exception exception)
            {
                _loggerInfo.LogError($"日志记录到Mongo失败：", exception);
            }
        }
        private static IEnumerable<BsonDocument> GetBsonDocuments(IGrouping<string, MongoLog> data)
        {
            IEnumerable<Dictionary<string, object>> keyValuePairs = GetKeyValuePairs(data);
            IEnumerable<BsonDocument> bsonElements = keyValuePairs.Select(m => new BsonDocument(m));
            return bsonElements;
        }
        private static IEnumerable<Dictionary<string, object>> GetKeyValuePairs(IGrouping<string, MongoLog> data) => data.Select(m => m.Data);
        private class MongoLog(BatchLog<MongoLoggerTargetOptions> batchLog, LoggerOptions options)
        {
            /// <summary>
            /// 链接字符串
            /// </summary>
            public string ConnectionString { get; } = batchLog.Log.ApplyText(batchLog.TargetOptions.ConnectionString, options);
            /// <summary>
            /// 数据库名
            /// </summary>
            public string DBName { get; } = batchLog.Log.ApplyText(batchLog.TargetOptions.DBName, options);
            /// <summary>
            /// 集合名
            /// </summary>
            public string CollectionName { get; } = batchLog.Log.ApplyText(batchLog.TargetOptions.CollectionName, options);
            public Dictionary<string, object> Data { get; } = SetData(batchLog);
            /// <summary>
            /// 获取键值对
            /// </summary>
            /// <returns></returns>
            private static Dictionary<string, object> SetData(BatchLog<MongoLoggerTargetOptions> batchLog)
            {
                Dictionary<string, object> result = new()
                {
                    [nameof(Log.ID)] = batchLog.Log.ID,
                    [nameof(LoggerOptions.Application)] = batchLog.Log.Application,
                    [nameof(Log.Level)] = batchLog.Log.Level,
                    ["LevelText"] = batchLog.Log.Level.GetDescription(),
                    [nameof(Log.CreateTime)] = batchLog.Log.CreateTime,
                    ["ProgressID"] = batchLog.Log.ProgressID,
                    [nameof(Log.ThreadID)] = batchLog.Log.ThreadID,
                    [nameof(Log.Message)] = batchLog.Log.Message,
                    ["Scope"] = batchLog.Log.ScopeName,
                    [nameof(Log.MachineName)] = batchLog.Log.MachineName
                };
                SetNotNullKeyVlueToDictionary(result, nameof(Log.Exception), batchLog.Log.Exception?.GetErrorMessage());
                SetNotNullKeyVlueToDictionary(result, nameof(Log.CategoryName), batchLog.Log.CategoryName);
                if (batchLog.Log.ScopeData is not null && batchLog.Log.ScopeData.Count > 0)
                {
                    foreach (KeyValuePair<string, object?> data in batchLog.Log.ScopeData)
                    {
                        SetNotNullKeyVlueToDictionary(result, data.Key, data.Value);
                    }
                }
                return result;
            }
            /// <summary>
            /// 设置非空键值对
            /// </summary>
            /// <param name="dic"></param>
            /// <param name="key"></param>
            /// <param name="value"></param>
            private static void SetNotNullKeyVlueToDictionary(Dictionary<string, object> dic, string key, object? value)
            {
                if (dic.ContainsKey(key) || value is null || value.IsNullOrWhiteSpaceString()) return;
                object? trueValue = value.ToExpandoObject();
                if (trueValue is null) return;
                dic.Add(key, trueValue);
            }
        }
    }
}
