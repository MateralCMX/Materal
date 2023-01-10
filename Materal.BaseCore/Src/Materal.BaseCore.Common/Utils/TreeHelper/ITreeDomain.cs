namespace Materal.BaseCore.Common.Utils.TreeHelper
{
    /// <summary>
    /// 树领域
    /// </summary>
    public interface ITreeDomain
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 父级
        /// </summary>
        public Guid? ParentID { get; set; }
    }
}
