using Materal.BaseCore.ServiceImpl;
using Materal.BaseCore.Services.Models;
using MBC.Demo.DataTransmitModel.User;
using MBC.Demo.Domain;
using MBC.Demo.Domain.Repositories;
using MBC.Demo.Services;
using MBC.Demo.Services.Models.User;

namespace MBC.Demo.ServiceImpl
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
