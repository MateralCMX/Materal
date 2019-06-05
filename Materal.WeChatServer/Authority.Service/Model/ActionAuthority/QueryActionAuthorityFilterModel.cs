using Materal.Common;
namespace Authority.Service.Model.ActionAuthority
{
    /// <summary>
    /// 功能权限查询模型
    /// </summary>
    public class QueryActionAuthorityFilterModel : PageRequestModel
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
    }
}
