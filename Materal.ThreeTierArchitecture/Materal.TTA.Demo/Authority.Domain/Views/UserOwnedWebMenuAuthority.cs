using Domain;
using Materal.TTA.Common;
using System;
using System.Collections.Generic;
namespace Authority.Domain.Views
{
    /// <summary>
    /// 用户拥有的网页菜单权限
    /// </summary>
    [ViewEntity]
    public sealed class UserOwnedWebMenuAuthority : IEntity<Guid>
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
    }
}
