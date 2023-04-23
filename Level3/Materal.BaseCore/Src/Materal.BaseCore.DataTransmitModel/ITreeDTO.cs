namespace Materal.BaseCore.DataTransmitModel
{
    /// <summary>
    /// 树数据传输模型
    /// </summary>
    public interface ITreeDTO<T>
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 父级
        /// </summary>
        public Guid? ParentID { get; set; }
        /// <summary>
        /// 子级
        /// </summary>
        public List<T> Children { get; set; }
    }
}
