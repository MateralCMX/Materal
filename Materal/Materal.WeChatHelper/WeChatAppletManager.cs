using LitJson;
using Materal.WeChatHelper.Model;

namespace Materal.WeChatHelper
{
    /// <summary>
    /// 微信小程序管理器
    /// </summary>
    public class WeChatAppletManager
    {
        protected const string WeChatApiUrl = "https://api.weixin.qq.com/";
        /// <summary>
        /// 配置文件
        /// </summary>
        protected readonly WeChatConfigModel Config;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="configM"></param>
        public WeChatAppletManager(WeChatConfigModel configM)
        {
            Config = configM;
        }
        /// <summary>
        /// 根据Code获得OpenID
        /// </summary>
        /// <param name="code">Code</param>
        /// <returns>OpenID</returns>
        public string GetOpenIDByCode(string code)
        {
            var data = new WeChatDataModel();
            data.SetValue("appid", Config.APPID);
            data.SetValue("secret", Config.APPSECRET);
            data.SetValue("grant_type", "authorization_code");
            data.SetValue("js_code", code);
            string url = $"{WeChatApiUrl}sns/jscode2session?{data.ToUrlParams()}";
            string result = WeChatHttpManager.Get(url);
            JsonData jsonData = JsonMapper.ToObject(result);
            if (jsonData["openid"] == null) throw new WeChatException(result);
            string openID = (string)jsonData["openid"];
            return openID;
        }
    }
}
