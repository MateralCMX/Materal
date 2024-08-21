namespace Materal.Utils.Model
{
    /// <summary>
    /// 键值对模型
    /// </summary>
    public class KeyValueModel
    {
        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public KeyValueModel()
        {
            Key = string.Empty;
            Value = string.Empty;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public KeyValueModel(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
    /// <summary>
    /// 键值对模型
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class KeyValueModel<TKey, TValue>
    {
        /// <summary>
        /// 键
        /// </summary>
        public TKey? Key { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public TValue? Value { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public KeyValueModel()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public KeyValueModel(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
    /// <summary>
    /// 键值对模型
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    public class KeyValueModel<TEnum>
        where TEnum : struct, Enum
    {
        /// <summary>
        /// 键
        /// </summary>
        public TEnum Key { get; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; } = string.Empty;
        /// <summary>
        /// 构造方法
        /// </summary>
        public KeyValueModel()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="key">键</param>
        public KeyValueModel(TEnum key)
        {
            Key = key;
            Value = key.ToString();
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public KeyValueModel(TEnum key, string value)
        {
            Key = key;
            Value = value;
        }
        /// <summary>
        /// 获得所有识别码
        /// </summary>
        /// <returns>所有识别码</returns>
        public static List<KeyValueModel<TEnum>> GetAllCode()
        {
            Type enumType = typeof(TEnum);
            return enumType.GetAllCode<TEnum>();
        }
    }
    /// <summary>
    /// 键值对模型扩展
    /// </summary>
    public static class KeyValueModelExtension
    {
        /// <summary>
        /// 转换为字典
        /// </summary>
        /// <param name="keyValueModels"></param>
        /// <param name="useNewValue">如果Key重复,使用新的值</param>
        /// <returns></returns>
        public static Dictionary<string, string> ToDictionary(this List<KeyValueModel> keyValueModels, bool useNewValue = true)
        {
            Dictionary<string, string> result = [];
            foreach (KeyValueModel item in keyValueModels)
            {
                if (result.ContainsKey(item.Key))
                {
                    if (useNewValue)
                    {
                        result[item.Key] = item.Value;
                    }
                }
                else
                {
                    result.Add(item.Key, item.Value);
                }
            }
            return result;
        }
        /// <summary>
        /// 转换为键值对模型
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static List<KeyValueModel> ToKeyValueModel(this Dictionary<string, string> dictionary)
            => dictionary.Select(item => new KeyValueModel(item.Key, item.Value)).ToList();
        /// <summary>
        /// 转换为键值对模型
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static List<KeyValueModel<TKey, TValue>> ToKeyValueModel<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
            where TKey : notnull
            => dictionary.Select(item => new KeyValueModel<TKey, TValue>(item.Key, item.Value)).ToList();
        /// <summary>
        /// 获得所有识别码
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static List<KeyValueModel<TEnum>> GetAllCode<TEnum>(this TEnum @enum)
            where TEnum : struct, Enum => @enum.GetType().GetAllCode<TEnum>();
        /// <summary>
        /// 获得所有识别码
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="enumType"></param>
        /// <returns></returns>
        internal static List<KeyValueModel<TEnum>> GetAllCode<TEnum>(this Type enumType)
            where TEnum : struct, Enum
        {
            List<Enum> allCodeList = enumType.GetAllEnum();
            List<KeyValueModel<TEnum>> result = allCodeList.Select(item => new KeyValueModel<TEnum>((TEnum)item, item.GetDescription())).ToList();
            return result;
        }
    }
}
