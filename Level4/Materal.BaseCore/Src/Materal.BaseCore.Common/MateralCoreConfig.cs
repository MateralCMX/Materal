using Materal.BaseCore.Common.ConfigModels;
using Microsoft.Extensions.Configuration;

namespace Materal.BaseCore.Common
{
    /// <summary>
    /// 核心配置
    /// </summary>
    public static class MateralCoreConfig
    {
        /// <summary>
        /// 本地配置项
        /// </summary>
        public static IConfiguration? Configuration { get; set; }
        /// <summary>
        /// 授权配置
        /// </summary>
        public static JWTConfigModel JWTConfig => GetConfigItem<JWTConfigModel>("JWT");
        /// <summary>
        /// 异常配置
        /// </summary>
        public static ExceptionConfigModel ExceptionConfig => GetConfigItem<ExceptionConfigModel>("Exception");
        /// <summary>
        /// 事件总线配置
        /// </summary>
        public static EventBusConfigModel EventBusConfig => GetConfigItem<EventBusConfigModel>(nameof(EventBusConfig));
        /// <summary>
        /// 获取配置项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="defatuleValue"></param>
        /// <returns></returns>
        public static T GetConfigItem<T>(string name, T? defatuleValue = default)
            where T : new()
        {
            T? result = default;
            if (Configuration != null)
            {
                result = Configuration.GetConfigItem<T>(name);
            }
            if (result != null) return result;
            return defatuleValue ?? new T();
        }
        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetConfigItemToString(string name, string? defaultValue = null)
        {
            string? result = null;
            if (Configuration != null)
            {
                result = Configuration.GetConfigItemToString(name);
            }
            if (!string.IsNullOrWhiteSpace(result)) return result;
            return defaultValue ?? string.Empty;
        }
    }
}
