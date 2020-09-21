using ConfigCenter.DataTransmitModel.Namespace;
using ConfigCenter.HttpManage;
using ConfigCenter.PresentationModel.Namespace;
using Materal.APP.HttpClient;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPP.HttpClientImpl.ConfigCenter
{
    public class NamespaceHttpClientImpl : ConfigCenterHttpClient, INamespaceManage
    {
        private const string _controllerUrl = "/api/Namespace/";
        public NamespaceHttpClientImpl(IAuthorityManage authorityManage) : base(authorityManage)
        {
        }

        public async Task<ResultModel> AddNamespaceAsync(AddNamespaceRequestModel requestModel)
        {
            var resultModel = await SendPutAsync<ResultModel>($"{_controllerUrl}AddNamespace", requestModel);
            return resultModel;
        }

        public async Task<ResultModel> EditNamespaceAsync(EditNamespaceRequestModel requestModel)
        {
            var resultModel = await SendPutAsync<ResultModel>($"{_controllerUrl}EditNamespace", requestModel);
            return resultModel;
        }

        public async Task<ResultModel> DeleteNamespaceAsync(Guid id)
        {
            var resultModel = await SendDeleteAsync<ResultModel>($"{_controllerUrl}DeleteNamespace", null, new Dictionary<string, string>
            {
                ["id"] = id.ToString()
            });
            return resultModel;
        }

        public async Task<ResultModel<NamespaceDTO>> GetNamespaceInfoAsync(Guid id)
        {
            var resultModel = await SendGetAsync<ResultModel<NamespaceDTO>>($"{_controllerUrl}GetNamespaceInfo", new Dictionary<string, string>
            {
                ["id"] = id.ToString()
            });
            return resultModel;
        }

        public async Task<ResultModel<List<NamespaceListDTO>>> GetNamespaceListAsync(QueryNamespaceFilterRequestModel requestModel)
        {
            var resultModel = await SendPostAsync<ResultModel<List<NamespaceListDTO>>>($"{_controllerUrl}GetNamespaceList", requestModel);
            return resultModel;
        }
    }
}
