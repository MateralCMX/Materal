using LitJson;
using Materal.Utils.Wechat.Model;
using Materal.Utils.Wechat.Model.OfficialAccount.Request;
using Materal.Utils.Wechat.Model.OfficialAccount.Result;

namespace Materal.Utils.Wechat
{
    /// <summary>
    /// 微信公众号帮助类
    /// </summary>
    public class WechatOfficialAccountHelper(WechatConfigModel config, IHttpHelper? httpHelper = null) : WechatHelper(config, httpHelper)
    {
        /// <summary>
        /// 根据Code获得网页AccessToken
        /// </summary>
        /// <param name="code">Code</param>
        /// <returns>OpenID</returns>
        public async Task<WebAssessTokenResultModel> GetWebAssessTokenByCodeAsync([Required] string code)
        {
            Dictionary<string, string> queryParams = new()
            {
                {"appid", Config.APPID},
                {"secret", Config.APPSECRET},
                {"code", code},
                {"grant_type", "authorization_code"},
            };
            string httpResult = await HttpHelper.SendGetAsync($"{Config.WechatAPIUrl}sns/oauth2/access_token", queryParams);
            JsonData jsonData = HandlerHttpResult(httpResult);
            WebAssessTokenResultModel result = new()
            {
                WebAssessToken = jsonData.GetString("access_token") ?? throw WechatHelper.GetWechatException(jsonData),
                OpenID = jsonData.GetString("openid") ?? throw WechatHelper.GetWechatException(jsonData),
                ExpiresIn = jsonData.GetInt("expires_in") ?? throw WechatHelper.GetWechatException(jsonData),
                RefreshToken = jsonData.GetString("refresh_token") ?? throw WechatHelper.GetWechatException(jsonData),
                Scope = jsonData.GetString("scope") ?? throw WechatHelper.GetWechatException(jsonData),
            };
            return result;
        }
        /// <summary>
        /// 发送模版消息
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task SendTemplateMessageAsync(SendTemplateMessageRequestModel requestModel)
        {
            // 文档地址：https://mp.weixin.qq.com/debug/cgi-bin/readtmpl?t=tmplmsg/faq_tmpl
            // 请求方式：POST https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=ACCESS_TOKEN
            requestModel.Validation();
            Dictionary<string, string> queryParams = new()
            {
                {"access_token", requestModel.AccessToken}
            };
            var data = new
            {
                touser = requestModel.UserOpenID,
                template_id = requestModel.TemplateID,
                url = requestModel.Url,
                data = new Dictionary<string, object>(),
                miniprogram = requestModel.Miniprogram is null ? null : new
                {
                    appid = requestModel.Miniprogram.AppID,
                    pagepath = requestModel.Miniprogram.PagePath
                },
                client_msg_id = requestModel.ClientMessageID
            };
            foreach (KeyValueModel templateData in requestModel.TemplateDatas)
            {
                data.data.Add(templateData.Key, new
                {
                    value = templateData.Value
                });
            }
            string httpResult = await HttpHelper.SendPostAsync($"{Config.WechatAPIUrl}cgi-bin/message/template/send", queryParams, data);
            HandlerHttpResult(httpResult);
        }
    }
}
