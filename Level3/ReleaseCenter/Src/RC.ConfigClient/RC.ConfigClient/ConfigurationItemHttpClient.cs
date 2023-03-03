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
        public async Task<ICollection<ConfigurationItemListDTO>?> GetDataAsync(QueryConfigurationItemRequestModel requestModel)
        {
            string url = _url;
            if (!_url.EndsWith("/"))
            {
                url += "/";
            }
            url += $"api/ConfigurationItem/GetList";
            string httpResult = await _httpHelper.SendPostAsync(url, null, requestModel);
            PageResultModel<ConfigurationItemListDTO> result = httpResult.JsonToObject<PageResultModel<ConfigurationItemListDTO>>();
            if(result.ResultType == ResultTypeEnum.Success) return result.Data;
            throw new MateralException("获取配置项失败");
        }
    }
}