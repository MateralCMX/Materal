using System.Collections.Generic;
using System.Threading.Tasks;
using LitJson;
using Materal.NetworkHelper;
using Materal.WeChatHelper.Model;

namespace Materal.WeChatHelper
{
    /// <summary>
    /// 微信小程序管理器
    /// </summary>
    public class WeChatMiniProgramManager
    {
        /// <summary>
        /// 配置文件
        /// </summary>
        protected readonly WeChatConfigModel Config;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="configM"></param>
        public WeChatMiniProgramManager(WeChatConfigModel configM)
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
            var data = new Dictionary<string, string>
            {
                {"appid", Config.APPID},
                {"secret", Config.APPSECRET},
                {"grant_type", "authorization_code"},
                {"js_code", code},
            };
            string result = HttpManager.SendGet($"{Config.WeChatAPIUrl}sns/jscode2session", data);
            JsonData jsonData = JsonMapper.ToObject(result);
            if (!jsonData.ContainsKey("openid") || jsonData["openid"] == null) throw new WeChatException(result);
            var openID = (string)jsonData["openid"];
            return openID;
        }
        /// <summary>
        /// 根据Code获得OpenID
        /// </summary>
        /// <param name="code">Code</param>
        /// <returns>OpenID</returns>
        public async Task<string> GetOpenIDByCodeAsync(string code)
        {
            var data = new Dictionary<string, string>
            {
                {"appid", Config.APPID},
                {"secret", Config.APPSECRET},
                {"grant_type", "authorization_code"},
                {"js_code", code},
            };
            string result = await HttpManager.SendGetAsync($"{Config.WeChatAPIUrl}sns/jscode2session", data);
            JsonData jsonData = JsonMapper.ToObject(result);
            if (!jsonData.ContainsKey("openid") || jsonData["openid"] == null) throw new WeChatException(result);
            var openID = (string)jsonData["openid"];
            return openID;
        }
    }
}
