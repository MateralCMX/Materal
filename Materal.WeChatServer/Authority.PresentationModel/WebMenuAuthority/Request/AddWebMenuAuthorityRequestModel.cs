using System;
using System.ComponentModel.DataAnnotations;
namespace Authority.PresentationModel.WebMenuAuthority.Request
{
    /// <summary>
    /// 网页菜单权限添加请求模型
    /// </summary>
    public class AddWebMenuAuthorityRequestModel
    {
        /// <summary>
        /// 代码
        /// </summary>
        [MaxLength(100, ErrorMessage = "代码长度不能超过100")]
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称不可以为空"), MaxLength(100, ErrorMessage = "名称长度不能超过100")]
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
