using ConfigCenter.Environment.DataTransmitModel.ConfigurationItem;
using ConfigCenter.Environment.HttpManage;
using ConfigCenter.Environment.PresentationModel.ConfigurationItem;
using Materal.APP.HttpClient;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPP.HttpClientImpl.Environment
{
    public class ConfigurationItemHttpClientImpl : EnvironmentHttpClient, IConfigurationItemManage
    {
        private const string _controllerUrl = "/api/ConfigurationItem/";
        public ConfigurationItemHttpClientImpl(IAuthorityManage authorityManage) : base(authorityManage)
        {
        }
        public async Task<ResultModel> AddConfigurationItemAsync(AddConfigurationItemRequestModel requestModel)
        {
            var resultModel = await SendPutAsync<ResultModel>($"{_controllerUrl}AddConfigurationItem", requestModel);
            return resultModel;
        }

        public async Task<ResultModel> EditConfigurationItemAsync(EditConfigurationItemRequestModel requestModel)
        {
            var resultModel = await SendPutAsync<ResultModel>($"{_controllerUrl}EditConfigurationItem", requestModel);
            return resultModel;
        }

        public async Task<ResultModel> DeleteConfigurationItemAsync(Guid id)
        {
            var resultModel = await SendDeleteAsync<ResultModel>($"{_controllerUrl}DeleteConfigurationItem", null, new Dictionary<string, string>
            {
                ["id"] = id.ToString()
            });
            return resultModel;
        }

        public async Task<ResultModel<ConfigurationItemDTO>> GetConfigurationItemInfoAsync(Guid id)
        {
            var resultModel = await SendGetAsync<ResultModel<ConfigurationItemDTO>>($"{_controllerUrl}GetConfigurationItemInfo", new Dictionary<string, string>
            {
                ["id"] = id.ToString()
            });
            return resultModel;
        }

        public async Task<ResultModel<List<ConfigurationItemListDTO>>> GetConfigurationItemListAsync(QueryConfigurationItemFilterRequestModel requestModel)
        {
            var resultModel = await SendPostAsync<ResultModel<List<ConfigurationItemListDTO>>>($"{_controllerUrl}GetConfigurationItemList", requestModel);
            return resultModel;
        }
    }
}
