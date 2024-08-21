namespace Materal.Extensions
{
    /// <summary>
    /// 配置对象扩展
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// 获得值对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T? GetConfigItem<T>(this IConfiguration configuration, string key)
        {
            IConfigurationSection configSection = configuration.GetSection(key);
            if (!string.IsNullOrWhiteSpace(configSection.Value)) return default;
            return configSection.GetConfigItem<T>();
        }
        /// <summary>
        /// 获得值对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configSection"></param>
        /// <returns></returns>
        public static T? GetConfigItem<T>(this IConfigurationSection configSection)
        {
            string? value = configSection.GetConfigItemToString();
            if (string.IsNullOrEmpty(value) || value is null) return default;
            T? result;
            Type tType = typeof(T);
            if (tType.IsGenericType && tType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                tType = tType.GetGenericArguments()[0];
            }
            if (value.CanConvertTo(tType))
            {
                result = (T?)value.ConvertTo(tType);
            }
            else if (tType.IsEnum)
            {
                result = (T?)Enum.Parse(tType, value);
            }
            else if (value.IsJson())
            {
                result = value.JsonToObject<T>();
            }
            else
            {
                result = default;
            }
            return result;
        }
        /// <summary>
        /// 获得动态对象
        /// </summary>
        /// <param name="configSection"></param>
        /// <returns></returns>
        public static object? GetConfigItem(this IConfigurationSection configSection)
        {
            IConfigurationSection[] sectionItems = configSection.GetChildren().ToArray();
            if (sectionItems.Length == 0) return null;
            Dictionary<string, object?> propertyDic = [];
            List<object?> objects = [];
            bool isArray = sectionItems.First().Key == "0";
            foreach (IConfigurationSection sectionItem in sectionItems)
            {
                if (!string.IsNullOrWhiteSpace(sectionItem.Value))
                {
                    if (!isArray)
                    {
                        propertyDic.Add(sectionItem.Key, sectionItem.Value);
                    }
                    else
                    {
                        objects.Add(sectionItem.Value);
                    }
                }
                else
                {
                    object? value = sectionItem.GetConfigItem();
                    if (value is null) continue;
                    if (!isArray)
                    {
                        propertyDic.Add(sectionItem.Key, value);
                    }
                    else
                    {
                        objects.Add(value);
                    }
                }
            }
            return isArray ? objects : propertyDic;
        }
        /// <summary>
        /// 获得值
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string? GetConfigItemToString(this IConfiguration configuration, string key)
        {
            IConfigurationSection configSection = configuration.GetSection(key);
            if (!string.IsNullOrWhiteSpace(configSection.Value)) return configSection.Value;
            return configSection.GetConfigItemToString();
        }
        /// <summary>
        /// 获得对象Json
        /// </summary>
        /// <param name="configSection"></param>
        /// <returns></returns>
        public static string? GetConfigItemToString(this IConfigurationSection configSection)
        {
            object? value = configSection.GetConfigItem();
            if (value is not null)
            {
                return value.ToJson();
            }
            return null;
        }
    }
}
