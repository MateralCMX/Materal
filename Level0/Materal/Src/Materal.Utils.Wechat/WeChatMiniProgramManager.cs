using LitJson;
using Materal.Utils.Http;
using Materal.WeChatHelper.Model;
using Materal.WeChatHelper.Model.Basis.Request;
using Materal.WeChatHelper.Model.Basis.Result;

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
        protected readonly IHttpHelper _httpHelper;
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
        public async Task<string> GetOpenIDByCodeAsync(string code)
        {
            var queryParams = new Dictionary<string, string>
            {
                {"appid", Config.APPID},
                {"secret", Config.APPSECRET},
                {"grant_type", "authorization_code"},
                {"js_code", code},
            };
            string httpResult = await _httpHelper.SendGetAsync($"{Config.WeChatAPIUrl}sns/jscode2session", queryParams);
            JsonData jsonData = HandlerHttpResult(httpResult);
            string openID = jsonData.GetString("openid") ?? throw GetWeChatException(jsonData);
            return openID;
        }
        /// <summary>
        /// 获得AccessToken
        /// </summary>
        /// <returns></returns>
        /// <exception cref="WeChatException"></exception>
        public async Task<AccessTokenResultModel> GetAccessTokenAsync()
        {
            var queryParams = new Dictionary<string, string>
            {
                {"grant_type", "client_credential"},
                {"appid", Config.APPID},
                {"secret", Config.APPSECRET}
            };
            string httpResult = await _httpHelper.SendGetAsync($"{Config.WeChatAPIUrl}cgi-bin/token", queryParams);
            JsonData jsonData = HandlerHttpResult(httpResult);
            var result = new AccessTokenResultModel
            {
                AccessToken = jsonData.GetString("access_token") ?? throw GetWeChatException(jsonData),
                ExpiresIn = jsonData.GetInt("expires_in") ?? throw GetWeChatException(jsonData),
            };
            return result;
        }
        /// <summary>
        /// 获得AccessToken
        /// </summary>
        /// <returns></returns>
        /// <exception cref="WeChatException"></exception>
        public async Task SubscribeMessageSendAsync(SubscribeMessageSendRequestModel requestModel)
        {
            var queryParams = new Dictionary<string, string>
            {
                {"access_token", requestModel.AccessToken}
            };
            var data = new
            {
                touser = requestModel.OpenID,
                template_id = requestModel.TemplateID,
                page = requestModel.GoToPage,
                data = new Dictionary<string, object>(),
                miniprogram_state = requestModel.MiniprogramState,
                lang = requestModel.Language
            };
            foreach (var item in requestModel.TemplateData)
            {
                data.data.Add(item.Key, new { value = item.Value });
            }
            string result = await _httpHelper.SendPostAsync($"{Config.WeChatAPIUrl}cgi-bin/message/subscribe/send", queryParams, data);
            HandlerHttpResult(result);
        }
        /// <summary>
        /// 处理Http结果
        /// </summary>
        /// <param name="httpResult"></param>
        /// <returns></returns>
        /// <exception cref="WeChatException"></exception>
        private JsonData HandlerHttpResult(string httpResult)
        {
            JsonData jsonData = JsonMapper.ToObject(httpResult);
            int? errcode = jsonData.GetInt("errcode");
            if (!errcode.HasValue) return jsonData;
            if (errcode.HasValue && errcode.Value == 0) return jsonData;
            throw GetWeChatException(jsonData);
        }
        /// <summary>
        /// 获得微信异常
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        private WeChatException GetWeChatException(JsonData jsonData)
        {
            int? errcode = jsonData.GetInt("errcode");
            string message = jsonData.GetString("errmsg");
            return new WeChatException(message, errcode.ToString());
        }
    }
}
