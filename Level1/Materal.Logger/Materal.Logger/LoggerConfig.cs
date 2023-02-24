using Materal.Logger.Models;
using Microsoft.Extensions.Configuration;
using System.Collections;
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
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public static void Init(LoggerConfigOptions options, IConfiguration? configuration)
        {
            _config = configuration;
            _application = null;
            _targetsConfig = null;
            _rulesConfig = null;
            _serverConfig = null;
            options.Apply(TargetsConfig, RulesConfig);
        }

        private static string? _application;
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public static string Application
        {
            get
            {
                if (_application != null && !string.IsNullOrWhiteSpace(_application)) return _application;
                _application = GetValue("Application", _rootKey);
                _application = FormatConfig(_application);
                return _application;
            }
            set
            {
                _application = value;
                _application = FormatConfig(_application);
            }
        }
        private static List<LoggerTargetConfigModel>? _targetsConfig;
        /// <summary>
        /// 目标配置
        /// </summary>
        public static List<LoggerTargetConfigModel> TargetsConfig
        {
            get
            {
                if (_targetsConfig != null) return _targetsConfig;
                _targetsConfig = GetValueObject<List<LoggerTargetConfigModel>>("Targets");
                _targetsConfig = FormatListConfig(_targetsConfig);
                return _targetsConfig;
            }
        }
        private static List<LoggerRuleConfigModel>? _rulesConfig;
        /// <summary>
        /// 规则配置
        /// </summary>
        public static List<LoggerRuleConfigModel> RulesConfig
        {
            get
            {
                if (_rulesConfig != null) return _rulesConfig;
                _rulesConfig = GetValueObject<List<LoggerRuleConfigModel>>("Rules");
                _rulesConfig = FormatListConfig(_rulesConfig);
                return _rulesConfig;
            }
        }
        private static LoggerServerConfigModel? _serverConfig;
        /// <summary>
        /// 服务配置
        /// </summary>
        public static LoggerServerConfigModel ServerConfig
        {
            get
            {
                if (_serverConfig != null) return _serverConfig;
                _serverConfig = GetValueObject<LoggerServerConfigModel>("Server");
                _serverConfig = FormatConfig(_serverConfig);
                return _serverConfig;
            }
        }

        /// <summary>
        /// 格式化配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objs"></param>
        /// <returns></returns>
        public static List<T> FormatListConfig<T>(List<T> objs)
        {
            IList list = FormatListConfig((IList)objs);
            if (list is List<T> result)
            {
                return result;
            }
            return objs;
        }
        /// <summary>
        /// 格式化列表配置
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        public static IList FormatListConfig(IList objs)
        {
            for (int i = 0; i < objs.Count; i++)
            {
                if (objs[i] == null) continue;
                else if (objs[i] is string stringValue)
                {
                    objs[i] = FormatConfig(stringValue);
                }
                else
                {
                    objs[i] = FormatConfig(objs[i]);
                }
            }
            return objs;
        }
        /// <summary>
        /// 格式化配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T FormatConfig<T>(T obj)
        {
            if (obj == null) return obj;
            foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
            {
                if (!propertyInfo.CanWrite || !propertyInfo.CanRead) continue;
                object? value = propertyInfo.GetValue(obj);
                if (value == null) continue;
                else if (value is string stringValue)
                {
                    propertyInfo.SetValue(obj, FormatConfig(stringValue));
                }
                else if (propertyInfo.PropertyType.IsAssignableTo(typeof(IList)))
                {
                    IList list = (IList)value;
                    propertyInfo.SetValue(obj, FormatListConfig(list));
                }
                else
                {
                    propertyInfo.SetValue(obj, FormatConfig(value));
                }
            }
            return obj;
        }
        /// <summary>
        /// 格式化配置
        /// </summary>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        public static string FormatConfig(string stringValue)
        {
            foreach (KeyValuePair<string, string> item in LoggerManager.CustomConfig)
            {
                stringValue = stringValue.Replace($"${{{item.Key}}}", item.Value);
            }
            return stringValue;
        }
        /// <summary>
        /// 获得值对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        private static string GetValue(string key, string defaultValue)
        {
            if (_config == null) return defaultValue;
            string? result = _config.GetValue($"{_rootKey}:{key}");
            return result == null || string.IsNullOrWhiteSpace(result) ? defaultValue : result;
        }
        /// <summary>
        /// 获得值对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private static T GetValueObject<T>(string key, T defaultValue)
        {
            if (_config == null) return defaultValue;
            T? result = _config.GetValueObject<T>($"{_rootKey}:{key}");
            return result ?? defaultValue;
        }
        /// <summary>
        /// 获得值对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        private static T GetValueObject<T>(string key)
            where T : new()
        {
            return GetValueObject(key, new T());
        }
    }
}
