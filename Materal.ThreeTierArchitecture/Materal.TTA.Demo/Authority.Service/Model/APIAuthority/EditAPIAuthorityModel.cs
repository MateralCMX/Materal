using System;
namespace Authority.Service.Model.APIAuthority
{
    /// <summary>
    /// API权限修改模型
    /// </summary>
    public class EditAPIAuthorityModel : AddAPIAuthorityModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
    }
}
