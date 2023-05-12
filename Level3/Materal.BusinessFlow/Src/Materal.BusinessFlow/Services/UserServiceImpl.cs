using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Abstractions.Services.Models.User;

namespace Materal.BusinessFlow.Services
{
    public class UserServiceImpl : BaseServiceImpl<User, User, IUserRepository, AddUserModel, EditUserModel, QueryUserModel>, IUserService
    {
        public UserServiceImpl(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
