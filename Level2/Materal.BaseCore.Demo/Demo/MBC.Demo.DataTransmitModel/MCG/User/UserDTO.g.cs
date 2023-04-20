#nullable enable
using System.ComponentModel.DataAnnotations;
using Materal.BaseCore.DataTransmitModel;
using MBC.Demo.Enums;

namespace MBC.Demo.DataTransmitModel.User
{
    /// <summary>
    /// 用户数据传输模型
    /// </summary>
    public partial class UserDTO : UserListDTO, IDTO
    {
    }
}
