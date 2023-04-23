using System;
namespace Authority.Service.Model.ActionAuthority
{
    /// <summary>
    /// 功能权限添加模型
    /// </summary>
    public class AddActionAuthorityModel
    {
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
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
