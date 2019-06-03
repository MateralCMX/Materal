using System;
namespace Authority.Service.Model.Role
{
    /// <summary>
    /// 角色修改模型
    /// </summary>
    public class EditRoleModel : AddRoleModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
    }
}
