using Common;
using Materal.TTA.Common;
using System;
using Common.Tree;

namespace Authority.Domain.Views
{
    /// <summary>
    /// 用户拥有的网页菜单权限
    /// </summary>
    [ViewEntity]
    public sealed class UserOwnedWebMenuAuthority : IEntity<Guid>, ITreeDomain<Guid>
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
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 样式
        /// </summary>
        public string Style { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 位序
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 父级唯一标识
        /// </summary>
        public Guid? ParentID { get; set; }
    }
}
