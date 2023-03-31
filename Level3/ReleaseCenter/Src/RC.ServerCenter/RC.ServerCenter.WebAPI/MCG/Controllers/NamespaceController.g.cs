using Materal.BaseCore.PresentationModel;
using Materal.BaseCore.Services.Models;
using Materal.BaseCore.WebAPI.Controllers;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;
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
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        public NamespaceController(IServiceProvider serviceProvider) : base(serviceProvider) { }
    }
}
