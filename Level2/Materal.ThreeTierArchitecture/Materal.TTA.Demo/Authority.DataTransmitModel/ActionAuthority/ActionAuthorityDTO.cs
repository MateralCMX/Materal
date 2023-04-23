using System;
namespace Authority.DataTransmitModel.ActionAuthority
{
    /// <summary>
    /// 功能权限数据传输模型
    /// </summary>
    public class ActionAuthorityDTO : ActionAuthorityListDTO
    {
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
