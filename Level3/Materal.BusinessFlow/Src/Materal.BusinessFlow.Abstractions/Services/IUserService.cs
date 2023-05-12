using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.Abstractions.Services.Models.User;

namespace Materal.BusinessFlow.Abstractions.Services
{
    public interface IUserService : IBaseService<User, User, IUserRepository, AddUserModel, EditUserModel, QueryUserModel>
    {

    }
}
