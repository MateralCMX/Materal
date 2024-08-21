namespace Materal.Utils.Wechat.Model.OfficialAccount.Request
{
    /// <summary>
    /// 发送模版消息请求模型
    /// </summary>
    public class SendTemplateMessageRequestModel
    {
        /// <summary>
        /// 接口调用凭证
        /// </summary>
        public string AccessToken { get; set; } = string.Empty;
        /// <summary>
        /// 用户OpenID
        /// </summary>
        [Required(ErrorMessage = "用户OpenID必填")]
        public string UserOpenID { get; set; } = string.Empty;
        /// <summary>
        /// 模版ID
        /// </summary>
        [Required(ErrorMessage = "模版ID必填")]
        public string TemplateID { get; set; } = string.Empty;
        /// <summary>
        /// 跳转Url
        /// </summary>
        public string? Url { get; set; }
        /// <summary>
        /// 跳转小程序
        /// </summary>
        public GoToMiniprogramModel? Miniprogram { get; set; }
        /// <summary>
        /// 数据集
        /// </summary>
        [Required(ErrorMessage = "数据集必填")]
        public List<KeyValueModel> TemplateDatas { get; set; } = [];
        /// <summary>
        /// 防重复标识ID
        /// </summary>
        public string? ClientMessageID { get; set; }
    }
}
