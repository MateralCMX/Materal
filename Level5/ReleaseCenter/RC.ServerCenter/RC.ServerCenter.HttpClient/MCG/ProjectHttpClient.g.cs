#nullable enable
using RC.Core.HttpClient;
using Materal.Utils.Model;
using Materal.BaseCore.PresentationModel;
using RC.ServerCenter.DataTransmitModel.Project;
using RC.ServerCenter.PresentationModel.Project;

namespace RC.ServerCenter.HttpClient
{
    public partial class ProjectHttpClient : HttpClientBase<AddProjectRequestModel, EditProjectRequestModel, QueryProjectRequestModel, ProjectDTO, ProjectListDTO>
    {
        public ProjectHttpClient(IServiceProvider serviceProvider) : base("RC.ServerCenter", serviceProvider) { }
    }
}
