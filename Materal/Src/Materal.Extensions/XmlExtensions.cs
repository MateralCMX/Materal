using Newtonsoft.Json;
using System.Xml;
using System.Xml.Serialization;

namespace Materal.Extensions
{
    /// <summary>
    /// XML扩展
    /// </summary>
    public static class XmlExtensions
    {
        /// <summary>
        /// XML文档对象转换为Json字符串
        /// </summary>
        /// <param name="xmlDoc">XML文档对象</param>
        /// <returns>Json字符串</returns>
        public static string ToJson(this XmlDocument xmlDoc) => JsonConvert.SerializeXmlNode(xmlDoc);
        /// <summary>
        /// XML节点对象转换为Json字符串
        /// </summary>
        /// <param name="xmlNode">XML节点对象</param>
        /// <returns>Json字符串</returns>
        public static string ToJson(this XmlNode xmlNode) => JsonConvert.SerializeXmlNode(xmlNode);
        /// <summary>
        /// 对象转换为Xml
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToXml(this object obj)
        {
            if (obj is IDictionary<string, object> dictionary) return dictionary.ToXml();
            XmlSerializer serializer;
            using StringWriter stringWriter = new();
            serializer = new(obj.GetType());
            serializer.Serialize(stringWriter, obj);
            return stringWriter.ToString();
        }
        /// <summary>
        /// 对象转换为Xml
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static string ToXml(this IDictionary<string, object> dictionary)
        {
            XmlDocument xmlDocument = new();
            XmlNode xmlNode = xmlDocument.CreateElement("Root");
            foreach (KeyValuePair<string, object> item in dictionary)
            {
                xmlNode.AppendChild(xmlDocument, item);
            }
            xmlDocument.AppendChild(xmlNode);
            return xmlDocument.OuterXml;
        }
        /// <summary>
        /// 添加子级
        /// </summary>
        /// <param name="node"></param>
        /// <param name="xmlDocument"></param>
        /// <param name="item"></param>
        private static void AppendChild(this XmlNode node, XmlDocument xmlDocument, KeyValuePair<string, object> item)
        {
            XmlNode newNode = xmlDocument.CreateElement(item.Key);
            if (item.Value is IDictionary<string, object> dictionary)
            {
                foreach (KeyValuePair<string, object> child in dictionary)
                {
                    newNode.AppendChild(xmlDocument, child);
                }
            }
            else
            {
                newNode.InnerText = item.Value.ToString() ?? string.Empty;
            }
            node.AppendChild(newNode);
        }
    }
}
