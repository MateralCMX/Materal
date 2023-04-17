namespace Materal.BusinessFlow.Abstractions.Domain
{
    public interface IBaseDomain
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        Guid ID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime { get; set; }
    }
}
