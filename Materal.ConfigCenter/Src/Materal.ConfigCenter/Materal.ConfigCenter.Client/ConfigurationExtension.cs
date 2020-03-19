using System.Linq;
using Materal.ConvertHelper;
using Microsoft.Extensions.Configuration;

namespace Materal.ConfigCenter.Client
{
    public static class ConfigurationExtension
    {
        public static string GetValue(this IConfiguration configuration, string key, string @namespace = null)
        {
            if (string.IsNullOrEmpty(@namespace) || !(configuration is IConfigurationRoot configurationRoot)) return configuration[key];
            IConfigurationProvider provider = configurationRoot.Providers?.FirstOrDefault(m=>m is MateralConfigurationProvider temp && temp.NamespaceName.Equals(@namespace));
            if (provider == null) throw new MateralConfigCenterException("命名空间未加载");
            return provider.TryGet(key, out string result) ? result : null;
        }
        public static T GetValue<T>(this IConfiguration configuration, string key, string @namespace = null)
        {
            string json = GetValue(configuration, key, @namespace);
            return string.IsNullOrEmpty(json) ? default : json.JsonToObject<T>();
        }
    }
}
