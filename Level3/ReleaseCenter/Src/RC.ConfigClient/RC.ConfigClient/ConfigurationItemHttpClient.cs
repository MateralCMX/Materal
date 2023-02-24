using Materal.Abstractions;
using Materal.Utils.Http;
using Materal.Utils.Model;
using RC.EnvironmentServer.DataTransmitModel.ConfigurationItem;
using RC.EnvironmentServer.PresentationModel.ConfigurationItem;

namespace RC.ConfigClient
{
    public class ConfigurationItemHttpClient
    {
        private static readonly IHttpHelper _httpHelper;
        static ConfigurationItemHttpClient()
        {
            _httpHelper = new HttpHelper();
        }
        private readonly string _url;
        public ConfigurationItemHttpClient(string url)
        {
            _url = url;
        }
        /// <summary>
        /// 获得默认头部
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetDefaultHeaders() => new()
        {
            ["Content-Type"] = "application/json"
        };
        public async Task<ICollection<ConfigurationItemListDTO>?> GetDataAsync(QueryConfigurationItemRequestModel requestModel)
        {
            string url = $"{_url}/ConfigurationItem/GetList";
            string httpResult = await _httpHelper.SendPostAsync(url, null, requestModel, GetDefaultHeaders());
            PageResultModel<ConfigurationItemListDTO> result = httpResult.JsonToObject<PageResultModel<ConfigurationItemListDTO>>();
            if(result.ResultType == ResultTypeEnum.Success) return result.Data;
            throw new MateralException("获取配置项失败");
        }
    }
}