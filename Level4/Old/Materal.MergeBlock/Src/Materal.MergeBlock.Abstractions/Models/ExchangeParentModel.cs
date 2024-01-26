namespace Materal.MergeBlock.Abstractions.Models
{
    /// <summary>
    /// 更改父级模型
    /// </summary>
    public class ExchangeParentModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识为空")]
        public Guid SourceID { get; set; }
        /// <summary>
        /// 目标唯一标识
        /// </summary>
        public Guid? TargetID { get; set; }
    }
}