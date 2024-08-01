using System.Text;
using System.Xml;

namespace Materal.Tools.Core.Extensions
{
    /// <summary>
    /// Xml文档扩展
    /// </summary>
    public static class XmlDocumentExtensions
    {
        /// <summary>
        /// 格式化xml内容
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string GetFormatXmlContent(this XmlDocument xmlDocument, Encoding? encoding = null)
        {
            if (encoding is null && xmlDocument.FirstChild is XmlDeclaration xmlDeclaration)
            {
                string encodingName = xmlDeclaration.Encoding;
                if (!string.IsNullOrEmpty(encodingName))
                {
                    encoding = Encoding.GetEncoding(encodingName);
                }
            }
            encoding ??= Encoding.UTF8;
            XmlWriterSettings settings = new()
            {
                Indent = true,
                IndentChars = "\t",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace,
                Encoding = encoding
            };
            string result = string.Empty;
            using (MemoryStream memoryStream = new())
            {
                using (StreamWriter streamWriter = new(memoryStream, encoding))
                {
                    using XmlWriter xmlWriter = XmlWriter.Create(streamWriter, settings);
                    xmlDocument.Save(xmlWriter);
                }
                result = encoding.GetString(memoryStream.ToArray());
            }
            while (result[0] != '<')
            {
                result = result[1..];
            }
            return result;
        }
    }
}
