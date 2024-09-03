namespace Materal.MergeBlock.Domain.Abstractions
{
    /// <summary>
    /// 位序领域模型
    /// </summary>
    public interface IIndexDomain : IDomain
    {
        /// <summary>
        /// 位序
        /// </summary>
        public int Index { get; set; }
    }
}
