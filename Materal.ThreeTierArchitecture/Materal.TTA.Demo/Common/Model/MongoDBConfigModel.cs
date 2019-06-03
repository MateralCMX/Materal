namespace Common.Model
{
    public class MongoDBConfigModel
    {
        /// <summary>
        /// 主机名
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString =>
            string.IsNullOrEmpty(Port) ? $"mongodb://{Host}" : $"mongodb://{Host}:{Port}";
    }
}
