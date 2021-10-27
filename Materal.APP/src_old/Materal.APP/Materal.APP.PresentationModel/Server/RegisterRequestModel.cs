using Materal.APP.Enums;

namespace Materal.APP.PresentationModel.Server
{
    /// <summary>
    /// 注册请求模型
    /// </summary>
    public class RegisterRequestModel
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
        public string Url { get; set; }
    }
}
