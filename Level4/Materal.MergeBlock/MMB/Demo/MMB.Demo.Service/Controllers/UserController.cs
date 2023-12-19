using Materal.MergeBlock.Authorization.Abstractions;
using Materal.TFMS.EventBus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MMB.Demo.Abstractions.Controllers;
using MMB.Demo.Abstractions.Events;
using MMB.Demo.Abstractions.Services;

namespace MMB.Demo.WebAPI.Controllers
{
    /// <summary>
    /// ²âÊÔ¿ØÖÆÆ÷
    /// </summary>
    [ApiController, Route("/api/[controller]/[action]")]
    public class UserController(IUserService userService) : MateralCoreWebAPIServiceControllerBase<AddUserRequestModel, EditUserRequestModel, QueryUserRequestModel, AddUserModel, EditUserModel, QueryUserModel, UserDTO, UserListDTO, IUserService>, IUserController
    {
    }
}
