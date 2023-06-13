using Materal.Utils.Http;
using Materal.Utils.Wechat.Model;
using Materal.Utils.Wechat.Model.OfficialAccount.Request;
using System.Security.Cryptography;
using System.Text;

namespace Materal.Utils.Wechat
{
    /// <summary>
    /// 微信公众号帮助类
    /// </summary>
    public class WechatOfficialAccountHelper : WechatHelper
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="config"></param>
        /// <param name="httpHelper"></param>
        public WechatOfficialAccountHelper(WechatConfigModel config, IHttpHelper? httpHelper = null) : base(config, httpHelper)
        {
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
                data = new Dictionary<string, object>()
            };
            foreach (TemplateDataModel templateData in requestModel.TemplateDatas)
            {
                data.data.Add(templateData.Key, new
                {
                    value = templateData.Value,
                    color = templateData.Color
                });
            }
            string httpResult = await HttpHelper.SendPostAsync($"{Config.WechatAPIUrl}cgi-bin/message/template/send", queryParams, data);
            HandlerHttpResult(httpResult);
        }
    }
}
