using Materal.APP.Enums;

namespace Materal.APP.Services.Models.Server
{
    public class RegisterModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 服务类型
        /// </summary>
        public ServerCategoryEnum ServerCategory { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 连接地址
        /// </summary>
        public string Url{ get; set; }
    }
}
