using Materal.BaseCore.Services;
using Materal.BaseCore.Services.Models;
using Materal.Utils.Model;
using MBC.Demo.DataTransmitModel.User;
using MBC.Demo.Services.Models.User;

namespace MBC.Demo.Services
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public partial interface IUserService : IBaseService<AddUserModel, EditUserModel, QueryUserModel, UserDTO, UserListDTO>
    {
    }
}
