#nullable enable
using RC.Core.HttpClient;
using Materal.BaseCore.PresentationModel;
using Materal.Utils.Model;
using RC.ServerCenter.DataTransmitModel.Namespace;
using RC.ServerCenter.PresentationModel.Namespace;

namespace RC.ServerCenter.HttpClient
{
    public partial class NamespaceHttpClient : HttpClientBase<AddNamespaceRequestModel, EditNamespaceRequestModel, QueryNamespaceRequestModel, NamespaceDTO, NamespaceListDTO>
    {
        public NamespaceHttpClient() : base("RC.ServerCenter") { }
    }
}
