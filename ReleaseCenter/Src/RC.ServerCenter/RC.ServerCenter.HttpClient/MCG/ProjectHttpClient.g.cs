#nullable enable
using RC.Core.HttpClient;
using RC.ServerCenter.DataTransmitModel.Project;
using RC.ServerCenter.PresentationModel.Project;
using System.ComponentModel.DataAnnotations;
using Materal.BaseCore.Common.Utils.TreeHelper;
using Materal.BaseCore.Common.Utils.IndexHelper;
using Materal.BaseCore.PresentationModel;
using Materal.Model;

namespace RC.ServerCenter.HttpClient
{
    public partial class ProjectHttpClient : HttpClientBase<AddProjectRequestModel, EditProjectRequestModel, QueryProjectRequestModel, ProjectDTO, ProjectListDTO>
    {
        public ProjectHttpClient() : base("RC.ServerCenter") { }
    }
}
