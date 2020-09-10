using Materal.APP.Enums;

namespace Materal.APP.DataTransmitModel
{
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
    }
}
