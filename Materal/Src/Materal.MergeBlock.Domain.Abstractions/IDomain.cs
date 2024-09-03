namespace Materal.MergeBlock.Domain.Abstractions
{
    /// <summary>
    /// Domain
    /// </summary>
    public interface IDomain : IEntity<Guid>
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
