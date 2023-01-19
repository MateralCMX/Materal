using Materal.Common;
using Materal.ConvertHelper;
using Materal.Model;
using Materal.NetworkHelper;
using Newtonsoft.Json.Linq;
using RC.EnvironmentServer.DataTransmitModel.ConfigurationItem;
using RC.EnvironmentServer.PresentationModel.ConfigurationItem;

namespace RC.ConfigClient
{
    public class ConfigurationItemHttpClient
    {
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
            string httpResult = await HttpManager.SendPostAsync(url, requestModel, null, GetDefaultHeaders());
            PageResultModel<ConfigurationItemListDTO> result = httpResult.JsonToObject<PageResultModel<ConfigurationItemListDTO>>();
            if(result.ResultType == ResultTypeEnum.Success) return result.Data;
            throw new MateralException("获取配置项失败");
        }
    }
}