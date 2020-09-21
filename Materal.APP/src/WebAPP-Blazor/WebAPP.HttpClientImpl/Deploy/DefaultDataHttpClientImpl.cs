using Deploy.DataTransmitModel.DefaultData;
using Deploy.HttpManage;
using Deploy.PresentationModel.DefaultData;
using Materal.APP.HttpClient;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPP.HttpClientImpl.Deploy
{
    public class DefaultDataHttpClientImpl : DeployHttpClient, IDefaultDataManage
    {
        private const string _controllerUrl = "/api/DefaultData/";
        public DefaultDataHttpClientImpl(IAuthorityManage authorityManage) : base(authorityManage)
        {
        }

        public async Task<ResultModel> AddAsync(AddDefaultDataRequestModel requestModel)
        {
            var resultModel = await SendPutAsync<ResultModel>($"{_controllerUrl}Add", requestModel);
            return resultModel;
        }

        public async Task<ResultModel> EditAsync(EditDefaultDataRequestModel requestModel)
        {
            var resultModel = await SendPutAsync<ResultModel>($"{_controllerUrl}Edit", requestModel);
            return resultModel;
        }

        public async Task<ResultModel> DeleteAsync(Guid id)
        {
            var resultModel = await SendDeleteAsync<ResultModel>($"{_controllerUrl}Delete", null, new Dictionary<string, string>
            {
                ["id"] = id.ToString()
            });
            return resultModel;
        }

        public async Task<ResultModel<DefaultDataDTO>> GetInfoAsync(Guid id)
        {
            var resultModel = await SendGetAsync<ResultModel<DefaultDataDTO>>($"{_controllerUrl}GetInfo", new Dictionary<string, string>
            {
                ["id"] = id.ToString()
            });
            return resultModel;
        }

        public async Task<PageResultModel<DefaultDataListDTO>> GetListAsync(QueryDefaultDataFilterRequestModel requestModel)
        {
            var resultModel = await SendPostAsync<PageResultModel<DefaultDataListDTO>>($"{_controllerUrl}GetList", requestModel);
            return resultModel;
        }
    }
}
