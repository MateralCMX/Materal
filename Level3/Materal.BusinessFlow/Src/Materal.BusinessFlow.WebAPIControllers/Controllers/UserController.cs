using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Abstractions.Services.Models.User;

namespace Materal.BusinessFlow.WebAPIControllers.Controllers
{
    public class UserController : BusinessFlowServiceControllerBase<User, User, IUserService, AddUserModel, EditUserModel, QueryUserModel>
    {
        public UserController(IServiceProvider service) : base(service)
        {
        }
    }
}