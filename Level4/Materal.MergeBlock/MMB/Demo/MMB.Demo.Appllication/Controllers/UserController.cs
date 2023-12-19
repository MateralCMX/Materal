using Materal.MergeBlock.Application.Controllers;
using Microsoft.AspNetCore.Mvc;
using MMB.Demo.Abstractions.DTO.User;
using MMB.Demo.Abstractions.Services.Models.User;

namespace MMB.Demo.Appllication.Controllers
{
    /// <summary>
    /// ²âÊÔ¿ØÖÆÆ÷
    /// </summary>
    [ApiController, Route("/api/[controller]/[action]")]
    public class UserController : MergeBlockControllerBase<AddUserRequestModel, EditUserRequestModel, QueryUserRequestModel, AddUserModel, EditUserModel, QueryUserModel, UserDTO, UserListDTO, IUserService>, IUserController
    {
    }
}
