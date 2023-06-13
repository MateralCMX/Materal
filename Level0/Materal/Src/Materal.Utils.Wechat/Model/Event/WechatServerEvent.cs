﻿using System.Xml;

namespace Materal.Utils.Wechat.Model.Event
{
    /// <summary>
    /// 微信服务事件
    /// </summary>
    public abstract class WechatServerEvent
    {
        /// <summary>
        /// 原始Xml
        /// </summary>
        public XmlDocument XmlDocument { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="xmlDocument"></param>
        protected WechatServerEvent(XmlDocument xmlDocument)
        {
            XmlDocument = xmlDocument;
        }
        /// <summary>
        /// 获得Xml值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="WechatException"></exception>
        protected string GetXmlValue(string name)
        {
            if (XmlDocument.FirstChild == null) throw new WechatException("未识别xml文档");
            XmlNodeList? nodes = XmlDocument.FirstChild.SelectNodes(name);
            if (nodes == null || nodes.Count <= 0 || nodes[0] == null) return string.Empty;
            XmlNode node = nodes[0];
            return node.FirstChild.Value;
        }
        /// <summary>
        /// 获得Xml值(时间格式)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected DateTime GetXmlValueForDateTime(string name)
        {
            string dateTimeString = GetXmlValue(name);
            if (string.IsNullOrWhiteSpace(dateTimeString)) throw new WechatException("时间值错误");
            long dateTimeSecond = Convert.ToInt64(dateTimeString);
            DateTime result = new(1970, 1, 1);
            result = result.AddHours(8).AddSeconds(dateTimeSecond);
            return result;
        }
    }
}
