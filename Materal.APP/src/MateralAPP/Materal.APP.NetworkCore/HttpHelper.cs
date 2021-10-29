using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Materal.APP.Core;
using Materal.APP.Core.Models;
using Materal.Common;
using Materal.ConvertHelper;
using Materal.Model;
using Materal.NetworkHelper;

namespace Materal.APP.NetworkCore
{
    public static class HttpHelper
    {
        /// <summary>
        /// 发送Post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<T> SendPostAsync<T>(string url, object data, string token = null)
        {
            string trueUrl = $"{ApplicationConfig.GatewayConfig.Address}/{url}";
            string resultText = await HttpManager.SendPostAsync(trueUrl, data, null, GetHeads(token), Encoding.UTF8);
            ResultModel<T> resultObject = resultText.JsonToObject<ResultModel<T>>();
            if (resultObject.ResultType == ResultTypeEnum.Success)
            {
                return resultObject.Data;
            }
            throw new MateralAPPException(resultObject.Message);
        }
        /// <summary>
        /// 获得请求头
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetHeads(string token)
        {
            var result = new Dictionary<string, string>
            {
                ["Accept"] = "*",
                ["Content-Type"] = "application/json"
            };
            if (!string.IsNullOrEmpty(token))
            {
                result.Add("Authorization", $"Bearer {token}");
            }
            return result;
        }
    }
}
