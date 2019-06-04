using System;
namespace Authority.Service.Model.Role
{
    /// <summary>
    /// 角色修改模型
    /// </summary>
    public class EditRoleModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 功能权限唯一标识组
        /// </summary>
        public Guid[] ActionAuthorityIDs { get; set; }
        /// <summary>
        /// API权限唯一标识组
        /// </summary>
        public Guid[] APIAuthorityIDs { get; set; }
        /// <summary>
        /// 网页菜单权限唯一标识组
        /// </summary>
        public Guid[] WebMenuAuthorityIDs { get; set; }
    }
}
