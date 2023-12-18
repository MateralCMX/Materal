namespace Materal.Utils.WebServiceClient
{
    /// <summary>
    /// WebService客户端
    /// </summary>
    public interface IWebServiceClient
    {
        /// <summary>
        /// 发送Soap请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="serviceName"></param>
        /// <param name="serviceNamespace"></param>
        /// <param name="data"></param>
        /// <param name="soapVersion"></param>
        /// <returns></returns>
        Task<string> SendSoapAsync(string url, string serviceName, string serviceNamespace, Dictionary<string, object?>? data = null, SoapVersionEnum soapVersion = SoapVersionEnum.Soap1_2);
        /// <summary>
        /// 发送Soap1.1请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="serviceName"></param>
        /// <param name="serviceNamespace"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<string> SendSoap1_1Async(string url, string serviceName, string serviceNamespace, Dictionary<string, object?>? data = null);
        /// <summary>
        /// 发送Soap1.2请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="serviceName"></param>
        /// <param name="serviceNamespace"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<string> SendSoap1_2Async(string url, string serviceName, string serviceNamespace, Dictionary<string, object?>? data = null);
        /// <summary>
        /// 发送Soap请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="serviceName"></param>
        /// <param name="serviceNamespace"></param>
        /// <param name="data"></param>
        /// <param name="soapVersion"></param>
        /// <returns></returns>
        Task<T?> SendSoapAsync<T>(string url, string serviceName, string serviceNamespace, Dictionary<string, object?>? data = null, SoapVersionEnum soapVersion = SoapVersionEnum.Soap1_2);
        /// <summary>
        /// 发送Soap1.1请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="serviceName"></param>
        /// <param name="serviceNamespace"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<T?> SendSoap1_1Async<T>(string url, string serviceName, string serviceNamespace, Dictionary<string, object?>? data = null);
        /// <summary>
        /// 发送Soap1.2请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="serviceName"></param>
        /// <param name="serviceNamespace"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<T?> SendSoap1_2Async<T>(string url, string serviceName, string serviceNamespace, Dictionary<string, object?>? data = null);
    }
}
