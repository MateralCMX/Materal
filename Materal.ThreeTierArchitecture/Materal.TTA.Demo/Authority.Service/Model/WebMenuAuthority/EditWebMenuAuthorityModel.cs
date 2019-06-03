using System;
namespace Authority.Service.Model.WebMenuAuthority
{
    /// <summary>
    /// 网页菜单权限修改模型
    /// </summary>
    public class EditWebMenuAuthorityModel : AddWebMenuAuthorityModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
    }
}
