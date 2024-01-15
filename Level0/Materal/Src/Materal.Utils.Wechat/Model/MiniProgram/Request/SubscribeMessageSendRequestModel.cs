namespace Materal.Utils.Wechat.Model.MiniProgram.Request
{
    /// <summary>
    /// 订阅消息发送请求模型
    /// </summary>
    public class SubscribeMessageSendRequestModel
    {
        /// <summary>
        /// 接口调用凭证
        /// </summary>
        public string AccessToken { get; set; } = string.Empty;
        /// <summary>
        /// 接收者（用户）的 openid
        /// </summary>
        public string OpenID { get; set; } = string.Empty;
        /// <summary>
        /// 所需下发的订阅模板id
        /// </summary>
        public string TemplateID { get; set; } = string.Empty;
        /// <summary>
        /// 模板内容，格式形如 { "key1": { "value": any }, "key2": { "value": any } }
        /// </summary>
        public Dictionary<string, string> TemplateData { get; set; } = [];
        /// <summary>
        /// 点击模板卡片后的跳转页面，仅限本小程序内的页面。支持带参数,（示例index?foo=bar）。该字段不填则模板无跳转。
        /// </summary>
        public string? GoToPage { get; set; } = null;
        /// <summary>
        /// 跳转小程序类型：developer为开发版；trial为体验版；formal为正式版；默认为正式版
        /// </summary>
        public string? MiniprogramState { get; set; } = null;
        /// <summary>
        /// 进入小程序查看”的语言类型，支持zh_CN(简体中文)、en_US(英文)、zh_HK(繁体中文)、zh_TW(繁体中文)，默认为zh_CN
        /// </summary>
        public string? Language { get; set; } = null;
    }
}
