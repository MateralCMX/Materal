using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Abstractions.Services.Models;
using Materal.BusinessFlow.WebAPIControllers.Models.User;

namespace Materal.BusinessFlow.WebAPIControllers.Controllers
{
    public class UserController : BusinessFlowServiceControllerBase<User, User, IUserService, QueryUserModel, AddUserModel, EditUserModel>
    {
        public UserController(IServiceProvider service) : base(service)
        {
        }
    }
}