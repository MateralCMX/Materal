using System.Diagnostics;

namespace Materal.Logger
{
    /// <summary>
    /// 日志
    /// </summary>
    public class Log(LogLevel level, EventId eventId, string categoryName, string message, Exception? exception, int threadID, LoggerScope scope) : ILog
    {
        /// <summary>
        /// 获得进程ID
        /// </summary>
        /// <returns></returns>
        public static string GetProgressID()
        {
            Process processes = Process.GetCurrentProcess();
            return processes.Id.ToString();
        }
        /// <summary>
        /// 计算机名称
        /// </summary>
        public static string MachineName => Environment.MachineName;
        private static string? _rootPath;
        /// <summary>
        /// 根路径
        /// </summary>
        public static string RootPath
        {
            get
            {
                if (_rootPath is null || string.IsNullOrWhiteSpace(_rootPath))
                {
                    _rootPath = typeof(Logger).Assembly.GetDirectoryPath();
                    if (_rootPath.EndsWith('\\') || _rootPath.EndsWith('/'))
                    {
                        _rootPath = _rootPath[..^1];
                    }
                }
                return _rootPath;
            }
        }
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; } = Guid.NewGuid();
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; } = DateTime.UtcNow;
        /// <summary>
        /// 日志等级
        /// </summary>
        public LogLevel Level { get; } = level;
        /// <summary>
        /// 事件ID
        /// </summary>
        public EventId EventID { get; } = eventId;
        /// <summary>
        /// 线程ID
        /// </summary>
        public int ThreadID { get; } = threadID;
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; } = categoryName;
        /// <summary>
        /// 状态
        /// </summary>
        public string Message { get; set; } = message;
        /// <summary>
        /// 异常
        /// </summary>
        public Exception? Exception { get; } = exception;
        /// <summary>
        /// 作用域提供者
        /// </summary>
        public LoggerScope Scope { get; } = scope;
        /// <summary>
        /// 应用域
        /// </summary>
        /// <param name="messages"></param>
        /// <param name="options"></param>
        public string ApplyText(string messages, LoggerOptions options)
        {
            string result = messages;
            Dictionary<string, object?> data = [];
            AddData(data, "Application", options.Application);
            AddData(data, "LogID", ID);
            AddData(data, "Time", CreateTime.ToString("HH:mm:ss"));
            AddData(data, "DateTime", CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            AddData(data, "RootPath", RootPath);
            AddData(data, "Date", CreateTime.ToString("yyyyMMdd"));
            AddData(data, "Year", CreateTime.Year);
            AddData(data, "Month", CreateTime.Month);
            AddData(data, "Day", CreateTime.Day);
            AddData(data, "Hour", CreateTime.Hour);
            AddData(data, "Minute", CreateTime.Minute);
            AddData(data, "Second", CreateTime.Second);
            AddData(data, "Level", Level);
            if (!string.IsNullOrWhiteSpace(CategoryName))
            {
                AddData(data, "CategoryName", CategoryName);
            }
            string progressID = GetProgressID();
            AddData(data, "ProgressID", progressID);
            AddData(data, "ThreadID", ThreadID);
            AddData(data, "MachineName", MachineName);
            data = data.Concat(options.CustomData).ToDictionary(k => k.Key, v => v.Value);
            AddData(data, "Scope", Scope.ScopeName);
            data = data.Concat(Scope.ScopeData).ToDictionary(k => k.Key, v => v.Value);
            result = LoggerHelper.ApplyText(result, data);
            return result;
        }
        private static void AddData(Dictionary<string, object?> data, string key, object? value)
        {
            if (!data.TryAdd(key, value))
            {
                data[key] = value;
            }
        }
    }
}
