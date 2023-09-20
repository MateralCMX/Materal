using Materal.Logger.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace Materal.Logger
{
    /// <summary>
    /// 日志配置
    /// </summary>
    public class LoggerConfig
    {
        /// <summary>
        /// 本地配置项
        /// </summary>
        private readonly IConfiguration? _config;
        /// <summary>
        /// 日志配置选项
        /// </summary>
        private readonly LoggerConfigOptions _options;
        /// <summary>
        /// 根键
        /// </summary>
        private const string _rootKey = "MateralLogger";
        /// <summary>
        /// 配置模型类型
        /// </summary>
        public Dictionary<string, Type> ConfigModelTypes { get; } = new();
        /// <summary>
        /// 构造方法
        /// </summary>
        public LoggerConfig(List<LoggerTargetConfigModel> configModels, Action<LoggerConfigOptions>? options, IConfiguration? configuration)
        {
            foreach (LoggerTargetConfigModel item in configModels)
            {
                if (ConfigModelTypes.ContainsKey(item.Type)) continue;
                ConfigModelTypes.Add(item.Type, item.GetType());
            }
            _config = configuration;
            _options = new();
            options?.Invoke(_options);
            _options.Init(this);
        }
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string Application => GetValue(nameof(Application), Assembly.GetEntryAssembly().GetName().Name);
        /// <summary>
        /// 模式
        /// </summary>
        public LoggerModeEnum Mode => GetValueObject(nameof(Mode), LoggerModeEnum.Strict);
        /// <summary>
        /// 默认日志等级组
        /// </summary>
        public Dictionary<string, LogLevel> DefaultLogLevels => GetValueObject("LogLevel", name =>
        {
            static Dictionary<string, LogLevel> GetDefaultLogLevels() => new() { ["Default"] = LogLevel.Information };
            if (_config is null) return GetDefaultLogLevels();
            return _config.GetValueObject<Dictionary<string, LogLevel>>($"Logging:{name}") ?? GetDefaultLogLevels();
        });
        /// <summary>
        /// 缓冲推入间隔(ms)
        /// </summary>
        public int BufferPushInterval => GetValueObject(nameof(BufferPushInterval), 1000, value => value >= 500);
        /// <summary>
        /// 缓冲区数量
        /// </summary>
        public int BufferCount => GetValueObject(nameof(BufferCount), 2000, value => value > 2);
        /// <summary>
        /// 目标配置
        /// </summary>
        public List<LoggerTargetConfigModel> Targets => GetAllTargets<LoggerTargetConfigModel>();
        /// <summary>
        /// 获得所有目标
        /// </summary>
        /// <returns></returns>
        public List<T> GetAllTargets<T>()
            where T : LoggerTargetConfigModel
        {
            List<T> result = new();
            #region 读取配置对象中的配置
            string value = GetValue(nameof(Targets));
            if (value is not null && !string.IsNullOrWhiteSpace(value) && value.IsArrayJson())
            {
                Type? targetType = typeof(T);
                if (targetType == typeof(LoggerTargetConfigModel))
                {
                    targetType = null;
                }
                JObject[] jObjs = value.JsonToDeserializeObject<JObject[]>();
                foreach (JObject jObj in jObjs)
                {
                    string? targetName = jObj.GetValue("Name")?.ToString();
                    if (targetName is null || string.IsNullOrWhiteSpace(targetName)) continue;
                    string? targetTypeName = jObj.GetValue("Type")?.ToString();
                    if (targetTypeName is null || string.IsNullOrWhiteSpace(targetTypeName) || !ConfigModelTypes.ContainsKey(targetTypeName)) continue;
                    Type configType = ConfigModelTypes[targetTypeName];
                    if (targetType is not null && targetType != configType) continue;
                    object? config = jObj.ToObject(configType);
                    if (config is null || config is not T targetConfig) continue;
                    if (!targetConfig.Enable) continue;
                    result.Add(targetConfig);
                }
            }
            #endregion
            #region 读取手动配置中的配置
            if (_options.Targets.Count > 0)
            {
                foreach (LoggerTargetConfigModel item in _options.Targets)
                {
                    if (!item.Enable || item is not T config) continue;
                    result.Add(config);
                }
            }
            #endregion
            return result;
        }
        /// <summary>
        /// 规则配置
        /// </summary>
        public List<LoggerRuleConfigModel> Rules
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
        public LoggerLogLevelConfigModel LoggerLogLevel => GetValueObject(nameof(LoggerLogLevel), name => _options.LoggerLogLevel ?? new LoggerLogLevelConfigModel());
        /// <summary>
        /// 自定义配置
        /// </summary>
        public Dictionary<string, string> CustomConfig { get; private set; } = new();
        /// <summary>
        /// 获取配置项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <param name="validation"></param>
        /// <returns></returns>
        public T GetValueObject<T>(string name, T? defaultValue = default, Func<T, bool>? validation = null)
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
        public string GetValue(string name, string? defaultValue = null, Func<string, bool>? validation = null)
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
        public T GetValueObject<T>(string name, Func<string, T>? defaultValue, Func<T, bool>? validation = null)
            where T : new()
        {
            if (_config is not null)
            {
                T? result = _config.GetValueObject<T>($"{_rootKey}:{name}");
                if (result is not null)
                {
                    if (validation is null)
                    {
                        return result;
                    }
                    else if (validation(result))
                    {
                        return result;
                    }
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
        public string GetValue(string name, Func<string, string>? defaultValue, Func<string, bool>? validation = null)
        {
            string? result = null;
            if (_config != null)
            {
                result = _config.GetValue($"{_rootKey}:{name}");
            }
            if (result is not null && validation is not null && !validation(result))
            {
                result = null;
            }
            if (result is null || string.IsNullOrWhiteSpace(result))
            {
                if (defaultValue is null) return string.Empty;
                result = defaultValue(name);
            }
            return result;
        }
    }
}
