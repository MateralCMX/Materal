using Domain;
using Materal.TTA.Common;
using System;
using System.Collections.Generic;
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
    }
}
