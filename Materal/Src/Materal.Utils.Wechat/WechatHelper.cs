using LitJson;
using Materal.Utils.Wechat.Model;
using Materal.Utils.Wechat.Model.Result;

namespace Materal.Utils.Wechat
{
    /// <summary>
    /// 微信帮助类
    /// </summary>
    public abstract class WechatHelper
    {
        /// <summary>
        /// 微信配置
        /// </summary>
        protected readonly WechatConfigModel Config;
        /// <summary>
        /// Http帮助类
        /// </summary>
        protected readonly IHttpHelper HttpHelper;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="config"></param>
        /// <param name="httpHelper"></param>
        protected WechatHelper(WechatConfigModel config, IHttpHelper? httpHelper = null)
        {
            Config = config;
            httpHelper ??= new HttpHelper();
            HttpHelper = httpHelper;
        }

        /// <summary>
        /// 获得AccessToken
        /// </summary>
        /// <returns></returns>
        public async Task<AccessTokenResultModel> GetAccessTokenAsync()
        {
            // 文档地址：https://developers.weixin.qq.com/doc/offiaccount/Basic_Information/Get_access_token.html
            // 请求方式：GET https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=APPID&secret=APPSECRET
            Dictionary<string, string> queryParams = new()
            {
                {"grant_type", "client_credential"},
                {"appid", Config.APPID},
                {"secret", Config.APPSECRET}
            };
            string httpResult = await HttpHelper.SendGetAsync($"{Config.WechatAPIUrl}cgi-bin/token", queryParams);
            JsonData jsonData = HandlerHttpResult(httpResult);
            AccessTokenResultModel result = new()
            {
                AccessToken = jsonData.GetString("access_token") ?? throw GetWechatException(jsonData),
                ExpiresIn = jsonData.GetInt("expires_in") ?? throw GetWechatException(jsonData),
            };
            return result;
        }
        /// <summary>
        /// 处理Http结果
        /// </summary>
        /// <param name="httpResult"></param>
        /// <returns></returns>
        /// <exception cref="WechatException"></exception>
        protected static JsonData HandlerHttpResult(string httpResult)
        {
            JsonData jsonData = JsonMapper.ToObject(httpResult);
            int? errcode = jsonData.GetInt("errcode");
            if (!errcode.HasValue) return jsonData;
            if (errcode.HasValue && errcode.Value == 0) return jsonData;
            throw GetWechatException(jsonData);
        }
        /// <summary>
        /// 获得微信异常
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        protected static WechatException GetWechatException(JsonData jsonData)
        {
            int errcode = jsonData.GetInt("errcode") ?? 500;
            string message = jsonData.GetString("errmsg") ?? "无返回错误消息";
            return new WechatException(message, errcode.ToString());
        }
    }
}
