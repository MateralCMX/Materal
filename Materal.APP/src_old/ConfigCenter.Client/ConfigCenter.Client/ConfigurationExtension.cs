using System;
using System.Linq;
using System.Reflection;
using Materal.ConvertHelper;
using Microsoft.Extensions.Configuration;

namespace ConfigCenter.Client
{
    public static class ConfigurationExtension
    {
        public static string GetValue(this IConfiguration configuration, string key, string @namespace = null)
        {
            if (string.IsNullOrEmpty(@namespace) || !(configuration is IConfigurationRoot configurationRoot)) return configuration[key];
            IConfigurationProvider provider = configurationRoot.Providers?.FirstOrDefault(m=>m is MateralConfigurationProvider temp && temp.NamespaceName.Equals(@namespace));
            if (provider == null) throw new MateralConfigCenterClientException("命名空间未加载");
            return provider.TryGet(key, out string result) ? result : null;
        }
        public static T GetValueObject<T>(this IConfiguration configuration, string key, string @namespace = null)
        {
            string value = GetValue(configuration, key, @namespace);
            if (string.IsNullOrEmpty(value)) return default;
            T result;
            Type tType = typeof(T);
            try
            {
                if (value.CanConvertTo(tType))
                {
                    result = value.ConvertTo<T>();
                }
                else if (tType.GetCustomAttribute<SerializableAttribute>() != null)
                {
                    result = value.JsonToDeserializeObject<T>();
                }
                else
                {
                    result = value.JsonToObject<T>();
                }
            }
            catch (Exception ex)
            {
                throw new MateralConfigCenterClientException("数据转换失败", ex);
            }
            return result;
        }
    }
}
