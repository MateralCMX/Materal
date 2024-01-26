using Materal.Utils.Http;
using RC.EnvironmentServer.Abstractions.DTO.ConfigurationItem;
using RC.EnvironmentServer.Abstractions.RequestModel.ConfigurationItem;

namespace RC.ConfigClient
{
    /// <summary>
    /// 配置项HTTP客户端
    /// </summary>
    public class ConfigurationItemHttpClient(string url)
    {
        private static readonly HttpHelper _httpHelper = new();
        private readonly string _url = url;
        /// <summary>
        /// 添加配置项
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        /// <exception cref="MateralException"></exception>
        public async Task<ICollection<ConfigurationItemListDTO>?> GetDataAsync(QueryConfigurationItemRequestModel requestModel)
        {
            string url = _url;
            if (!_url.EndsWith('/'))
            {
                url += "/";
            }
            url += $"api/ConfigurationItem/GetList";
            string httpResult = await _httpHelper.SendPostAsync(url, null, requestModel);
            PageResultModel<ConfigurationItemListDTO> result = httpResult.JsonToObject<PageResultModel<ConfigurationItemListDTO>>();
            if (result.ResultType == ResultTypeEnum.Success) return result.Data;
            throw new MateralException("获取配置项失败");
        }
    }
}