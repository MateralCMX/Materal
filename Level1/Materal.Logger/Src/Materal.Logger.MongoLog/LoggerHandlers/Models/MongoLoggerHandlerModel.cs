using Materal.Logger.LoggerHandlers;
using Materal.Logger.LoggerHandlers.Models;
using Materal.Logger.Models;

namespace Materal.Logger.MongoLog.LoggerHandlers.Models
{
    /// <summary>
    /// Mongo日志处理器模型
    /// </summary>
    public class MongoLoggerHandlerModel : BufferLoggerHandlerModel
    {
        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string DBName { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string CollectionName { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="target"></param>
        /// <param name="model"></param>
        /// <param name="loggerConfig"></param>
        public MongoLoggerHandlerModel(LoggerRuleConfigModel rule, MongoLoggerTargetConfigModel target, LoggerHandlerModel model, LoggerConfig loggerConfig) : base(rule, target, model)
        {
            ConnectionString = LoggerHandlerHelper.FormatPath(loggerConfig, target.ConnectionString, model);
            DBName = LoggerHandlerHelper.FormatText(loggerConfig, target.DBName, model);
            CollectionName = LoggerHandlerHelper.FormatText(loggerConfig, target.CollectionName, model);
            Message = LoggerHandlerHelper.FormatMessage(loggerConfig, Message, model);
        }
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
                [nameof(ThreadID)] = ThreadID,
                [nameof(Message)] = Message
            };
            SetNotNullKeyVlueToDictionary(result, nameof(Exception), Exception);
            SetNotNullKeyVlueToDictionary(result, nameof(CategoryName), CategoryName);
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
            if(dic.ContainsKey(key) || value is null || value.IsNullOrWhiteSpaceString()) return;
            dic.Add(key, value);
        }
    }
}
