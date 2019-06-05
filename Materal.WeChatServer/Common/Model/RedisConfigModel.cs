namespace Common.Model
{
    public class RedisConfigModel
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        public string Port { get; set; }
        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString => string.IsNullOrEmpty(Port) ? Host : $"{Host}:{Port}";
    }
}
