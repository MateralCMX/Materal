using System;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "名称不可以为空"), StringLength(100, ErrorMessage = "名称长度不能超过50")]
        public string Name { get; set; }
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        [Required(ErrorMessage = "用户唯一标识不可以为空")]
        public Guid UserID { get; set; }
        /// <summary>
        /// AppID
        /// </summary>
        [Required(ErrorMessage = "AppID不可以为空")]
        public string AppID { get; set; }
        /// <summary>
        /// AppSecret
        /// </summary>
        [Required(ErrorMessage = "AppSecret不可以为空")]
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
