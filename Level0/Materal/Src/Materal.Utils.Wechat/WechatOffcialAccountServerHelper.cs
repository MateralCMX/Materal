using Materal.Utils.Wechat.Model;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Materal.Utils.Wechat
{
    /// <summary>
    /// 微信公众号服务帮助类
    /// </summary>
    public class WechatOffcialAccountServerHelper
    {
        private readonly string _token;
        private readonly IServiceProvider _serviceProvider;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="token"></param>
        /// <param name="serviceProvider"></param>
        public WechatOffcialAccountServerHelper(string token, IServiceProvider serviceProvider)
        {
            _token = token;
            _serviceProvider = serviceProvider;
        }
        /// <summary>
        /// 获得签名
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public string GetSignature(string timestamp, string nonce)
        {
            string[] arr = new[] { _token, timestamp, nonce }.OrderBy(m => m).ToArray();
            string arrString = string.Join("", arr);
            SHA1 sha1 = SHA1.Create();
            byte[] sha1Buffer = sha1.ComputeHash(Encoding.UTF8.GetBytes(arrString));
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
            string eventValue = GetEventValue(xmlDocument);
            string eventHandlerName = $"I{eventValue}EventHandler";
            Type? eventHandlerType = eventHandlerName.GetTypeByTypeName(m => m.Name.Equals(eventHandlerName, StringComparison.OrdinalIgnoreCase));
            if (eventHandlerType == null) return null;
            object? eventHandler = _serviceProvider.GetService(eventHandlerType);
            if (eventHandler == null) return null;
            string eventName = $"{eventValue}Event";
            Type? eventType = eventName.GetTypeByTypeName(new object[] { xmlDocument });
            if (eventType == null) return null;
            object @event = eventType.Instantiation(new object[] { xmlDocument });
            MethodInfo? methodInfo = eventHandler.GetType().GetMethod("HandlerAsync");
            if (methodInfo == null) return null;
            object handlerResult = methodInfo.Invoke(eventHandler, new object[] { @event });
            if(handlerResult is Task<ReplyMessageModel?> task)
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
        public string GetEventValue(XmlDocument xmlDocument)
        {
            if (xmlDocument.FirstChild == null) throw new WechatException("未识别xml文档");
            try
            {
                XmlNodeList? eventNodes = xmlDocument.FirstChild.SelectNodes("Event");
                XmlNode eventNode;
                if (eventNodes != null && eventNodes.Count > 0)
                {
                    eventNode = eventNodes[0];
                    return eventNode.FirstChild.Value.FirstUpper();
                }
                eventNodes = xmlDocument.FirstChild.SelectNodes("MsgId");
                if (eventNodes != null && eventNodes.Count > 0)
                {
                    eventNodes = xmlDocument.FirstChild.SelectNodes("MsgType");
                    if (eventNodes != null && eventNodes.Count > 0)
                    {
                        // https://developers.weixin.qq.com/doc/offiaccount/Message_Management/Receiving_standard_messages.html
                        eventNode = eventNodes[0];
                        string messageType = eventNode.FirstChild.Value;
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
