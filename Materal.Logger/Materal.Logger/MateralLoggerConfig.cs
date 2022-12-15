using Materal.ConfigurationHelper;
using Materal.ConvertHelper;
using Materal.Logger.Models;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Materal.Logger
{
    public static class MateralLoggerConfig
    {
        /// <summary>
        /// 本地配置项
        /// </summary>
        private static IConfiguration? _config;
        public static void Init(IConfiguration? configuration)
        {
            if (configuration == null) return;
            _config = configuration;
        }
        private static string? _application;
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public static string Application
        {
            get
            {
                if (_application != null) return _application;
                _application = GetValue("Application");
                if (string.IsNullOrWhiteSpace(_application))
                {
                    _application = "未知应用程序";
                }
                return _application;
            }
            set => _application = value;
        }
        private static List<MateralLoggerTargetConfigModel>? _targetsConfig;
        /// <summary>
        /// 目标配置
        /// </summary>
        public static List<MateralLoggerTargetConfigModel> TargetsConfig
        {
            get
            {
                if (_targetsConfig != null) return _targetsConfig;
                _targetsConfig = GetValueObject<List<MateralLoggerTargetConfigModel>>("Targets");
                return _targetsConfig;
            }
        }
        private static List<MateralLoggerRuleConfigModel>? _rulesConfig;
        /// <summary>
        /// 规则配置
        /// </summary>
        public static List<MateralLoggerRuleConfigModel> RulesConfig
        {
            get
            {
                if (_rulesConfig != null) return _rulesConfig;
                _rulesConfig = GetValueObject<List<MateralLoggerRuleConfigModel>>("Rules");
                return _rulesConfig;
            }
        }
        private static MateralLoggerServerConfigModel? _serverConfig;
        /// <summary>
        /// 服务配置
        /// </summary>
        public static MateralLoggerServerConfigModel ServerConfig
        {
            get
            {
                if (_serverConfig != null) return _serverConfig;
                _serverConfig = GetValueObject<MateralLoggerServerConfigModel>("Server");
                return _serverConfig;
            }
        }
        /// <summary>
        /// 获取配置项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="MateralLoggerException"></exception>
        private static T GetValueObject<T>(string name)
            where T : class, new()
        {
            T? result = default;
            if (_config != null)
            {
                result = GetValueObject<T>(_config, name);
            }
            return result ?? new();
        }
        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="MateralLoggerException"></exception>
        private static string? GetValue(string name)
        {
            string? result = null;
            if (_config != null)
            {
                result = GetValue(_config, name);
            }
            return result;
        }
        private const string ConfigKey = "MateralLogger";
        /// <summary>
        /// 获得值对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static T? GetValueObject<T>(IConfiguration configuration, string key) => configuration.GetValueObject<T>($"{ConfigKey}:{key}");
        /// <summary>
        /// 获得值
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string? GetValue(IConfiguration configuration, string key) => configuration.GetValue($"{ConfigKey}:{key}");
    }
}
