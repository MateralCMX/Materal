﻿using LitJson;
using Materal.Utils.Http;
using Materal.Utils.Wechat.Model;
using Materal.Utils.Wechat.Model.MiniProgram.Request;
using System.ComponentModel.DataAnnotations;

namespace Materal.Utils.Wechat
{
    /// <summary>
    /// 微信小程序管理器
    /// </summary>
    public class WechatMiniProgramHelper : WechatHelper
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="config"></param>
        /// <param name="httpHelper"></param>
        public WechatMiniProgramHelper(WechatConfigModel config, IHttpHelper? httpHelper = null) : base(config, httpHelper)
        {
        }
        /// <summary>
        /// 根据Code获得OpenID
        /// </summary>
        /// <param name="code">Code</param>
        /// <returns>OpenID</returns>
        public async Task<string> GetOpenIDByCodeAsync([Required]string code)
        {
            Dictionary<string, string> queryParams = new()
            {
                {"appid", Config.APPID},
                {"secret", Config.APPSECRET},
                {"grant_type", "authorization_code"},
                {"js_code", code},
            };
            string httpResult = await HttpHelper.SendGetAsync($"{Config.WechatAPIUrl}sns/jscode2session", queryParams);
            JsonData jsonData = WechatHelper.HandlerHttpResult(httpResult);
            string openID = jsonData.GetString("openid") ?? throw WechatHelper.GetWechatException(jsonData);
            return openID;
        }
        /// <summary>
        /// 订阅消息发送
        /// </summary>
        /// <returns></returns>
        /// <exception cref="WechatException"></exception>
        public async Task SubscribeMessageSendAsync(SubscribeMessageSendRequestModel requestModel)
        {
            requestModel.Validation();
            Dictionary<string, string> queryParams = new()
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
            string httpResult = await HttpHelper.SendPostAsync($"{Config.WechatAPIUrl}cgi-bin/message/subscribe/send", queryParams, data);
            WechatHelper.HandlerHttpResult(httpResult);
        }
    }
}
