using Materal.Gateway.Common;
using Materal.Gateway.Model;
using Materal.Gateway.OcelotExtension;
using Materal.Gateway.OcelotExtension.Services;
using System.Text;
using System.Text.Json;

namespace Materal.Gateway
{
    /// <summary>
    /// 服务发现
    /// </summary>
    public class ConsulManager
    {
        private readonly static IOcelotConfigService _ocelotConfigService;
        private readonly static HttpClient _httpClient;
        static ConsulManager()
        {
            _httpClient = new HttpClient();
            _ocelotConfigService = OcelotService.GetService<IOcelotConfigService>();
        }
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static ConsulServiceModel? GetService(string scheam = "http", Func<ConsulServiceModel, bool>? filter = null)
        {
            List<ConsulServiceModel> consulServices = GetServices(scheam, filter);
            return consulServices.FirstOrDefault();
        }
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static List<ConsulServiceModel> GetServices(string scheam = "http", Func<ConsulServiceModel, bool>? filter = null)
        {
            if (_ocelotConfigService.OcelotConfig.GlobalConfiguration.ServiceDiscoveryProvider == null) return new();
            string url = $"{scheam}://{_ocelotConfigService.OcelotConfig.GlobalConfiguration.ServiceDiscoveryProvider.Host}:{_ocelotConfigService.OcelotConfig.GlobalConfiguration.ServiceDiscoveryProvider.Port}/v1/agent/services";
            string requestText = SendHttpGet(url);
            JsonDocument jsonDocument = JsonDocument.Parse(requestText);
            if (jsonDocument.RootElement.ValueKind != JsonValueKind.Object) throw new GatewayException("ConsulServices返回错误");
            JsonElement.ObjectEnumerator element = jsonDocument.RootElement.EnumerateObject();
            List<JsonProperty> jsonProperties = element.ToList();
            List<string> jsonTexts = jsonProperties.Select(jsonProperty => jsonProperty.Value.ToString()).ToList();
            List<ConsulServiceModel> result = jsonTexts.Select(jsonText => jsonText.JsonToObject<ConsulServiceModel>() ?? throw new GatewayException("反序列化失败")).ToList();
            if (filter != null)
            {
                result = result.Where(filter).ToList();
            }
            return result;
        }
        private static string SendHttpGet(string url)
        {
            HttpRequestMessage httpRequest = new()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get
            };
            HttpResponseMessage response = _httpClient.Send(httpRequest);
            using Stream responseContent = response.Content.ReadAsStream();
            using MemoryStream memoryStream = new();
            responseContent.CopyTo(memoryStream);
            byte[] responseContentBuffer = memoryStream.ToArray();
            string result = Encoding.UTF8.GetString(responseContentBuffer);
            return result;
        }
    }
}
