namespace Materal.RedisHelper
{
    public class RedisConfigModel
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Host { get; set; } = "127.0.0.1";
        /// <summary>
        /// 端口号
        /// </summary>
        public string Port { get; set; } = "6379";
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString
        {
            get
            {
                string result = string.IsNullOrEmpty(Port) ? Host : $"{Host}:{Port}";
                if (!string.IsNullOrEmpty(Password)) result += $",password={Password}";
                return result;
            }
        }
    }
}
