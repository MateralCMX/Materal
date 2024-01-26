namespace Materal.MergeBlock.Abstractions.Models
{
    /// <summary>
    /// 时间段请求模型
    /// </summary>
    public class TimeQuantumAndOperationUserIDModel : TimeQuantumModel
    {
        /// <summary>
        /// 操作用户唯一标识
        /// </summary>
        [Required(ErrorMessage = "操作用户唯一标识为空")]
        public Guid OperationUserID { get; set; }
    }
}
