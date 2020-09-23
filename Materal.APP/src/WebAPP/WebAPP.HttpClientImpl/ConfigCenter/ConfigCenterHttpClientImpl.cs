using ConfigCenter.DataTransmitModel.ConfigCenter;
using ConfigCenter.HttpManage;
using Materal.APP.HttpClient;
using Materal.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigCenter.PresentationModel.ConfigCenter;

namespace WebAPP.HttpClientImpl.ConfigCenter
{
    public class ConfigCenterHttpClientImpl : ConfigCenterHttpClient, IConfigCenterManage
    {
        private const string _controllerUrl = "/api/ConfigCenter/";
        public ConfigCenterHttpClientImpl(IAuthorityManage authorityManage) : base(authorityManage)
        {
        }
        public async Task<ResultModel<List<EnvironmentListDTO>>> GetEnvironmentListAsync()
        {
            var resultModel = await SendGetAsync<ResultModel<List<EnvironmentListDTO>>>($"{_controllerUrl}GetEnvironmentList");
            return resultModel;
        }

        public async Task<ResultModel> SyncAsync(SyncRequestModel requestModel)
        {
            var resultModel = await SendPutAsync<ResultModel>($"{_controllerUrl}Sync", requestModel);
            return resultModel;
        }
    }
}
