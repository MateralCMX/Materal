using Materal.ConfigurationHelper;
using Materal.Logger.Models;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.ComponentModel.DataAnnotations;
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
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public static string Application
        {
            get
            {
                string? application = GetValue("Application");
                if (string.IsNullOrWhiteSpace(application))
                {
                    application = "MateralLogger";
                }
                application = FormatConfig(application);
                return application;
            }
        }
        /// <summary>
        /// 目标配置
        /// </summary>
        public static List<MateralLoggerTargetConfigModel> TargetsConfig
        {
            get
            {
                List<MateralLoggerTargetConfigModel> targetsConfig = GetValueObject<List<MateralLoggerTargetConfigModel>>("Targets");
                targetsConfig = FormatConfigs(targetsConfig);
                return targetsConfig;
            }
        }
        /// <summary>
        /// 规则配置
        /// </summary>
        public static List<MateralLoggerRuleConfigModel> RulesConfig
        {
            get
            {
                List<MateralLoggerRuleConfigModel> rulesConfig = GetValueObject<List<MateralLoggerRuleConfigModel>>("Rules");
                rulesConfig = FormatConfigs(rulesConfig);
                return rulesConfig;
            }
        }
        /// <summary>
        /// 服务配置
        /// </summary>
        public static MateralLoggerServerConfigModel ServerConfig
        {
            get
            {
                MateralLoggerServerConfigModel serverConfig = GetValueObject<MateralLoggerServerConfigModel>("Server");
                serverConfig = FormatConfig(serverConfig);
                return serverConfig;
            }
        }
        private const string ConfigKey = "MateralLogger";
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
                result = _config.GetValueObject<T>($"{ConfigKey}:{name}");
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
                result = _config.GetValue($"{ConfigKey}:{name}");
            }
            return result;
        }
        private static List<T> FormatConfigs<T>(List<T> objs)
        {
            IList list = FormatListConfig(objs);
            if(list is List<T> result)
            {
                return result;
            }
            return objs;
        }
        private static IList FormatListConfig(IList objs)
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
        private static T FormatConfig<T>(T obj)
        {
            if (obj == null) return obj;
            foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
            {
                if (!propertyInfo.CanWrite || !propertyInfo.CanRead) continue;
                object? value = propertyInfo.GetValue(obj);
                if (value == null) continue;
                else if(value is string stringValue)
                {
                    propertyInfo.SetValue(obj, FormatConfig(stringValue));
                }
                else if(propertyInfo.PropertyType.IsAssignableTo(typeof(IList)))
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
        private static string FormatConfig(string stringValue)
        {
            foreach (KeyValuePair<string, string> item in MateralLoggerManager.CustomConfig)
            {
                stringValue = stringValue.Replace($"${{{item.Key}}}", item.Value);
            }
            return stringValue;
        }
    }
}
