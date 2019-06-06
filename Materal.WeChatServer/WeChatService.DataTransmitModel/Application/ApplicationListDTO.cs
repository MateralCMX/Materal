using System;
namespace WeChatService.DataTransmitModel.Application
{
    /// <summary>
    /// 应用列表数据传输模型
    /// </summary>
    public class ApplicationListDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// AppID
        /// </summary>
        public string AppID { get; set; }
        /// <summary>
        /// AppSecret
        /// </summary>
        public string AppSecret { get; set; }
        /// <summary>
        /// WeChatToken
        /// </summary>
        public string WeChatToken { get; set; }
    }
}
