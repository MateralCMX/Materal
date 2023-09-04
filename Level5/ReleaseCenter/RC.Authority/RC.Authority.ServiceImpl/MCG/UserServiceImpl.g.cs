using Materal.BaseCore.ServiceImpl;
using Materal.BaseCore.Services.Models;
using Materal.TTA.EFRepository;
using RC.Authority.DataTransmitModel.User;
using RC.Authority.Domain;
using RC.Authority.Domain.Repositories;
using RC.Authority.Services;
using RC.Authority.Services.Models.User;

namespace RC.Authority.ServiceImpl
{
    /// <summary>
    /// 用户服务实现
    /// </summary>
    public partial class UserServiceImpl : BaseServiceImpl<AddUserModel, EditUserModel, QueryUserModel, UserDTO, UserListDTO, IUserRepository, User>, IUserService
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        public UserServiceImpl(IServiceProvider serviceProvider) : base(serviceProvider) { }
    }
}
