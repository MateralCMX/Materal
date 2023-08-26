using Materal.Logger.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Materal.Logger
{
    /// <summary>
    /// 日志配置
    /// </summary>
    public static class LoggerConfig
    {
        /// <summary>
        /// 本地配置项
        /// </summary>
        private static IConfiguration? _config;
        /// <summary>
        /// 根键
        /// </summary>
        private const string _rootKey = "MateralLogger";
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="configuration"></param>
        public static void Init(IConfiguration? configuration)
        {
            _config = configuration;
        }
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public static string Application => GetValue(nameof(Application), Assembly.GetEntryAssembly().GetName().Name);
        /// <summary>
        /// 模式
        /// </summary>
        public static LoggerModeEnum Mode => GetValueObject(nameof(Mode), LoggerModeEnum.Strict);
        /// <summary>
        /// 缓冲推入间隔(ms)
        /// </summary>
        public static int BufferPushInterval => GetValueObject(nameof(BufferPushInterval), 1000, value => value >= 500);
        /// <summary>
        /// 缓冲区数量
        /// </summary>
        public static int BufferCount => GetValueObject(nameof(BufferCount), 2000, value => value > 2);
        /// <summary>
        /// 默认日志等级组
        /// </summary>
        public static Dictionary<string, LogLevel> DefaultLogLevels => GetValueObject("LogLevel", name =>
        {
            static Dictionary<string, LogLevel> GetDefaultLogLevels() => new() { ["Default"] = LogLevel.Information };
            if (_config is null) return GetDefaultLogLevels();
            return _config.GetValueObject<Dictionary<string, LogLevel>>($"Logging:{name}") ?? GetDefaultLogLevels();
        });
        /// <summary>
        /// 目标配置
        /// </summary>
        public static List<LoggerTargetConfigModel> Targets => GetValueObject(nameof(Targets), name => new List<LoggerTargetConfigModel>() { new LoggerTargetConfigModel() { Name = "DefaultConsole" } });
        /// <summary>
        /// 规则配置
        /// </summary>
        public static List<LoggerRuleConfigModel> Rules => GetValueObject(nameof(Rules), name =>
        {
            List<LoggerRuleConfigModel> result = new();
            LoggerRuleConfigModel loggerRuleConfig = new();
            foreach (LoggerTargetConfigModel item in Targets)
            {
                loggerRuleConfig.Targets.Add(item.Name);
            }
            result.Add(loggerRuleConfig);
            return result;
        });
        /// <summary>
        /// 日志日志等级配置
        /// </summary>
        public static LogLogLevelConfigModel LogLogLevel => GetValueObject(nameof(LogLogLevel), name => new LogLogLevelConfigModel());
        /// <summary>
        /// 自定义数据
        /// </summary>

        public static Dictionary<string, string> CustomData { get; private set; } = new();
        /// <summary>
        /// 自定义配置
        /// </summary>

        public static Dictionary<string, string> CustomConfig { get; private set; } = new();
        /// <summary>
        /// 获取配置项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <param name="validation"></param>
        /// <returns></returns>
        public static T GetValueObject<T>(string name, T? defaultValue = default, Func<T, bool>? validation = null)
            where T : new()
        {
            if (defaultValue is null) return GetValueObject(name, (m) => new T(), validation);
            return GetValueObject(name, (m) => defaultValue, validation);
        }
        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <param name="validation"></param>
        /// <returns></returns>
        public static string GetValue(string name, string? defaultValue = null, Func<string, bool>? validation = null)
        {
            if (defaultValue is null) return GetValue(name, (m) => string.Empty, validation);
            return GetValue(name, (m) => defaultValue, validation);
        }
        /// <summary>
        /// 获取配置项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <param name="validation"></param>
        /// <returns></returns>
        public static T GetValueObject<T>(string name, Func<string, T>? defaultValue, Func<T, bool>? validation = null)
            where T : new()
        {
            T? result = default;
            if (_config is not null)
            {
                result = _config.GetValueObject<T>($"{_rootKey}:{name}");
            }
            if (result is not null)
            {
                if(validation is null)
                {
                    return result;
                }
                else if(validation(result))
                {
                    return result;
                }
            }
            if (defaultValue is null) return new T();
            return defaultValue(name);
        }
        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <param name="validation"></param>
        /// <returns></returns>
        public static string GetValue(string name, Func<string, string>? defaultValue, Func<string, bool>? validation = null)
        {
            string? result = null;
            if (_config != null)
            {
                result = _config.GetValue($"{_rootKey}:{name}");
            }
            if(result is not null && validation is not null && !validation(result))
            {
                result = null;
            }
            if (result is null || string.IsNullOrWhiteSpace(result))
            {
                if (defaultValue is null) return string.Empty;
                result = defaultValue(name);
            }
            foreach (KeyValuePair<string, string> item in CustomConfig)
            {
                result = Regex.Replace(result, $@"\$\{{{item.Key}\}}", item.Value);
            }
            foreach (KeyValuePair<string, string> item in CustomData)
            {
                result = Regex.Replace(result, $@"\$\{{{item.Key}\}}", item.Value);
            }
            return result;
        }
    }
}
