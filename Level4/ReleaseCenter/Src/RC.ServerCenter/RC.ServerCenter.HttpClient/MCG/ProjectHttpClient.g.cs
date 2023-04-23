#nullable enable
using RC.Core.HttpClient;
using Materal.BaseCore.PresentationModel;
using Materal.Utils.Model;
using RC.ServerCenter.DataTransmitModel.Project;
using RC.ServerCenter.PresentationModel.Project;

namespace RC.ServerCenter.HttpClient
{
    public partial class ProjectHttpClient : HttpClientBase<AddProjectRequestModel, EditProjectRequestModel, QueryProjectRequestModel, ProjectDTO, ProjectListDTO>
    {
        public ProjectHttpClient() : base("RC.ServerCenter") { }
    }
}
