using System.ComponentModel.DataAnnotations;

namespace Materal.Oscillator.Abstractions.Models
{
    /// <summary>
    /// 添加调度器模型
    /// </summary>
    public class AddScheduleModel
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
        public string Territory { get; set; } = "Public";
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
        /// 计划组
        /// </summary>
        [Required(ErrorMessage = "计划组为空"), MinLength(1, ErrorMessage = "至少需要一个计划")]
        public List<AddPlanModel> Plans { get; set; } = new();
        /// <summary>
        /// 任务组
        /// </summary>
        [Required(ErrorMessage = "任务组为空"), MinLength(1, ErrorMessage = "至少需要一个任务")]
        public List<AddScheduleWorkModel> Wokrs { get; set; } = new();
        /// <summary>
        /// 响应组
        /// </summary>
        public List<AddAnswerModel> Answers { get; set; } = new();
        /// <summary>
        /// 任务事件组
        /// </summary>
        public List<AddWorkEventModel> WorkEvents { get; set; } = new();
    }
}
