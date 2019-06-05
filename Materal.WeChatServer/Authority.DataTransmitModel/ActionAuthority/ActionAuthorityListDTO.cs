using System;
namespace Authority.DataTransmitModel.ActionAuthority
{
    /// <summary>
    /// 功能权限列表数据传输模型
    /// </summary>
    public class ActionAuthorityListDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
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
