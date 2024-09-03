namespace Materal.MergeBlock.Abstractions.Models
{
    /// <summary>
    /// 交换索引请求模型
    /// </summary>
    public class ExchangeIndexRequestModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识为空")]
        public Guid SourceID { get; set; }
        /// <summary>
        /// 目标唯一标识
        /// </summary>
        [Required(ErrorMessage = "目标唯一标识为空")]
        public Guid TargetID { get; set; }
        /// <summary>
        /// 是否在目标之前
        /// </summary>
        public bool Before { get; set; } = false;
    }
}