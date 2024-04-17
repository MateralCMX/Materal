using Microsoft.Extensions.Configuration;
using System.Dynamic;

namespace Materal.Logger
{
    /// <summary>
    /// 自定义日志配置
    /// </summary>
    public class LoggerOptions
    {
        /// <summary>
        /// 日志目标选项类型
        /// </summary>
        public static ICollection<Type> LoggerTargetOptionTypes { get; set; } = [];
        /// <summary>
        /// 服务提供者
        /// </summary>
        public IConfiguration? Configuration { get; set; }
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string Application { get; set; } = "LoggerApplication";
        /// <summary>
        /// 最小等级
        /// </summary>
        public LogLevel MinLevel { get; set; } = Microsoft.Extensions.Logging.LogLevel.Trace;
        /// <summary>
        /// 最大等级
        /// </summary>
        public LogLevel MaxLevel { get; set; } = Microsoft.Extensions.Logging.LogLevel.Critical;
        /// <summary>
        /// 最小日志主机日志等级
        /// </summary>
        public LogLevel MinLoggerInfoLevel { get; set; } = Microsoft.Extensions.Logging.LogLevel.Information;
        /// <summary>
        /// 最大日志主机日志等级
        /// </summary>
        public LogLevel MaxLoggerInfoLevel { get; set; } = Microsoft.Extensions.Logging.LogLevel.Critical;
        /// <summary>
        /// 批量处理大小
        /// </summary>
        public int BatchSize { get; set; } = 2000;
        /// <summary>
        /// 批量处理推送间隔(秒)
        /// </summary>
        public int BatchPushInterval { get; set; } = 2;
        /// <summary>
        /// 作用域组
        /// </summary>
        public Dictionary<string, LogLevel>? Scopes { get; set; }
        /// <summary>
        /// 日志等级组
        /// </summary>
        public Dictionary<string, LogLevel>? LogLevel { get; set; }
        /// <summary>
        /// 自定义数据
        /// </summary>
        public Dictionary<string, object?> CustomData { get; set; } = [];
        /// <summary>
        /// 目标选项
        /// </summary>
        public List<LoggerTargetOptions> Targets { get; } = [];
        /// <summary>
        /// 代码配置目标名称
        /// </summary>
        public List<string> CodeConfigTargetNames { get; } = [];
        /// <summary>
        /// 规则
        /// </summary>
        public List<LoggerRuleOptions> Rules { get; set; } = [];
        /// <summary>
        /// 默认日志等级组
        /// </summary>
        public static Dictionary<string, LogLevel>? DefaultLogLevel { get; set; }
        private readonly SemaphoreSlim _semaphore = new(0, 1);
        /// <summary>
        /// 构造方法
        /// </summary>
        public LoggerOptions() => _semaphore.Release();
        /// <summary>
        /// 更新目标选项
        /// </summary>
        public void UpdateTargetOptions()
        {
            if (Configuration is null) return;
            try
            {
                _semaphore.Wait();
                DefaultLogLevel = Configuration.GetConfigItem<Dictionary<string, LogLevel>>("Logging:MateralLogger:LogLevel");
                Rules = Rules.Distinct((m, n) => m.Name == n.Name).ToList();
                List<LoggerTargetOptions> configFileTargets = Targets.Where(m => !CodeConfigTargetNames.Contains(m.Name)).ToList();
                foreach (LoggerTargetOptions configFileTarget in configFileTargets)
                {
                    Targets.Remove(configFileTarget);
                }
                string? targetConfigs = Configuration.GetConfigItemToString("Logging:MateralLogger:Targets");
                if (targetConfigs is null || string.IsNullOrWhiteSpace(targetConfigs)) return;
                List<ExpandoObject> targets = targetConfigs.JsonToObject<List<ExpandoObject>>();
                foreach (ExpandoObject target in targets)
                {
                    string? typeName = target.GetObjectValue<string>(nameof(LoggerTargetOptions.Type));
                    if (string.IsNullOrWhiteSpace(typeName)) continue;
                    string targetOptionsTypeName = $"{typeName}LoggerTargetOptions";
                    Type? type = LoggerTargetOptionTypes.FirstOrDefault(m => m.Name == targetOptionsTypeName);
                    if (type is null) continue;
                    object targetOptionsObj = target.ToJson().JsonToObject(type);
                    if (targetOptionsObj is not LoggerTargetOptions targetOptions || Targets.Any(m => m.Name == targetOptions.Name)) continue;
                    Targets.Add(targetOptions);
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
