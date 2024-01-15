namespace Materal.TTA.MySqlEFRepository
{
    /// <summary>
    /// MySql配置模型
    /// </summary>
    public class MySqlConfigModel : IDBConfigModel
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; } = "127.0.0.1";
        /// <summary>
        /// 端口号
        /// </summary>
        public string Port { get; set; } = "3306";
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = "MyDataBase";
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserID { get; set; } = "root";
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = "123456";
        /// <summary>
        /// 字符集
        /// </summary>
        public string Charset { get; set; } = "utf8";
        /// <summary>
        /// binary blobs 是否按 utf8 对待
        /// </summary>
        public bool TreatTinyAsBoolean { get; set; } = false;
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString => $"Database={Name};Data Source={Address};Port={Port};User Id={UserID};Password={Password};Charset={Charset};TreatTinyAsBoolean={TreatTinyAsBoolean};";
    }
}
