using System;
namespace Authority.Service.Model.WebMenuAuthority
{
    /// <summary>
    /// 网页菜单权限添加模型
    /// </summary>
    public class AddWebMenuAuthorityModel
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
        /// 样式
        /// </summary>
        public string Style { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public Guid? ParentID { get; set; }
    }
}
