#nullable enable
using RC.Core.HttpClient;
using RC.ServerCenter.DataTransmitModel.Namespace;
using RC.ServerCenter.PresentationModel.Namespace;
using System.ComponentModel.DataAnnotations;
using Materal.BaseCore.Common.Utils.TreeHelper;
using Materal.BaseCore.Common.Utils.IndexHelper;
using Materal.BaseCore.PresentationModel;
using Materal.Model;

namespace RC.ServerCenter.HttpClient
{
    public partial class NamespaceHttpClient : HttpClientBase<AddNamespaceRequestModel, EditNamespaceRequestModel, QueryNamespaceRequestModel, NamespaceDTO, NamespaceListDTO>
    {
        public NamespaceHttpClient() : base("RC.ServerCenter") { }
    }
}
