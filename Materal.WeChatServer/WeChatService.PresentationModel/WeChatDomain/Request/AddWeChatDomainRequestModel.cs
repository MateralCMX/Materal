namespace WeChatService.PresentationModel.WeChatDomain.Request
{
    /// <summary>
    /// 微信域名添加请求模型
    /// </summary>
    public class AddWeChatDomainRequestModel
    {
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
