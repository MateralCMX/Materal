using LitJson;
using Materal.WeChatHelper.Model;
using System;
using System.Collections.Generic;

namespace Materal.WeChatHelper
{
    public class WeChatPublicNumberManager
    {
        /// <summary>
        /// 配置文件
        /// </summary>
        protected readonly WeChatConfigModel Config;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="configM"></param>
        public WeChatPublicNumberManager(WeChatConfigModel configM)
        {
            Config = configM;
        }
        /// <summary>
        /// 根据Code获得OpenID
        /// </summary>
        /// <returns>OpenID</returns>
        public AccessTokenResultModel GetAccessToken()
        {
            var data = new WeChatDataModel();
            data.SetValue("appid", Config.APPID);
            data.SetValue("secret", Config.APPSECRET);
            data.SetValue("grant_type", "client_credential");
            string url = $"{Config.WeChatAPIUrl}cgi-bin/token?{data.ToUrlParams()}";
            string weChatResult = WeChatHttpManager.Get(url);
            JsonData jsonData = JsonMapper.ToObject(weChatResult);
            if (WeChatPublicNumberErrorHelper.IsError(jsonData)) throw WeChatPublicNumberErrorHelper.GetWeChatException(jsonData);
            var accessToken = new AccessTokenResultModel
            {
                AccessToken = jsonData["access_token"].ToString(),
                ExpiresIn = Convert.ToInt32(jsonData["expires_in"].ToString())
            };
            return accessToken;
        }
        /// <summary>
        /// 获取微信IP地址
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public List<string> GetWeChatIPAddress(string accessToken)
        {
            var data = new WeChatDataModel();
            data.SetValue("access_token", accessToken);
            string url = $"{Config.WeChatAPIUrl}cgi-bin/getcallbackip?{data.ToUrlParams()}";
            string weChatResult = WeChatHttpManager.Get(url);
            JsonData jsonData = JsonMapper.ToObject(weChatResult);
            if (WeChatPublicNumberErrorHelper.IsError(jsonData)) throw WeChatPublicNumberErrorHelper.GetWeChatException(jsonData);
            JsonData ipList = jsonData["ip_list"];
            var result = new List<string>();
            for (var i = 0; i < ipList.Count; i++)
            {
                result.Add(ipList[i].ToString());
            }
            return result;
        }
        /// <summary>
        /// 网络检测
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="action"></param>
        /// <param name="operatorEnum"></param>
        /// <returns></returns>
        public NetworkDetectionResultModel NetworkDetection(string accessToken, NetworkDetectionActionEnum action = NetworkDetectionActionEnum.All, NetworkDetectionOperatorEnum operatorEnum = NetworkDetectionOperatorEnum.Default)
        {
            var data = new WeChatDataModel();
            data.SetValue("access_token", accessToken);
            string url = $"{Config.WeChatAPIUrl}cgi-bin/callback/check?{data.ToUrlParams()}";
            var postData = new WeChatDataModel();
            postData.SetValue("action", action.ToString().ToLower());
            postData.SetValue("check_operator", operatorEnum.ToString().ToUpper());
            string weChatResult = WeChatHttpManager.PostJson(url, postData.ToObject(), false, 3000, Config);
            JsonData jsonData = JsonMapper.ToObject(weChatResult);
            if (WeChatPublicNumberErrorHelper.IsError(jsonData)) throw WeChatPublicNumberErrorHelper.GetWeChatException(jsonData);
            var result = new NetworkDetectionResultModel();
            if (jsonData.ContainsKey("dns"))
            {
                result.DNS = new List<NetworkDetectionDNSModel>();
                JsonData dns = jsonData["dns"];
                for (var i = 0; i < dns.Count; i++)
                {
                    string operatorStr = dns[i]["real_operator"].ToString();
                    var realOperator = NetworkDetectionOperatorEnum.Default;
                    switch (operatorStr)
                    {
                        case "CAP":
                            realOperator = NetworkDetectionOperatorEnum.Cap;
                            break;
                        case "CHINANET":
                            realOperator = NetworkDetectionOperatorEnum.ChinaNet;
                            break;
                        case "UNICOM":
                            realOperator = NetworkDetectionOperatorEnum.UniCom;
                            break;
                    }
                    result.DNS.Add(new NetworkDetectionDNSModel
                    {
                        IP = dns[i]["ip"].ToString(),
                        RealOperator = realOperator
                    });
                }
            }
            if (jsonData.ContainsKey("ping"))
            {
                result.Ping = new List<NetworkDetectionPingModel>();
                JsonData ping = jsonData["ping"];
                for (var i = 0; i < ping.Count; i++)
                {
                    string operatorStr = ping[i]["from_operator"].ToString();
                    var fromOperator = NetworkDetectionOperatorEnum.Default;
                    switch (operatorStr)
                    {
                        case "CAP":
                            fromOperator = NetworkDetectionOperatorEnum.Cap;
                            break;
                        case "CHINANET":
                            fromOperator = NetworkDetectionOperatorEnum.ChinaNet;
                            break;
                        case "UNICOM":
                            fromOperator = NetworkDetectionOperatorEnum.UniCom;
                            break;
                    }
                    result.Ping.Add(new NetworkDetectionPingModel
                    {
                        IP = ping[i]["ip"].ToString(),
                        PackageLoss = ping[i]["package_loss"].ToString(),
                        Time = ping[i]["time"].ToString(),
                        FromOperator = fromOperator
                    });
                }
            }
            return result;
        }
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="model"></param>
        public void CreateMenu(string accessToken, CreateMenuModel model)
        {
            var data = new WeChatDataModel();
            data.SetValue("access_token", accessToken);
            string url = $"{Config.WeChatAPIUrl}cgi-bin/menu/create?{data.ToUrlParams()}";
            string weChatResult = WeChatHttpManager.PostJson(url, model, false, 3000, Config);
            JsonData jsonData = JsonMapper.ToObject(weChatResult);
            if (WeChatPublicNumberErrorHelper.IsError(jsonData)) throw WeChatPublicNumberErrorHelper.GetWeChatException(jsonData);
        }
    }
}
