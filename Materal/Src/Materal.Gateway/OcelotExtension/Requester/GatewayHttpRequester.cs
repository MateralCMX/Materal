using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Ocelot.Middleware;
using Ocelot.Request.Middleware;
using Ocelot.Requester;
using Ocelot.Responses;
using System.Xml;
using Error = Ocelot.Errors.Error;

namespace Materal.Gateway.OcelotExtension.Requester
{
    /// <summary>
    /// 网关HTTP请求器
    /// </summary>
    public partial class GatewayHttpRequester(IMessageInvokerPool messageHandlerPool, IExceptionToErrorMapper mapper)
    {
        /// <summary>
        /// 获得响应
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task<Response<HttpResponseMessage>> GetResponse(HttpContext httpContext)
        {
            DownstreamRequest downstreamRequest = httpContext.Items.DownstreamRequest();
            HttpMessageInvoker messageInvoker = messageHandlerPool.Get(httpContext.Items.DownstreamRoute());
            try
            {
                HttpRequestMessage httpRequestMessage = downstreamRequest.ToHttpRequestMessage();
                HttpResponseMessage response = await messageInvoker.SendAsync(httpRequestMessage, httpContext.RequestAborted);
                HttpResponseMessage result = await ConvertResponseAsync(httpContext, response);
                return new OkResponse<HttpResponseMessage>(response);
            }
            catch (Exception exception)
            {
                Error error = mapper.Map(exception);
                return new ErrorResponse<HttpResponseMessage>(error);
            }
        }
    }
    /// <summary>
    /// 网关HTTP请求器
    /// </summary>
    public partial class GatewayHttpRequester : IHttpRequester
    {
        private const string RsultTypeKey = "ResultType";
        /// <summary>
        /// 转换Response
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        private static async Task<HttpResponseMessage> ConvertResponseAsync(HttpContext httpContext, HttpResponseMessage response)
        {
            KeyValuePair<string, StringValues>? resultTypeHeadr = httpContext.Request.Headers.FirstOrDefault(m => m.Key == RsultTypeKey);
            if (resultTypeHeadr == null || resultTypeHeadr.Value.Value.Count != 1) return response;
            string? resultType = resultTypeHeadr.Value.Value.First()?.ToLower();
            if (string.IsNullOrWhiteSpace(resultType)) return response;
            string? responseContentType = response.Content.Headers.ContentType?.MediaType?.ToLower();
            if (string.IsNullOrWhiteSpace(responseContentType)) return response;
            if (resultType == "xml")
            {
                switch (responseContentType)
                {
                    case "application/json": return await JsonToXmlAsync(response);
                }
            }
            else if (resultType == "json")
            {
                switch (responseContentType)
                {
                    case "application/xml": return await XmlToJsonAsync(response);
                }
            }
            return response;
        }
        /// <summary>
        /// 获得转换后的响应消息
        /// </summary>
        /// <param name="response"></param>
        /// <param name="convertFunc"></param>
        /// <returns></returns>
        private static async Task<HttpResponseMessage> GetConvertHttpResponseMessage(HttpResponseMessage response, Func<byte[], Encoding, (byte[]?, string?)> convertFunc)
        {
            if (response.Content.Headers.ContentType == null) return response;
            byte[] bodyBuffer;
            using MemoryStream readResponseStream = new();
            await response.Content.CopyToAsync(readResponseStream);
            bodyBuffer = readResponseStream.ToArray();
            string? charSet = response.Content.Headers.ContentType.CharSet;
            Encoding encoding = string.IsNullOrEmpty(charSet) ? Encoding.UTF8 : Encoding.GetEncoding(charSet);
            (byte[]? convertBodyBuffer, string? contentType) = convertFunc(bodyBuffer, encoding);
            if (convertBodyBuffer == null || string.IsNullOrWhiteSpace(contentType)) return response;
            MemoryStream memoryStream = new(convertBodyBuffer);
            HttpContent httpContent = new StreamContent(memoryStream);
            foreach (KeyValuePair<string, IEnumerable<string>> header in response.Content.Headers)
            {
                if (header.Key == "Content-Type")
                {
                    httpContent.Headers.Add(header.Key, [$"{contentType};charset={charSet}"]);
                }
                else if (header.Key == "Content-Length")
                {
                    httpContent.Headers.Add(header.Key, [convertBodyBuffer.Length.ToString()]);
                }
                else
                {
                    httpContent.Headers.Add(header.Key, header.Value);
                }
            }
            HttpResponseMessage result = new(response.StatusCode)
            {
                Content = httpContent,
                Version = response.Version,
                ReasonPhrase = response.ReasonPhrase,
                RequestMessage = response.RequestMessage
            };
            foreach (KeyValuePair<string, IEnumerable<string>> header in response.Headers)
            {
                result.Headers.Add(header.Key, header.Value);
            }
            foreach (KeyValuePair<string, IEnumerable<string>> header in response.TrailingHeaders)
            {
                result.TrailingHeaders.Add(header.Key, header.Value);
            }
            return result;
        }
        /// <summary>
        /// Json转换为Xml
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private static async Task<HttpResponseMessage> JsonToXmlAsync(HttpResponseMessage response)
        {
            return await GetConvertHttpResponseMessage(response, (bodyBuffer, encoding) =>
            {
                string json = encoding.GetString(bodyBuffer);
                XmlDocument? doc = JsonConvert.DeserializeXmlNode(json, "Root");
                if (doc == null) return (null, null);
                string xml = doc.InnerXml;
                bodyBuffer = encoding.GetBytes(xml);
                return (bodyBuffer, "application/xml");
            });
        }
        /// <summary>
        /// Xml转换为Json
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private static async Task<HttpResponseMessage> XmlToJsonAsync(HttpResponseMessage response)
        {
            return await GetConvertHttpResponseMessage(response, (bodyBuffer, encoding) =>
            {
                string xml = encoding.GetString(bodyBuffer);
                XmlDocument doc = new();
                doc.LoadXml(xml);
                string json = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None, true);
                bodyBuffer = encoding.GetBytes(json);
                return (bodyBuffer, "application/json");
            });
        }
    }
}
