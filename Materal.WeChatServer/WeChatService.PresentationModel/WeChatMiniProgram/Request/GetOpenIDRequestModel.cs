namespace WeChatService.PresentationModel.WeChatMiniProgram.Request
{
    /// <summary>
    /// 获取OpenID请求模型
    /// </summary>
    public class GetOpenIDRequestModel
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// AppID
        /// </summary>
        public string AppID { get; set; }
        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }
    }
}
