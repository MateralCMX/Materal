using System;

namespace Authority.DataTransmitModel.Role
{
    /// <summary>
    /// 角色功能权限列表数据传输模型
    /// </summary>
    public class RoleActionAuthorityListDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 功能组标识
        /// </summary>
        public string ActionGroupCode { get; set; }
        /// <summary>
        /// 拥有标识
        /// </summary>
        public bool Owned { get; set; }
    }
}
