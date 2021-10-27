using Materal.APP.Enums;

namespace Materal.APP.DataTransmitModel
{
    /// <summary>
    /// 服务列表数据传输模型
    /// </summary>
    public class ServerListDTO
    {
        /// <summary>
        /// 服务类型
        /// </summary>
        public ServerCategoryEnum ServerCategory { get; set; }
        /// <summary>
        /// Url地址
        /// </summary>
        public string Url{ get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name{ get; set; }
    }
}
