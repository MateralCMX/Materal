using ConfigCenter.DataTransmitModel.Project;
using ConfigCenter.HttpManage;
using ConfigCenter.PresentationModel.Project;
using Materal.APP.HttpClient;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPP.HttpClientImpl.ConfigCenter
{
    public class ProjectHttpClientImpl : ConfigCenterHttpClient, IProjectManage
    {
        private const string _controllerUrl = "/api/Project/";
        public ProjectHttpClientImpl(IAuthorityManage authorityManage) : base(authorityManage)
        {
        }

        public async Task<ResultModel> AddProjectAsync(AddProjectRequestModel requestModel)
        {
            var resultModel = await SendPutAsync<ResultModel>($"{_controllerUrl}AddProject", requestModel);
            return resultModel;
        }

        public async Task<ResultModel> EditProjectAsync(EditProjectRequestModel requestModel)
        {
            var resultModel = await SendPutAsync<ResultModel>($"{_controllerUrl}EditProject", requestModel);
            return resultModel;
        }

        public async Task<ResultModel> DeleteProjectAsync(Guid id)
        {
            var resultModel = await SendDeleteAsync<ResultModel>($"{_controllerUrl}DeleteProject", null, new Dictionary<string, string>
            {
                ["id"] = id.ToString()
            });
            return resultModel;
        }

        public async Task<ResultModel<ProjectDTO>> GetProjectInfoAsync(Guid id)
        {
            var resultModel = await SendGetAsync<ResultModel<ProjectDTO>>($"{_controllerUrl}GetProjectInfo", new Dictionary<string, string>
            {
                ["id"] = id.ToString()
            });
            return resultModel;
        }

        public async Task<ResultModel<List<ProjectListDTO>>> GetProjectListAsync(QueryProjectFilterRequestModel requestModel)
        {
            var resultModel = await SendPostAsync<ResultModel<List<ProjectListDTO>>>($"{_controllerUrl}GetProjectList", requestModel);
            return resultModel;
        }
    }
}
