namespace Materal.TTA.SqlServerRepository.Model
{
    /// <summary>
    /// SQLServer从属配置
    /// </summary>
    public class SqlServerSubordinateConfigModel
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string AttachDbFilename { get; set; }
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
        public string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(AttachDbFilename))
                {
                    return string.IsNullOrEmpty(Port) ?
                        $"Data Source={Address}; Database={Name}; User ID={UserID}; Password={Password};MultipleActiveResultSets=true" :
                        $"Data Source={Address},{Port}; Database={Name}; User ID={UserID}; Password={Password};MultipleActiveResultSets=true";
                }
                return $@"Data Source={Address};AttachDbFilename={AttachDbFilename};Integrated Security=True;MultipleActiveResultSets=true";
            }
        }
    }
}
