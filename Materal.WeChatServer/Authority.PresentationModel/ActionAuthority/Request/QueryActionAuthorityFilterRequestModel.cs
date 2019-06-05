using Materal.Common;
using System.ComponentModel.DataAnnotations;
namespace Authority.PresentationModel.ActionAuthority.Request
{
    /// <summary>
    /// 功能权限查询请求模型
    /// </summary>
    public class QueryActionAuthorityFilterRequestModel : PageRequestModel
    {
        /// <summary>
        /// 代码
        /// </summary>
        [StringLength(100, ErrorMessage = "代码长度不能超过100")]
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(100, ErrorMessage = "名称长度不能超过100")]
        public string Name { get; set; }
        /// <summary>
        /// 功能组标识
        /// </summary>
        [StringLength(100, ErrorMessage = "功能组标识长度不能超过100")]
        public string ActionGroupCode { get; set; }
    }
}
