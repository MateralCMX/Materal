#nullable enable
using RC.Core.HttpClient;
using Materal.Utils.Model;
using Materal.BaseCore.PresentationModel;
using RC.ServerCenter.DataTransmitModel.Namespace;
using RC.ServerCenter.PresentationModel.Namespace;

namespace RC.ServerCenter.HttpClient
{
    public partial class NamespaceHttpClient : HttpClientBase<AddNamespaceRequestModel, EditNamespaceRequestModel, QueryNamespaceRequestModel, NamespaceDTO, NamespaceListDTO>
    {
        public NamespaceHttpClient(IServiceProvider serviceProvider) : base("RC.ServerCenter", serviceProvider) { }
    }
}
