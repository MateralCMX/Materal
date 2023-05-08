using Materal.TTA.Common;

namespace Materal.BusinessFlow.Abstractions.Domain
{
    public interface IDomain : IEntity<Guid>
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime { get; set; }
    }
}
