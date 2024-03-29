using Materal.BaseCore.PresentationModel;
using Materal.BaseCore.Services.Models;
using Materal.BaseCore.WebAPI.Controllers;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;
using RC.Authority.DataTransmitModel.User;
using RC.Authority.PresentationModel.User;
using RC.Authority.Services;
using RC.Authority.Services.Models.User;

namespace RC.Authority.WebAPI.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    public partial class UserController : MateralCoreWebAPIServiceControllerBase<AddUserRequestModel, EditUserRequestModel, QueryUserRequestModel, AddUserModel, EditUserModel, QueryUserModel, UserDTO, UserListDTO, IUserService>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        public UserController(IServiceProvider serviceProvider) : base(serviceProvider) { }
    }
}
