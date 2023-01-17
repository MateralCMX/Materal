using Materal.BaseCore.WebAPI.Controllers;
using RC.ServerCenter.DataTransmitModel.Namespace;
using RC.ServerCenter.PresentationModel.Namespace;
using RC.ServerCenter.Services;
using RC.ServerCenter.Services.Models.Namespace;

namespace RC.ServerCenter.WebAPI.Controllers
{
    /// <summary>
    /// 命名空间控制器
    /// </summary>
    public partial class NamespaceController : MateralCoreWebAPIServiceControllerBase<AddNamespaceRequestModel, EditNamespaceRequestModel, QueryNamespaceRequestModel, AddNamespaceModel, EditNamespaceModel, QueryNamespaceModel, NamespaceDTO, NamespaceListDTO, INamespaceService>
    {
    }
}
