using Materal.BaseCore.ServiceImpl;
using Materal.BaseCore.Services.Models;
using Materal.TTA.EFRepository;
using RC.Demo.DataTransmitModel.User;
using RC.Demo.Domain;
using RC.Demo.Domain.Repositories;
using RC.Demo.Services;
using RC.Demo.Services.Models.User;

namespace RC.Demo.ServiceImpl
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
