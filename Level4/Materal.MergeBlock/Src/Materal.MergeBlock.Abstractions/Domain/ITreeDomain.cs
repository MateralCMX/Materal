namespace Materal.MergeBlock.Abstractions.Domain
{
    /// <summary>
    /// 树领域
    /// </summary>
    public interface ITreeDomain : IDomain
    {
        /// <summary>
        /// 父级
        /// </summary>
        public Guid? ParentID { get; set; }
    }
}
