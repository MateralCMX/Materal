using Materal.ConvertHelper;
using Microsoft.Extensions.Configuration;

namespace Materal.ConfigCenter.Client
{
    public static class ConfigurationExtension
    {
        public static string GetValue(this IConfiguration configuration, string key)
        {
            return configuration[key];
        }
        public static T GetValue<T>(this IConfiguration configuration, string key)
        {
            string json = GetValue(configuration, key);
            return string.IsNullOrEmpty(json) ? default : json.JsonToObject<T>();
        }
    }
}
