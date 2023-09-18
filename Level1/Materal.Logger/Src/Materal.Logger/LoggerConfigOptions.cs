using Microsoft.Extensions.Configuration;

namespace Materal.Logger
{
    /// <summary>
    /// 日志配置选项
    /// </summary>
    public class LoggerConfigOptions
    {
        ///// <summary>
        ///// 目标组
        ///// </summary>
        //public List<LoggerTargetConfigModel> Targets { get; } = new();
        ///// <summary>
        ///// 规则组
        ///// </summary>
        //public List<LoggerRuleConfigModel> Rules { get; } = new();
        ///// <summary>
        ///// 日志自身日志等级
        ///// </summary>
        //public LogLogLevelConfigModel? LogLogLevel { get; private set; }
        ///// <summary>
        ///// 添加所有目标规则
        ///// </summary>
        //private Action? _addAllTargetRule;
        ///// <summary>
        ///// 初始化
        ///// </summary>
        //public void Init()
        //{
        //    _addAllTargetRule?.Invoke();
        //}
        ///// <summary>
        ///// 设置日志自身日志等级
        ///// </summary>
        ///// <param name="minLevel"></param>
        ///// <param name="maxLevel"></param>
        ///// <returns></returns>
        //public LoggerConfigOptions SetLoggerLogLevel(LogLevel minLevel, LogLevel maxLevel = LogLevel.Critical)
        //{
        //    LogLogLevel = new LogLogLevelConfigModel()
        //    {
        //        MinLevel = minLevel,
        //        MaxLevel = maxLevel
        //    };
        //    return this;
        //}
        ///// <summary>
        ///// 添加自定义配置
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="value"></param>
        //public LoggerConfigOptions AddCustomConfig(string key, string value)
        //{
        //    if (!LoggerConfig.CustomConfig.ContainsKey(key))
        //    {
        //        LoggerConfig.CustomConfig.Add(key, value);
        //    }
        //    else
        //    {
        //        LoggerConfig.CustomConfig[key] = value;
        //    }
        //    return this;
        //}
        ///// <summary>
        ///// 添加一个规则
        ///// </summary>
        ///// <param name="targets"></param>
        ///// <param name="minLevel"></param>
        ///// <param name="maxLevel"></param>
        ///// <param name="loglevels"></param>
        //public LoggerConfigOptions AddRule(IEnumerable<string> targets, LogLevel? minLevel = null, LogLevel? maxLevel = null, Dictionary<string, LogLevel>? loglevels = null)
        //{
        //    if (targets.Count() <= 0) throw new LoggerException("至少需要一个目标");
        //    minLevel ??= LogLevel.Information;
        //    maxLevel ??= LogLevel.Critical;
        //    if (loglevels is null || loglevels.Count <= 0)
        //    {
        //        loglevels = new Dictionary<string, LogLevel>() { ["Default"] = minLevel.Value };
        //    }
        //    LoggerRuleConfigModel rule = new()
        //    {
        //        Targets = targets.ToList(),
        //        MinLevel = minLevel.Value,
        //        MaxLevel = maxLevel.Value,
        //        LogLevels = loglevels
        //    };
        //    Rules.Add(rule);
        //    return this;
        //}
        ///// <summary>
        ///// 添加一个规则
        ///// </summary>
        ///// <param name="targets"></param>
        ///// <param name="minLevel"></param>
        ///// <param name="maxLevel"></param>
        ///// <param name="loglevels"></param>
        //public LoggerConfigOptions AddRule(string targets, LogLevel? minLevel = null, LogLevel? maxLevel = null, Dictionary<string, LogLevel>? loglevels = null)
        //    => AddRule(new string[] { targets }, minLevel, maxLevel, loglevels);
        ///// <summary>
        ///// 添加一个全目标规则
        ///// </summary>
        ///// <param name="minLevel"></param>
        ///// <param name="maxLevel"></param>
        ///// <param name="loglevels"></param>
        //public LoggerConfigOptions AddAllTargetRule(LogLevel? minLevel = null, LogLevel? maxLevel = null, Dictionary<string, LogLevel>? loglevels = null)
        //{
        //    _addAllTargetRule = () =>
        //    {
        //        List<string> targets = new();
        //        LoggerConfig.Targets.ForEach(m => targets.Add(m.Name));
        //        AddRule(targets, minLevel, maxLevel, loglevels);
        //    };
        //    return this;
        //}
        ///// <summary>
        ///// 添加一个目标
        ///// </summary>
        ///// <param name="target"></param>
        ///// <returns></returns>
        ///// <exception cref="LoggerException"></exception>
        //public LoggerConfigOptions AddTarget(LoggerTargetConfigModel target)
        //{
        //    if (Targets.Any(m => m.Name == target.Name)) throw new LoggerException("已存在相同名称的目标");
        //    Targets.Add(target);
        //    return this;
        //}
        ///// <summary>
        ///// 添加一个控制台目标
        ///// </summary>
        ///// <param name="name"></param>
        ///// <param name="format"></param>
        ///// <param name="colors"></param>
        //public LoggerConfigOptions AddConsoleTarget(string name, string? format = null, Dictionary<LogLevel, ConsoleColor>? colors = null)
        //{
        //    ConsoleLoggerTargetConfigModel target = new()
        //    {
        //        Name = name
        //    };
        //    if (format is not null && !string.IsNullOrWhiteSpace(format))
        //    {
        //        target.Format = format;
        //    }
        //    if (colors is not null)
        //    {
        //        target.Colors = new LoggerColorsConfigModel(colors);
        //    }
        //    AddTarget(target);
        //    return this;
        //}
        ///// <summary>
        ///// 添加一个文件输出
        ///// </summary>
        ///// <param name="name"></param>
        ///// <param name="path"></param>
        ///// <param name="format"></param>
        //public LoggerConfigOptions AddFileTarget(string name, string path, string? format = null)
        //{
        //    FileLoggerTargetConfigModel target = new()
        //    {
        //        Name = name,
        //        Path = path
        //    };
        //    if (format is not null && !string.IsNullOrWhiteSpace(format))
        //    {
        //        target.Format = format;
        //    }
        //    AddTarget(target);
        //    return this;
        //}
        ///// <summary>
        ///// 添加一个Http输出
        ///// </summary>
        ///// <param name="name"></param>
        ///// <param name="url"></param>
        ///// <param name="httpMethod"></param>
        ///// <param name="format"></param>
        //public LoggerConfigOptions AddHttpTarget(string name, string url, HttpMethod? httpMethod = null, string? format = null)
        //{
        //    HttpLoggerTargetConfigModel target = new()
        //    {
        //        Name = name,
        //        Url = url
        //    };
        //    if (httpMethod is not null)
        //    {
        //        target.HttpMethod = httpMethod.Method;
        //    }
        //    if (format is not null && !string.IsNullOrWhiteSpace(format))
        //    {
        //        target.Format = format;
        //    }
        //    AddTarget(target);
        //    return this;
        //}
    }
}
