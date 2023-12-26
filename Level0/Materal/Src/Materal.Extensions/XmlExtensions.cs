using Newtonsoft.Json;
using System.Xml;

namespace System
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
    }
}
