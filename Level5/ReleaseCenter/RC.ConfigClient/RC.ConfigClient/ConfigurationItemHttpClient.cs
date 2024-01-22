using Materal.Abstractions;
using Materal.Extensions;
using Materal.Utils.Http;
using Materal.Utils.Model;
using RC.EnvironmentServer.DataTransmitModel.ConfigurationItem;
using RC.EnvironmentServer.PresentationModel.ConfigurationItem;

namespace RC.ConfigClient
{
    /// <summary>
    /// 配置项HTTP客户端
    /// </summary>
    public class ConfigurationItemHttpClient
    {
        private static readonly IHttpHelper _httpHelper;
        /// <summary>
        /// 静态构造方法
        /// </summary>
        static ConfigurationItemHttpClient()
        {
            _httpHelper = new HttpHelper();
        }
        private readonly string _url;
        /// <summary>
        /// 配置项HTTP客户端
        /// </summary>
        /// <param name="url"></param>
        public ConfigurationItemHttpClient(string url)
        {
            _url = url;
        }
        /// <summary>
        /// 添加配置项
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        /// <exception cref="MateralException"></exception>
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