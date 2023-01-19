using System;
using System.ComponentModel.DataAnnotations;
namespace Authority.PresentationModel.APIAuthority.Request
{
    /// <summary>
    /// API权限修改请求模型
    /// </summary>
    public class EditAPIAuthorityRequestModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识不可以为空")]
        public Guid ID { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        [Required(ErrorMessage = "代码不可以为空"), StringLength(100, ErrorMessage = "代码长度不能超过100")]
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称不可以为空"), StringLength(100, ErrorMessage = "名称长度不能超过100")]
        public string Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
