using Materal.Logger.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

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
        /// 获取配置项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetValueObject<T>(string name, T? defaultValue = default)
            where T : new()
        {
            if (defaultValue is null) return GetValueObject(name, (m) => new T());
            return GetValueObject(name, (m) => defaultValue);
        }
        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetValue(string name, string? defaultValue = null)
        {
            if (defaultValue is null) return GetValue(name, (m) => string.Empty);
            return GetValue(name, (m) => defaultValue);
        }
        /// <summary>
        /// 获取配置项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetValueObject<T>(string name, Func<string, T>? defaultValue)
            where T : new()
        {
            T? result = default;
            if (_config is not null)
            {
                result = _config.GetValueObject<T>($"{_rootKey}:{name}");
            }
            if (result is not null) return result;
            if (defaultValue is null) return new T();
            return defaultValue(name);
        }
        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetValue(string name, Func<string, string>? defaultValue)
        {
            string? result = null;
            if (_config != null)
            {
                result = _config.GetValue($"{_rootKey}:{name}");
            }
            if (result is not null && !string.IsNullOrWhiteSpace(result)) return result;
            if (defaultValue is null) return string.Empty;
            return defaultValue(name);
        }
    }
}
