namespace Materal.Utils.Wechat.Model
{
    /// <summary>
    /// 微信支付配置模型
    /// </summary>
    public class WechatConfigModel
    {
        private string _weChatAPIUrl = "https://api.weixin.qq.com/";

        /// <summary>
        /// 微信域名
        /// </summary>
        public string WechatAPIUrl
        {
            get => _weChatAPIUrl;
            set => _weChatAPIUrl = value[^1] != '/' ? $"{value}/" : value;
        }
        /// <summary>
        /// 绑定APPID（必须配置）
        /// </summary>
        public string APPID { get; set; } = string.Empty;
        /// <summary>
        /// 公众帐号secert
        /// </summary>
        public string APPSECRET { get; set; } = string.Empty;
    }
}
