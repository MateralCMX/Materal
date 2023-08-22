using Materal.Gateway.Common.ConfigModel;
using Microsoft.Extensions.Configuration;

namespace Materal.Gateway.Common
{
    /// <summary>
    /// 应用程序配置
    /// </summary>
    public static class ApplicationConfig
    {
        /// <summary>
        /// 本地配置项
        /// </summary>
        public static IConfigurationRoot? Configuration { get; set; }
        /// <summary>
        /// 授权配置
        /// </summary>
        public static List<UserConfigModel> Users => GetValueObject<List<UserConfigModel>>("Users");
        /// <summary>
        /// 忽略无法找到下游路由错误
        /// </summary>
        public static bool IgnoreUnableToFindDownstreamRouteError { get; set; } = false;
        /// <summary>
        /// 获取配置项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="defatuleValue"></param>
        /// <returns></returns>
        public static T GetValueObject<T>(string name, T? defatuleValue = default)
            where T : new()
        {
            T? result = default;
            if (Configuration != null)
            {
                result = Configuration.GetValueObject<T>(name);
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
        public static string GetValue(string name, string? defaultValue = null)
        {
            string? result = null;
            if (Configuration != null)
            {
                result = Configuration.GetValue(name);
            }
            if (!string.IsNullOrWhiteSpace(result)) return result;
            return defaultValue ?? string.Empty;
        }
    }
}
