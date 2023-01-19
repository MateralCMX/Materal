using Materal.TTA.Common;
using System;
namespace Authority.Domain.Views
{
    /// <summary>
    /// 用户拥有的功能权限
    /// </summary>
    [ViewEntity]
    public sealed class UserOwnedActionAuthority : IEntity<Guid>
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public Guid UserID { get; set; }
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
