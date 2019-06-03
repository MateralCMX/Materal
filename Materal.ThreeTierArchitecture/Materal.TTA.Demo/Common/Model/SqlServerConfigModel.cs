namespace Common.Model
{
    /// <summary>
    /// 数据库配置模型
    /// </summary>
    public class SqlServerConfigModel
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        public string Port { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString => string.IsNullOrEmpty(Port) ? 
            $"Data Source={Address}; Database={Name}; User ID={UserID}; Password={Password};": 
            $"Data Source={Address},{Port}; Database={Name}; User ID={UserID}; Password={Password};";
    }
}
