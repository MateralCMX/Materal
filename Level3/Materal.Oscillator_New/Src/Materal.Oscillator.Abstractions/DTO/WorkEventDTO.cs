using Materal.Oscillator.Abstractions.Domain;
using System.ComponentModel.DataAnnotations;

namespace Materal.Oscillator.Abstractions.DTO
{
    /// <summary>
    /// 任务事件数据传输模型
    /// </summary>
    public class WorkEventDTO : BaseDTO
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称为空"), StringLength(100, ErrorMessage = "名称长度大于100")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 值
        /// </summary>
        [Required(ErrorMessage = "值为空"), StringLength(100, ErrorMessage = "值长度大于100")]
        public string Value { get; set; } = string.Empty;
        /// <summary>
        /// 调度唯一标识
        /// </summary>
        [Required(ErrorMessage = "调度器唯一标识为空")]
        public Guid ScheduleID { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(400, ErrorMessage = "描述长度大于400")]
        public string? Description { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public WorkEventDTO() : base() { }
        /// <summary>
        /// 构造方法
        /// </summary>
        public WorkEventDTO(WorkEvent domain) : base(domain)
        {
            domain.CopyProperties(this);
        }
    }
}
