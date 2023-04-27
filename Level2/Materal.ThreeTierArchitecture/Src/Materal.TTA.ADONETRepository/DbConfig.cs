namespace Materal.TTA.ADONETRepository
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    public abstract class DbConfig
    {
        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;
    }
}
