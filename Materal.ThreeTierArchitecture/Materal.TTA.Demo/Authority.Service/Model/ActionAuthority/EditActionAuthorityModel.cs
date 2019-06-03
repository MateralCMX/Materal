using System;
namespace Authority.Service.Model.ActionAuthority
{
    /// <summary>
    /// 功能权限修改模型
    /// </summary>
    public class EditActionAuthorityModel : AddActionAuthorityModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
    }
}
