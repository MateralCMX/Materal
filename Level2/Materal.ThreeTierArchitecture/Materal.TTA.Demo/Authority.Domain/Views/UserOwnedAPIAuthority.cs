using Materal.TTA.Common;
using System;
namespace Authority.Domain.Views
{
    /// <summary>
    /// 用户拥有的API权限
    /// </summary>
    [ViewEntity]
    public sealed class UserOwnedAPIAuthority : IEntity<Guid>
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
        /// 父级唯一标识
        /// </summary>
        public Guid? ParentID { get; set; }
    }
}
