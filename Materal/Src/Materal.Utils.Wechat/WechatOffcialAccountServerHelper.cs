using Materal.Utils.Wechat.Model;
using System.Reflection;
using System.Security.Cryptography;

namespace Materal.Utils.Wechat
{
    /// <summary>
    /// 微信公众号服务帮助类
    /// </summary>
    public class WechatOffcialAccountServerHelper(string token, IServiceProvider serviceProvider)
    {
        /// <summary>
        /// 获得签名
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public string GetSignature(string timestamp, string nonce)
        {
            string[] arr = [.. new[] { token, timestamp, nonce }.OrderBy(m => m)];
            string arrString = string.Join("", arr);
#if NET
            byte[] sha1Buffer = SHA1.HashData(Encoding.UTF8.GetBytes(arrString));
#else
            SHA1 sha1 = SHA1.Create();
            byte[] sha1Buffer = sha1.ComputeHash(Encoding.UTF8.GetBytes(arrString));
#endif
            StringBuilder enText = new();
            foreach (byte sha1Item in sha1Buffer)
            {
                enText.AppendFormat("{0:x2}", sha1Item);
            }
            return enText.ToString();
        }
        /// <summary>
        /// 是否是微信的请求
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        public bool IsWechatRequest(string timestamp, string nonce, string signature)
        {
            string trueSignature = GetSignature(timestamp, nonce);
            return trueSignature == signature;
        }
        /// <summary>
        /// 处理微信事件
        /// </summary>
        /// <returns></returns>
        public async Task<ReplyMessageModel?> HandlerWechatEventAsync(XmlDocument xmlDocument)
        {
            string eventValue = WechatOffcialAccountServerHelper.GetEventValue(xmlDocument);
            string eventHandlerName = $"I{eventValue}EventHandler";
            Type? eventHandlerType = TypeHelper.GetTypeByFilter(m => m.Name.Equals(eventHandlerName, StringComparison.OrdinalIgnoreCase));
            if (eventHandlerType is null) return null;
            object? eventHandler = serviceProvider.GetService(eventHandlerType);
            if (eventHandler is null) return null;
            string eventName = $"{eventValue}Event";
            Type? eventType = eventName.GetTypeByTypeName(new object[] { xmlDocument });
            if (eventType is null) return null;
            object @event = eventType.Instantiation([xmlDocument]);
            MethodInfo? methodInfo = eventHandler.GetType().GetMethod("HandlerAsync");
            if (methodInfo is null) return null;
            object? handlerResult = methodInfo.Invoke(eventHandler, new object[] { @event });
            if (handlerResult is Task<ReplyMessageModel?> task)
            {
                ReplyMessageModel? result = await task;
                return result;
            }
            return null;
        }
        /// <summary>
        /// 获得Event值
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <returns></returns>
        /// <exception cref="WechatException"></exception>
        /// <exception cref="Exception"></exception>
        public static string GetEventValue(XmlDocument xmlDocument)
        {
            if (xmlDocument.FirstChild is null) throw new WechatException("未识别xml文档");
            try
            {
                XmlNodeList? eventNodes = xmlDocument.FirstChild.SelectNodes("Event");
                XmlNode eventNode;
                if (eventNodes is not null && eventNodes.Count > 0)
                {
                    eventNode = eventNodes[0] ?? throw new WechatException("获取Event节点失败");
                    return eventNode.FirstChild?.Value?.FirstUpper() ?? throw new WechatException("获取Event节点值失败");
                }
                eventNodes = xmlDocument.FirstChild.SelectNodes("MsgId");
                if (eventNodes is not null && eventNodes.Count > 0)
                {
                    eventNodes = xmlDocument.FirstChild.SelectNodes("MsgType");
                    if (eventNodes is not null && eventNodes.Count > 0)
                    {
                        // https://developers.weixin.qq.com/doc/offiaccount/Message_Management/Receiving_standard_messages.html
                        eventNode = eventNodes[0] ?? throw new WechatException("获取Event节点失败");
                        string messageType = eventNode.FirstChild?.Value ?? throw new WechatException("获取Event节点值失败");
                        switch (messageType)
                        {
                            case "text":
                                return "TextMessage";
                            case "image":
                                return "ImageMessage";
                            case "voice":
                                return "VoiceMessage";
                            case "video":
                                return "VideoMessage";
                            case "shortvideo":
                                return "ShortVideoMessage";
                            case "location":
                                return "LocationMessage";
                            case "link":
                                return "LinkMessage";
                        }
                    }
                }
                throw new WechatException("未识别找到Event节点");
            }
            catch (Exception ex)
            {
                throw new WechatException("解析Xml出错", ex);
            }
        }
    }
}
