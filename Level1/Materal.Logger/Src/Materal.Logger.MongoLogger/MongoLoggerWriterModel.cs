namespace Materal.Logger.MongoLogger
{
    /// <summary>
    /// Mongo日志写入器模型
    /// </summary>
    public class MongoLoggerWriterModel(LoggerWriterModel model, MongoLoggerTargetConfig target) : BatchLoggerWriterModel(model)
    {
        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString { get; set; } = LoggerWriterHelper.FormatPath(target.ConnectionString, model);
        /// <summary>
        /// 数据库名
        /// </summary>
        public string DBName { get; set; } = LoggerWriterHelper.FormatPath(target.DBName, model);
        /// <summary>
        /// 集合名
        /// </summary>
        public string CollectionName { get; set; } = LoggerWriterHelper.FormatPath(target.CollectionName, model);
        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> result = new()
            {
                [nameof(ID)] = ID,
                [nameof(LogLevel)] = LogLevel,
                ["LogLevelText"] = LogLevel.GetDescription(),
                [nameof(CreateTime)] = CreateTime,
                ["ProgressID"] = LoggerWriterHelper.GetProgressID(),
                [nameof(ThreadID)] = ThreadID,
                [nameof(Message)] = LoggerWriterHelper.FormatMessage(Message, this),
                [nameof(Scope)] = (Scope == null) ? "PublicScope" : Scope.ScopeName,
                [nameof(LoggerWriterHelper.MachineName)] = LoggerWriterHelper.MachineName
            };
            SetNotNullKeyVlueToDictionary(result, nameof(Exception), Exception?.GetErrorMessage());
            SetNotNullKeyVlueToDictionary(result, nameof(CategoryName), CategoryName);
            if (Scope is not null && Scope.IsAdvancedScope && Scope.AdvancedScope is not null)
            {
                foreach (KeyValuePair<string, object?> data in Scope.AdvancedScope.ScopeData)
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
