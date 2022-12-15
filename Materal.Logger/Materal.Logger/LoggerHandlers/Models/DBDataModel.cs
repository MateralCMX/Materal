using Materal.Logger.Models;

namespace Materal.Logger.LoggerHandlers.Models
{
    public class DBDataModel
    {
        /// <summary>
        /// 数据库路径
        /// </summary>
        public string DBConnectionString { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public MateralLogModel Model { get; set; } = new MateralLogModel();
    }
}
