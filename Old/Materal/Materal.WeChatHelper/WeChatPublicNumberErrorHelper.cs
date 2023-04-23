using LitJson;
using Materal.WeChatHelper.Model;

namespace Materal.WeChatHelper
{
    public class WeChatPublicNumberErrorHelper
    {
        public static WeChatException GetWeChatException(JsonData jsonData)
        {
            return new WeChatException(jsonData["errmsg"].ToString(), jsonData["errcode"].ToString());
        }
        /// <summary>
        /// 是否是错误
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        public static bool IsError(JsonData jsonData)
        {
            if (!jsonData.ContainsKey("errcode")) return false;
            return jsonData["errcode"].ToString() != "0";
        }
    }
}
