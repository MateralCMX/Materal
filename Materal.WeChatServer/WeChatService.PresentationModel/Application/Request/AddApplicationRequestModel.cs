using System;
namespace WeChatService.PresentationModel.Application.Request
{
    /// <summary>
    /// 应用添加请求模型
    /// </summary>
    public class AddApplicationRequestModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public Guid UserID { get; set; }
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
        /// <summary>
        /// EncodingAESKey
        /// </summary>
        public string EncodingAESKey { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
