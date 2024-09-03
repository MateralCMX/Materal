namespace Materal.MergeBlock.Domain.Abstractions
{
    /// <summary>
    /// 基础Domain
    /// </summary>
    public abstract class BaseDomain : IDomain, IEntity<Guid>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        protected BaseDomain()
        {
            ID = Guid.NewGuid();
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.MinValue;
        }
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
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