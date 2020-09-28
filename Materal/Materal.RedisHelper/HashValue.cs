namespace Materal.RedisHelper
{
    /// <summary>
    /// Hash值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HashValue<T>
    {
        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public T Value { get; set; }
    }
}
