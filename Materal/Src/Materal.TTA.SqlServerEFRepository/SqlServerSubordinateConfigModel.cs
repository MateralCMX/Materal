namespace Materal.TTA.SqlServerEFRepository
{
    /// <summary>
    /// SQLServer从属配置
    /// </summary>
    public class SqlServerSubordinateConfigModel : IDBConfigModel
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; } = "127.0.0.1";
        /// <summary>
        /// 文件名
        /// </summary>
        public string? AttachDbFilename { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        public string Port { get; set; } = "1433";
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = "MyDataBase";
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserID { get; set; } = "sa";
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = "123456";
        /// <summary>
        /// 支持多个
        /// </summary>
        public bool Multiple { get; set; } = true;
        /// <summary>
        /// 信任服务器证书
        /// </summary>
        public bool TrustServerCertificate { get; set; } = false;
        /// <summary>
        /// 加密的
        /// </summary>
        public bool Encrypt { get; set; } = true;
        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString
        {
            get
            {
                StringBuilder result = new();
                result.Append($"Data Source={Address}");
                if (!string.IsNullOrWhiteSpace(Port))
                {
                    result.Append($",{Port}");
                }
                result.Append(';');
                if (string.IsNullOrWhiteSpace(AttachDbFilename))
                {
                    result.Append($"Database={Name}; User ID={UserID}; Password={Password};");
                }
                else
                {
                    result.Append($"AttachDbFilename={AttachDbFilename};Integrated Security=True;");
                }
                result.Append($"MultipleActiveResultSets={Multiple};Encrypt={Encrypt};TrustServerCertificate={TrustServerCertificate};");
                return result.ToString();
            }
        }
    }
}
