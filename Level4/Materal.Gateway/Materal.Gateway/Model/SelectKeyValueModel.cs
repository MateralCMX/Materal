namespace Materal.Gateway.Model
{
    public class SelectKeyValueModel<T>
        where T : struct
    {
        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public T? Value { get; set; }
        public SelectKeyValueModel() : this(string.Empty, default)
        {
        }
        public SelectKeyValueModel(string key) : this(key, default)
        {
        }
        public SelectKeyValueModel(string key, T? value)
        {
            Key = key;
            Value = value;
        }
    }
}
