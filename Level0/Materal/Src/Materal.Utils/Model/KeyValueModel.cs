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
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public KeyValueModel(string key, string value)
        {
            Key = key;
            Value = value;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public KeyValueModel(int key, string value) : this(key.ToString(), value)
        {
        }
        /// <summary>
        /// 获得所有识别码
        /// </summary>
        /// <returns>所有识别码</returns>
        public static List<KeyValueModel> GetAllCode(Type enumType)
        {
            List<Enum> allCodeList = enumType.GetAllEnum();
            List<KeyValueModel> result = allCodeList.Select(item => new KeyValueModel(Convert.ToInt32(item), item.GetDescription())).ToList();
            return result;
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
    }
    /// <summary>
    /// 键值对模型
    /// </summary>
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
            List<Enum> allCodeList = enumType.GetAllEnum();
            List<KeyValueModel<TEnum>> result = allCodeList.Select(item => new KeyValueModel<TEnum>((TEnum)item, item.GetDescription())).ToList();
            return result;
        }
    }
}
