using Materal.Logger.Models;

namespace Materal.Logger.LoggerHandlers.Models
{
    /// <summary>
    /// 数据库数据模型
    /// </summary>
    public class DBDataModel
    {
        /// <summary>
        /// 数据库路径
        /// </summary>
        public string DBConnectionString { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public LogModel Model { get; set; } = new LogModel();
    }
}
