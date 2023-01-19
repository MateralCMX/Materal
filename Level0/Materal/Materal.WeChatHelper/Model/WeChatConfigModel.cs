using System.Collections.Generic;
using Materal.SystemHelper;

namespace Materal.WeChatHelper.Model
{
    /// <summary>
    /// 微信支付配置模型
    /// </summary>
    public class WeChatConfigModel
    {
        private string _weChatAPIUrl = "https://api.weixin.qq.com/";

        /// <summary>
        /// 微信域名
        /// </summary>
        public string WeChatAPIUrl
        {
            get => _weChatAPIUrl;
            set => _weChatAPIUrl = value[value.Length - 1] != '/' ? $"{value}/" : value;
        }
        #region 基本信息配置
        /// <summary>
        /// 绑定支付的APPID（必须配置）
        /// </summary>
        public string APPID { get; set; }
        /// <summary>
        /// 商户号（必须配置）
        /// </summary>
        public string MCHID { get; set; }
        /// <summary>
        /// 商户支付密钥（必须配置）
        /// </summary>
        public string KEY { get; set; }
        /// <summary>
        /// 公众帐号secert（仅JSAPI支付的时候需要配置）
        /// </summary>
        public string APPSECRET { get; set; }
        #endregion
        #region 证书路径设置
        /// <summary>
        /// 证书路径,注意应该填写绝对路径（仅退款、撤销订单时需要）
        /// </summary>
        public string SSLCERT_PATH { get; set; }
        /// <summary>
        /// 证书密码(默认是商户号)
        /// </summary>
        private string _sslcert_password = string.Empty;
        /// <summary>
        /// 证书密码(默认是商户号)
        /// </summary>
        public string SSLCERT_PASSWORD
        {
            get => string.IsNullOrEmpty(_sslcert_password) ? MCHID : _sslcert_password;
            set => _sslcert_password = value;
        }
        #endregion
        #region 回调URL
        /// <summary>
        /// 支付结果通知回调url，用于商户接收支付结果
        /// </summary>
        public string NOTIFY_URL { get; set; }
        #endregion
        #region 商户系统IP
        private string _ip = string.Empty;
        /// <summary>
        /// 此参数可手动配置也可在程序中自动获取
        /// </summary>
        public string IP
        {
            get
            {
                if (!string.IsNullOrEmpty(_ip)) return _ip;
                List<string> ipv4List = SystemInfoManager.GetLocalIPv4();
                return ipv4List.Count > 0 ? ipv4List[0] : "127.0.0.1";
            }
            set => _ip = value;
        }
        #endregion
        #region 上报信息配置
        /// <summary>
        /// 测速上报等级，0.关闭上报; 1.仅错误时上报; 2.全量上报
        /// </summary>
        public int REPORT_LEVENL { get; set; }
        #endregion
    }
}
