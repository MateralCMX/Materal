using System.ComponentModel.DataAnnotations;

namespace Materal.Oscillator.Abstractions.Domain
{
    /// <summary>
    /// 调度器
    /// </summary>
    [Serializable]
    public class ScheduleWorkView : BaseDomain, IDomain, ISchedultView
    {
        /// <summary>
        /// 调度器唯一标识
        /// </summary>
        [Required(ErrorMessage = "调度器唯一标识为空")]
        public Guid ScheduleID { get; set; }
        /// <summary>
        /// 任务唯一标识
        /// </summary>
        [Required(ErrorMessage = "任务唯一标识为空")]
        public Guid WorkID { get; set; }
        /// <summary>
        /// 位序
        /// </summary>
        [Required(ErrorMessage = "位序为空")]
        public int OrderIndex { get; set; }
        /// <summary>
        /// 成功事件
        /// </summary>
        [Required(ErrorMessage = "成功事件为空"), StringLength(100, ErrorMessage = "成功事件长度大于100")]
        public string SuccessEvent { get; set; } = string.Empty;
        /// <summary>
        /// 失败事件
        /// </summary>
        [Required(ErrorMessage = "失败事件为空"), StringLength(100, ErrorMessage = "失败事件长度大于100")]
        public string FailEvent { get; set; } = string.Empty;
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称为空"), StringLength(100, ErrorMessage = "名称长度大于100")]
        public string WorkName { get; set; } = string.Empty;
        /// <summary>
        /// 任务类型
        /// </summary>
        [Required(ErrorMessage = "任务类型为空"), StringLength(100, ErrorMessage = "任务类型长度大于100")]
        public string WorkType { get; set; } = string.Empty;
        /// <summary>
        /// 任务数据
        /// </summary>
        [Required(ErrorMessage = "任务数据为空"), StringLength(4000, ErrorMessage = "任务数据长度大于4000")]
        public string WorkData { get; set; } = string.Empty;
        /// <summary>
        /// 调度器名称
        /// </summary>
        [Required(ErrorMessage = "名称为空"), StringLength(100, ErrorMessage = "名称长度大于100")]
        public string ScheduleName { get; set; } = string.Empty;
        /// <summary>
        /// 业务领域
        /// </summary>
        [Required(ErrorMessage = "业务领域为空"), StringLength(100, ErrorMessage = "业务领域长度大于100")]
        public string Territory { get; set; } = string.Empty;
    }
}
