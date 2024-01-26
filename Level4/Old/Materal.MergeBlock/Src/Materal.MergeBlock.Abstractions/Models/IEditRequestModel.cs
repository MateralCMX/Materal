namespace Materal.MergeBlock.Abstractions.Models
{
    /// <summary>
    /// 编辑请求模型
    /// </summary>
    public interface IEditRequestModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
    }
}