using System.Collections.Generic;

namespace Authority.DataTransmitModel.User
{
    /// <summary>
    /// 用户数据传输模型
    /// </summary>
    public class UserDTO : UserListDTO
    {
        /// <summary>
        /// 用户子系统角色对象
        /// </summary>
        public ICollection<UserRoleTreeDTO> UserRoleTreeList { get; set; }
    }
}
