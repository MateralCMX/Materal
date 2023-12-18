using Materal.Utils.Http;
using System.Collections;
using System.Net;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Materal.Utils.WebServiceClient
{
    /// <summary>
    /// WebService客户端
    /// </summary>
    public class WebServiceClient : IWebServiceClient
    {
        private readonly IHttpHelper _httpHelper;
        /// <summary>
        /// 构造方法
        /// </summary>
        public WebServiceClient(IHttpHelper? httpHelper = null)
        {
            httpHelper ??= new HttpHelper();
            _httpHelper = httpHelper;
        }
        /// <summary>
        /// 发送Soap请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="serviceName"></param>
        /// <param name="serviceNamespace"></param>
        /// <param name="data"></param>
        /// <param name="soapVersion"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string> SendSoapAsync(string url, string serviceName, string serviceNamespace, Dictionary<string, object?>? data = null, SoapVersionEnum soapVersion = SoapVersionEnum.Soap1_2) => soapVersion == SoapVersionEnum.Soap1_1
                ? await SendSoap1_1Async(url, serviceName, serviceNamespace, data)
                : await SendSoap1_2Async(url, serviceName, serviceNamespace, data);
        /// <summary>
        /// 发送Soap请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="serviceName"></param>
        /// <param name="serviceNamespace"></param>
        /// <param name="data"></param>
        /// <param name="soapVersion"></param>
        /// <returns></returns>
        public async Task<T?> SendSoapAsync<T>(string url, string serviceName, string serviceNamespace, Dictionary<string, object?>? data = null, SoapVersionEnum soapVersion = SoapVersionEnum.Soap1_2)
            => soapVersion == SoapVersionEnum.Soap1_1
                ? await SendSoap1_1Async<T>(url, serviceName, serviceNamespace, data)
                : await SendSoap1_2Async<T>(url, serviceName, serviceNamespace, data);
        /// <summary>
        /// 发送Soap1.1请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="serviceName"></param>
        /// <param name="serviceNamespace"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<string> SendSoap1_1Async(string url, string serviceName, string serviceNamespace, Dictionary<string, object?>? data = null)
        {
            XmlDocument xmlObject = new();
            xmlObject.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"><soap:Body></soap:Body></soap:Envelope>");
            XmlNode requestNode = GetRequestNode(xmlObject, serviceName, serviceNamespace, data);
            xmlObject.ChildNodes[1]?.ChildNodes[0]?.AppendChild(requestNode);
            string xmlContent = xmlObject.OuterXml;
            HttpContent httpContent = new StringContent(xmlContent, Encoding.UTF8, "text/xml");
            string SOAPAction = serviceNamespace;
            if (SOAPAction.EndsWith("/"))
            {
                SOAPAction = SOAPAction[..^1];
            }
            SOAPAction = $"{SOAPAction}/{serviceName}";
            string responseMessage = await _httpHelper.SendHttpContentAsync(url, HttpMethod.Post, httpContent, new()
            {
                ["ContentType"] = "text/xml; charset=utf-8",
                ["SOAPAction"] = SOAPAction
            }, HttpVersion.Version11);
            return responseMessage;
        }
        /// <summary>
        /// 发送Soap1.1请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="serviceName"></param>
        /// <param name="serviceNamespace"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<T?> SendSoap1_1Async<T>(string url, string serviceName, string serviceNamespace, Dictionary<string, object?>? data = null)
        {
            T? result = default;
            string responseMessage = await SendSoap1_1Async(url, serviceName, serviceNamespace, data);
            XmlDocument xmlObject = new();
            xmlObject.LoadXml(responseMessage);
            if (xmlObject.ChildNodes.Count != 2) return result;
            XmlNode resultNode = xmlObject.ChildNodes[1];
            if (resultNode.Name != "soap:Envelope") throw new MateralHttpException("未找到节点soap:Envelope");
            resultNode = resultNode.ChildNodes[0];
            if (resultNode.Name != "soap:Body") throw new MateralHttpException("未找到节点soap:Body");
            resultNode = resultNode.ChildNodes[0];
            return GetResult<T>(resultNode, serviceName, serviceNamespace);
        }
        /// <summary>
        /// 发送Soap1.2请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="serviceName"></param>
        /// <param name="serviceNamespace"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<string> SendSoap1_2Async(string url, string serviceName, string serviceNamespace, Dictionary<string, object?>? data = null)
        {
            XmlDocument xmlObject = new();
            xmlObject.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><soap12:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap12=\"http://www.w3.org/2003/05/soap-envelope\"><soap12:Body></soap12:Body></soap12:Envelope>");
            XmlNode requestNode = GetRequestNode(xmlObject, serviceName, serviceNamespace, data);
            xmlObject.ChildNodes[1]?.ChildNodes[0]?.AppendChild(requestNode);
            string xmlContent = xmlObject.InnerXml;
            HttpContent httpContent = new StringContent(xmlContent, Encoding.UTF8, "application/soap+xml");
            string responseMessage = await _httpHelper.SendHttpContentAsync(url, HttpMethod.Post, httpContent, new()
            {
                ["ContentType"] = "application/soap+xml; charset=utf-8"
            }, HttpVersion.Version11);
            return responseMessage;
        }
        /// <summary>
        /// 发送Soap1.2请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="serviceName"></param>
        /// <param name="serviceNamespace"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<T?> SendSoap1_2Async<T>(string url, string serviceName, string serviceNamespace, Dictionary<string, object?>? data = null)
        {
            T? result = default;
            string responseMessage = await SendSoap1_2Async(url, serviceName, serviceNamespace, data);
            XmlDocument xmlObject = new();
            xmlObject.LoadXml(responseMessage);
            if (xmlObject.ChildNodes.Count != 2) return result;
            XmlNode resultNode = xmlObject.ChildNodes[1];
            if (resultNode.Name != "soap12:Envelope" && resultNode.Name != "soap:Envelope") throw new MateralHttpException("未找到节点soap12:Envelope");
            resultNode = resultNode.ChildNodes[0];
            if (resultNode.Name != "soap12:Body" && resultNode.Name != "soap:Body") throw new MateralHttpException("未找到节点soap12:Body");
            resultNode = resultNode.ChildNodes[0];
            return GetResult<T>(resultNode, serviceName, serviceNamespace);
        }
        /// <summary>
        /// 获得请求节点
        /// </summary>
        /// <param name="xmlObject"></param>
        /// <param name="serviceName"></param>
        /// <param name="serviceNamespace"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private XmlNode GetRequestNode(XmlDocument xmlObject, string serviceName, string serviceNamespace, Dictionary<string, object?>? data)
        {
            XmlNode requestNode = xmlObject.CreateElement(serviceName, serviceNamespace);
            if(data is not null && data.Count > 0)
            {
                foreach (KeyValuePair<string, object?> item in data)
                {
                    XmlNode? dataNode = GetRequestNode(xmlObject, item, serviceNamespace);
                    if (dataNode is null) continue;
                    requestNode.AppendChild(dataNode);
                }
            }
            return requestNode;
        }
        /// <summary>
        /// 获得请求节点
        /// </summary>
        /// <param name="xmlObject"></param>
        /// <param name="data"></param>
        /// <param name="serviceNamespace"></param>
        /// <returns></returns>
        private XmlNode? GetRequestNode(XmlDocument xmlObject, KeyValuePair<string, object?> data, string serviceNamespace)
        {
            XmlNode dataNode = xmlObject.CreateElement(data.Key, serviceNamespace);
            if (data.Value is null) return null;
            Type valueType = data.Value.GetType();
            if (!valueType.IsClass || valueType == typeof(string))
            {
                XmlText textNode = xmlObject.CreateTextNode(data.Value.ToString());
                dataNode.AppendChild(textNode);
                return dataNode;
            }
            if (data.Value is ICollection collection)
            {
                List<XmlNode> subNodes = GetRequestNodes(xmlObject, collection, serviceNamespace);
                foreach (XmlNode subNode in subNodes)
                {
                    dataNode.AppendChild(subNode);
                }
                return dataNode;
            }
            PropertyInfo[] propertyInfos = valueType.GetProperties();
            foreach (PropertyInfo item in propertyInfos)
            {
                object? value = item.GetValue(data.Value);
                XmlNode? subDataNode;
                if (value is null)
                {
                    continue;
                }
                else
                {
                    subDataNode = GetRequestNode(xmlObject, new(item.Name, value), serviceNamespace);
                    if (subDataNode is null) continue;
                }
                dataNode.AppendChild(subDataNode);
            }
            return dataNode;
        }
        /// <summary>
        /// 获得请求节点集合
        /// </summary>
        /// <param name="xmlObject"></param>
        /// <param name="data"></param>
        /// <param name="serviceNamespace"></param>
        /// <returns></returns>
        private List<XmlNode> GetRequestNodes(XmlDocument xmlObject, ICollection data, string serviceNamespace)
        {
            static string GetTypeName(object item)
            {
                Type type = item.GetType();
                return type.Name switch
                {
                    "Int32" => "int",
                    _ => type.Name,
                };
            }
            List<XmlNode> result = [];
            foreach (object? item in data)
            {
                if (item is null) continue;
                XmlNode? xmlNode = GetRequestNode(xmlObject, new(GetTypeName(item), item), serviceNamespace);
                if (xmlNode is null) continue;
                result.Add(xmlNode);
            }
            return result;
        }
        /// <summary>
        /// 获得返回
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resultNode"></param>
        /// <param name="serviceName"></param>
        /// <param name="serviceNamespace"></param>
        /// <returns></returns>
        private T? GetResult<T>(XmlNode resultNode, string serviceName, string serviceNamespace)
        {
            if (resultNode.Name != $"{serviceName}Response") throw new MateralHttpException($"未找到节点{serviceName}Response");
            bool isOK = false;
            foreach (XmlAttribute item in resultNode.Attributes)
            {
                if (item.Name != "xmlns" || item.Value != serviceNamespace) continue;
                isOK = true;
                break;
            }
            if (!isOK) throw new MateralHttpException("服务命名空间错误");
            Type tType = typeof(T);
            object? nodeValue = GetNodeValue(resultNode.FirstChild, tType);
            if (nodeValue is not T result) throw new MateralHttpException("转换结果失败");
            return result;
        }
        /// <summary>
        /// 获得节点值
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <param name="propertyType"></param>
        /// <returns></returns>
        private object? GetNodeValue(XmlNode xmlNode, Type propertyType)
        {
            if (xmlNode is null) return null;
            if (!propertyType.IsClass || propertyType == typeof(string)) return xmlNode.InnerText.ConvertTo(propertyType);
            if (propertyType.IsAssignableTo<ICollection>()) return GetNodeValueCollection(xmlNode, propertyType);
            PropertyInfo[] propertyInfos = propertyType.GetProperties();
            object? result = propertyType.Instantiation();
            if(xmlNode.ChildNodes is not null)
            {
                foreach (XmlNode item in xmlNode.ChildNodes)
                {
                    PropertyInfo? propertyInfo = propertyInfos.FirstOrDefault(m => m.Name == item.Name);
                    if (propertyInfo is null || !propertyInfo.CanWrite) continue;
                    object? value = GetNodeValue(item, propertyInfo.PropertyType);
                    propertyInfo.SetValue(result, value);
                }
            }
            return result;
        }
        /// <summary>
        /// 获得节点集合
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <param name="propertyType"></param>
        /// <returns></returns>
        private ICollection GetNodeValueCollection(XmlNode xmlNode, Type propertyType)
        {
            Type elementType;
            Type listType;
            Func<IList, ICollection>? GetCollection = null;
            if (propertyType.IsAssignableTo<Array>())
            {
                elementType = propertyType.GetElementType();
                listType = typeof(List<>).MakeGenericType(elementType);
                MethodInfo? toArray = listType.GetMethod("ToArray");
                if(toArray != null)
                {
                    GetCollection = m =>
                    {
                        object convertValue = toArray.Invoke(m, new object[] { });
                        if (convertValue is not ICollection value || convertValue.GetType() != propertyType) throw new MateralHttpException("转换为数组失败");
                        return value;
                    };
                }
            }
            else if (propertyType.IsAssignableTo<IList>() && propertyType.IsGenericType && propertyType.GenericTypeArguments.Length == 1)
            {
                elementType = propertyType.GetGenericArguments().First();
                listType = typeof(List<>).MakeGenericType(elementType);
            }
            else throw new MateralHttpException("不支持的集合类型");
            IList result = listType.Instantiation<IList>();
            foreach (XmlNode item in xmlNode.ChildNodes)
            {
                object? value = GetNodeValue(item, elementType);
                result.Add(value);
            }
            return GetCollection is not null ? GetCollection(result) : result;
        }
    }
}
