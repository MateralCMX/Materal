using Deploy.DataTransmitModel.ApplicationInfo;
using Deploy.HttpManage;
using Deploy.PresentationModel.ApplicationInfo;
using Materal.APP.HttpClient;
using Materal.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPP.HttpClientImpl.Deploy
{
    public class ApplicationInfoHttpClientImpl : DeployHttpClient, IApplicationInfoManage
    {
        private const string _controllerUrl = "/api/DefaultData/";
        public ApplicationInfoHttpClientImpl(IAuthorityManage authorityManage) : base(authorityManage)
        {
        }

        public async Task<ResultModel> AddAsync(AddApplicationInfoRequestModel requestModel)
        {
            var resultModel = await SendPostAsync<ResultModel>($"{_controllerUrl}Add", requestModel);
            return resultModel;
        }

        public async Task<ResultModel> EditAsync(EditApplicationInfoRequestModel requestModel)
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

        public async Task<ResultModel<ApplicationInfoDTO>> GetInfoAsync(Guid id)
        {
            var resultModel = await SendGetAsync<ResultModel<ApplicationInfoDTO>>($"{_controllerUrl}GetInfo", new Dictionary<string, string>
            {
                ["id"] = id.ToString()
            });
            return resultModel;
        }

        public async Task<ResultModel<List<ApplicationInfoListDTO>>> GetListAsync(QueryApplicationInfoFilterRequestModel requestModel)
        {
            var resultModel = await SendPatchAsync<ResultModel<List<ApplicationInfoListDTO>>>($"{_controllerUrl}GetList", requestModel);
            return resultModel;
        }

        public async Task<ResultModel> StartAsync(Guid id)
        {
            var resultModel = await SendPatchAsync<ResultModel>($"{_controllerUrl}Start", null, new Dictionary<string, string>
            {
                ["id"] = id.ToString()
            });
            return resultModel;
        }

        public async Task<ResultModel> StopAsync(Guid id)
        {
            var resultModel = await SendPatchAsync<ResultModel>($"{_controllerUrl}Stop", null, new Dictionary<string, string>
            {
                ["id"] = id.ToString()
            });
            return resultModel;
        }

        public async Task<ResultModel<ICollection<string>>> GetConsoleMessageAsync(Guid id)
        {
            var resultModel = await SendGetAsync<ResultModel<ICollection<string>>>($"{_controllerUrl}GetConsoleMessage", new Dictionary<string, string>
            {
                ["id"] = id.ToString()
            });
            return resultModel;
        }

        public async Task<ResultModel> StartAllAsync()
        {
            var resultModel = await SendPatchAsync<ResultModel>($"{_controllerUrl}StartAll");
            return resultModel;
        }

        public async Task<ResultModel> StopAllAsync()
        {
            var resultModel = await SendPatchAsync<ResultModel>($"{_controllerUrl}StopAll");
            return resultModel;
        }

        public async Task<ResultModel> UploadNewFileAsync(IFormFile file)
        {
            var resultModel = await SendPostAsync<ResultModel>($"{_controllerUrl}UploadNewFile", file);
            return resultModel;
        }

        public async Task<ResultModel> ClearUpdateFilesAsync()
        {
            var resultModel = await SendPatchAsync<ResultModel>($"{_controllerUrl}ClearUpdateFiles");
            return resultModel;
        }

        public async Task<ResultModel> ClearInactiveApplicationAsync()
        {
            var resultModel = await SendPatchAsync<ResultModel>($"{_controllerUrl}ClearInactiveApplication");
            return resultModel;
        }
    }
}
