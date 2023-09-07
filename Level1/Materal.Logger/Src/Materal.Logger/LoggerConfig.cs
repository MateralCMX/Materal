using Materal.Logger.LoggerHandlers;
using Materal.Logger.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
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
        private static readonly LoggerConfigOptions _options = new();
        /// <summary>
        /// 根键
        /// </summary>
        private const string _rootKey = "MateralLogger";
        /// <summary>
        /// 配置模型类型
        /// </summary>
        private static Dictionary<string, Type> ConfigModelTypes { get; set; } = new();
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="configModels"></param>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public static void Init(List<LoggerTargetConfigModel> configModels, Action<LoggerConfigOptions>? options, IConfiguration? configuration)
        {
            foreach (LoggerTargetConfigModel item in configModels)
            {
                if (ConfigModelTypes.ContainsKey(item.Type)) continue;
                ILoggerHandler loggerHandler = item.GetLoggerHandler();
                Logger.Handlers.Add(loggerHandler);
                ConfigModelTypes.Add(item.Type, item.GetType());
            }
            _config = configuration;
            options?.Invoke(_options);
            _options.Init();
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
        public static List<LoggerTargetConfigModel> Targets => GetAllTargets<LoggerTargetConfigModel>();
        /// <summary>
        /// 获得所有目标
        /// </summary>
        /// <returns></returns>
        public static List<T> GetAllTargets<T>(bool onlyEnable = true)
            where T : LoggerTargetConfigModel
        {
            List<T> result = new();
            #region 读取配置对象中的配置
            string value = GetValue(nameof(Targets));
            Type? targetType = typeof(T);
            if(targetType == typeof(LoggerTargetConfigModel))
            {
                targetType = null;
            }
            JObject[] jObjs = value.JsonToDeserializeObject<JObject[]>();
            foreach (JObject jObj in jObjs)
            {
                string? targetName = jObj.GetValue("Name")?.ToString();
                if (targetName is null || string.IsNullOrWhiteSpace(targetName)) continue;
                try
                {
                    string? targetTypeName = jObj.GetValue("Type")?.ToString();
                    if (targetTypeName is null || string.IsNullOrWhiteSpace(targetTypeName) || !ConfigModelTypes.ContainsKey(targetTypeName)) continue;
                    Type configType = ConfigModelTypes[targetTypeName];
                    if(targetType is not null && targetType != configType) continue;
                    object?  config = jObj.ToObject(configType);
                    if (config is null || config is not T targetConfig) continue;
                    if (onlyEnable && !targetConfig.Enable) continue;
                    result.Add(targetConfig);
                }
                catch (Exception ex)
                {
                    LoggerLog.LogWarning($"获取目标配置[{targetName}]失败：\r\n{ex.GetErrorMessage()}");
                }
            }
            #endregion
            #region 读取手动配置中的配置
            if(_options.Targets.Count > 0)
            {
                foreach (LoggerTargetConfigModel item in _options.Targets)
                {
                    if (item is not T config) continue;
                    result.Add(config);
                }
            }
            #endregion
            return result;
        }
        /// <summary>
        /// 规则配置
        /// </summary>
        public static List<LoggerRuleConfigModel> Rules
        {
            get
            {
                List<LoggerRuleConfigModel> result = GetValueObject<List<LoggerRuleConfigModel>>(nameof(Rules));
                if (_options.Rules.Count > 0)
                {
                    result.AddRange(_options.Rules);
                }
                return result;
            }
        }
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
