using Materal.Oscillator.Abstractions.Domain;

namespace Materal.Oscillator.Abstractions.DTO
{
    /// <summary>
    /// 调度器数据传输模型
    /// </summary>
    public class ScheduleDTO : BaseDTO
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称为空"), StringLength(100, ErrorMessage = "名称长度大于100")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 业务领域
        /// </summary>
        [Required(ErrorMessage = "业务领域为空"), StringLength(100, ErrorMessage = "业务领域长度大于100")]
        public string Territory { get; set; } = string.Empty;
        /// <summary>
        /// 启用标识
        /// </summary>
        [Required(ErrorMessage = "启用标识为空")]
        public bool Enable { get; set; } = true;
        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(400, ErrorMessage = "描述长度大于400")]
        public string? Description { get; set; }
        /// <summary>
        /// 响应组
        /// </summary>
        public List<AnswerDTO> Answers { get; set; } = new();
        /// <summary>
        /// 计划组
        /// </summary>
        public List<PlanDTO> Plans { get; set; } = new();
        /// <summary>
        /// 任务
        /// </summary>
        public List<WorkDTO> Works { get; set; } = new();
        /// <summary>
        /// 构造方法
        /// </summary>
        public ScheduleDTO() : base() { }
        /// <summary>
        /// 构造方法
        /// </summary>
        public ScheduleDTO(Schedule domain) : base(domain)
        {
            domain.CopyProperties(this);
        }
    }
}
