using Materal.Logger.Models;
using Microsoft.Extensions.Logging;

namespace Materal.Logger
{
    /// <summary>
    /// 日志配置选项
    /// </summary>
    public class LoggerConfigOptions
    {
        /// <summary>
        /// 目标组
        /// </summary>
        public List<LoggerTargetConfigModel> Targets { get; } = new();
        /// <summary>
        /// 规则组
        /// </summary>
        public List<LoggerRuleConfigModel> Rules { get; } = new();
        /// <summary>
        /// 日志自身日志等级
        /// </summary>
        public LoggerLogLevelConfigModel? LoggerLogLevel { get; private set; }
        /// <summary>
        /// 添加所有目标规则
        /// </summary>
        private Action<LoggerConfig>? _addAllTargetRule;
        /// <summary>
        /// 自定义配置
        /// </summary>

        private readonly Dictionary<string, string> _customConfig = new();
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="loggerConfig"></param>
        public void Init(LoggerConfig loggerConfig)
        {
            foreach (KeyValuePair<string, string> item in _customConfig)
            {
                if (loggerConfig.CustomConfig.ContainsKey(item.Key))
                {
                    loggerConfig.CustomConfig[item.Key] = item.Value;
                }
                else
                {
                    loggerConfig.CustomConfig.Add(item.Key, item.Value);
                }
            }
            _addAllTargetRule?.Invoke(loggerConfig);
        }
        /// <summary>
        /// 设置日志自身日志等级
        /// </summary>
        /// <param name="minLevel"></param>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        public LoggerConfigOptions SetLoggerLogLevel(LogLevel minLevel, LogLevel maxLevel = LogLevel.Critical)
        {
            LoggerLogLevel = new LoggerLogLevelConfigModel()
            {
                MinLevel = minLevel,
                MaxLevel = maxLevel
            };
            return this;
        }
        /// <summary>
        /// 添加自定义配置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public LoggerConfigOptions AddCustomConfig(string key, string value)
        {
            if (!_customConfig.ContainsKey(key))
            {
                _customConfig.Add(key, value);
            }
            else
            {
                _customConfig[key] = value;
            }
            return this;
        }
        /// <summary>
        /// 添加一个规则
        /// </summary>
        /// <param name="name"></param>
        /// <param name="targets"></param>
        /// <param name="minLevel"></param>
        /// <param name="maxLevel"></param>
        /// <param name="loglevels"></param>
        public LoggerConfigOptions AddRule(string name, IEnumerable<string> targets, LogLevel? minLevel = null, LogLevel? maxLevel = null, Dictionary<string, LogLevel>? loglevels = null)
        {
            if (targets.Count() <= 0) throw new LoggerException("至少需要一个目标");
            minLevel ??= LogLevel.Information;
            maxLevel ??= LogLevel.Critical;
            if (loglevels is null || loglevels.Count <= 0)
            {
                loglevels = new Dictionary<string, LogLevel>() { ["Default"] = minLevel.Value };
            }
            LoggerRuleConfigModel rule = new()
            {
                Name = name,
                Targets = targets.ToList(),
                MinLevel = minLevel.Value,
                MaxLevel = maxLevel.Value,
                LogLevels = loglevels
            };
            Rules.Add(rule);
            return this;
        }
        /// <summary>
        /// 添加一个规则
        /// </summary>
        /// <param name="name"></param>
        /// <param name="targets"></param>
        /// <param name="minLevel"></param>
        /// <param name="maxLevel"></param>
        /// <param name="loglevels"></param>
        public LoggerConfigOptions AddRule(string name, string targets, LogLevel? minLevel = null, LogLevel? maxLevel = null, Dictionary<string, LogLevel>? loglevels = null)
            => AddRule(name, new string[] { targets }, minLevel, maxLevel, loglevels);
        /// <summary>
        /// 添加一个全目标规则
        /// </summary>
        /// <param name="name"></param>
        /// <param name="minLevel"></param>
        /// <param name="maxLevel"></param>
        /// <param name="loglevels"></param>
        public LoggerConfigOptions AddAllTargetRule(string name = "全目标规则", LogLevel? minLevel = null, LogLevel? maxLevel = null, Dictionary<string, LogLevel>? loglevels = null)
        {
            _addAllTargetRule = (loggerConfig) =>
            {
                List<string> targets = new();
                loggerConfig.Targets.ForEach(m => targets.Add(m.Name));
                AddRule(name, targets, minLevel, maxLevel, loglevels);
            };
            return this;
        }
        /// <summary>
        /// 添加一个全目标规则
        /// </summary>
        /// <param name="minLevel"></param>
        /// <param name="maxLevel"></param>
        /// <param name="loglevels"></param>
        public LoggerConfigOptions AddAllTargetRule(LogLevel? minLevel, LogLevel? maxLevel = null, Dictionary<string, LogLevel>? loglevels = null) 
            => AddAllTargetRule("全目标规则", minLevel, maxLevel, loglevels);
        /// <summary>
        /// 添加一个目标
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        /// <exception cref="LoggerException"></exception>
        public LoggerConfigOptions AddTarget(LoggerTargetConfigModel target)
        {
            if (Targets.Any(m => m.Name == target.Name)) throw new LoggerException("已存在相同名称的目标");
            Targets.Add(target);
            return this;
        }
        /// <summary>
        /// 添加一个控制台目标
        /// </summary>
        /// <param name="name"></param>
        /// <param name="format"></param>
        /// <param name="colors"></param>
        public LoggerConfigOptions AddConsoleTarget(string name, string? format = null, Dictionary<LogLevel, ConsoleColor>? colors = null)
        {
            ConsoleLoggerTargetConfigModel target = new()
            {
                Name = name
            };
            if (format is not null && !string.IsNullOrWhiteSpace(format))
            {
                target.Format = format;
            }
            if (colors is not null)
            {
                target.Colors = new LoggerColorsConfigModel(colors);
            }
            AddTarget(target);
            return this;
        }
        /// <summary>
        /// 添加一个文件输出
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="format"></param>
        public LoggerConfigOptions AddFileTarget(string name, string path, string? format = null)
        {
            FileLoggerTargetConfigModel target = new()
            {
                Name = name,
                Path = path
            };
            if (format is not null && !string.IsNullOrWhiteSpace(format))
            {
                target.Format = format;
            }
            AddTarget(target);
            return this;
        }
        /// <summary>
        /// 添加一个Http输出
        /// </summary>
        /// <param name="name"></param>
        /// <param name="url"></param>
        /// <param name="httpMethod"></param>
        public LoggerConfigOptions AddHttpTarget(string name, string url, HttpMethod? httpMethod = null)
        {
            HttpLoggerTargetConfigModel target = new()
            {
                Name = name,
                Url = url
            };
            if (httpMethod is not null)
            {
                target.HttpMethod = httpMethod.Method;
            }
            AddTarget(target);
            return this;
        }
    }
}
