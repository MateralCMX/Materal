using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Materal.Logger
{
    /// <summary>
    /// 日志
    /// </summary>
    public class Log(LogLevel level, EventId eventId, string categoryName, string message, Exception? exception, int threadID, LoggerScope scopeData) : ILog
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
        public LoggerScope ScopeData { get; } = scopeData;
        /// <summary>
        /// 应用域
        /// </summary>
        /// <param name="messages"></param>
        public string ApplyText(string messages)
        {
            string result = messages;
            result = Regex.Replace(result, @"\$\{LogID\}", ID.ToString());
            result = Regex.Replace(result, @"\$\{Time\}", CreateTime.ToString("HH:mm:ss"));
            result = Regex.Replace(result, @"\$\{DateTime\}", CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            result = Regex.Replace(result, @"\$\{RootPath\}", RootPath);
            result = Regex.Replace(result, @"\$\{Date\}", CreateTime.ToString("yyyyMMdd"));
            result = Regex.Replace(result, @"\$\{Year\}", CreateTime.Year.ToString());
            result = Regex.Replace(result, @"\$\{Month\}", CreateTime.Month.ToString());
            result = Regex.Replace(result, @"\$\{Day\}", CreateTime.Day.ToString());
            result = Regex.Replace(result, @"\$\{Hour\}", CreateTime.Hour.ToString());
            result = Regex.Replace(result, @"\$\{Minute\}", CreateTime.Minute.ToString());
            result = Regex.Replace(result, @"\$\{Second\}", CreateTime.Second.ToString());
            result = Regex.Replace(result, @"\$\{Level\}", Level.ToString());
            if (!string.IsNullOrWhiteSpace(CategoryName))
            {
                result = Regex.Replace(result, @"\$\{CategoryName\}", CategoryName);
            }
            string progressID = GetProgressID();
            result = Regex.Replace(result, @"\$\{ProgressID\}", progressID);
            result = Regex.Replace(result, @"\$\{ThreadID\}", ThreadID.ToString());
            result = Regex.Replace(result, @"\$\{MachineName\}", MachineName);
            result = ScopeData.ApplyText(result);
            return result;
        }
    }
}
