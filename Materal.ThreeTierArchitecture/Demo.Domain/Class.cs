using System;
using TTA.Core.Domain;

namespace Demo.Domain
{
    /// <inheritdoc />
    /// <summary>
    /// 班级
    /// </summary>
    public class Class : BaseEntity<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
