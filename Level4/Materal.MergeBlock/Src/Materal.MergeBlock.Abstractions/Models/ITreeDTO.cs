namespace Materal.MergeBlock.Abstractions.Models
{
    /// <summary>
    /// 树数据传输模型
    /// </summary>
    public interface ITreeDTO
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
    /// <summary>
    /// 树数据传输模型
    /// </summary>
    public interface ITreeDTO<T> : ITreeDTO
    {
        /// <summary>
        /// 子级
        /// </summary>
        public List<T> Children { get; set; }
    }
}
