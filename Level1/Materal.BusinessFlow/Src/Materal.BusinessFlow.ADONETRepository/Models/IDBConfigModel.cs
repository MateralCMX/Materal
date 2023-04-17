namespace Materal.BusinessFlow.ADONETRepository.Models
{
    /// <summary>
    /// 数据库配置模型
    /// </summary>
    public interface IDBConfigModel
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        string ConnectionString { get; }
    }
}
