using System.ComponentModel.DataAnnotations;

namespace Materal.Oscillator.Abstractions.Domain
{
    /// <summary>
    /// 响应
    /// </summary>
    [Serializable]
    public class Answer : BaseDomain, IDomain
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称为空"), StringLength(100, ErrorMessage = "名称长度大于100")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 调度器唯一标识
        /// </summary>
        [Required(ErrorMessage = "调度器唯一标识为空")]
        public Guid ScheduleID { get; set; }
        /// <summary>
        /// 任务事件
        /// </summary>
        [Required(ErrorMessage = "任务事件为空"), StringLength(40, ErrorMessage = "任务事件长度大于40")]
        public string WorkEvent { get; set; } = string.Empty;
        /// <summary>
        /// 启用标识
        /// </summary>
        [Required(ErrorMessage = "启用标识为空")]
        public bool Enable { get; set; } = true;
        /// <summary>
        /// 位序
        /// </summary>
        [Required(ErrorMessage = "位序为空")]
        public int Index { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(400, ErrorMessage = "描述长度大于400")]
        public string? Description { get; set; }
        /// <summary>
        /// 响应数据类型
        /// </summary>
        [Required(ErrorMessage = "响应数据类型为空"), StringLength(100, ErrorMessage = "响应数据类型长度大于100")]
        public string AnswerType { get; set; } = string.Empty;
        /// <summary>
        /// 响应数据
        /// </summary>
        [Required(ErrorMessage = "响应数据为空"), StringLength(4000, ErrorMessage = "响应数据长度大于4000")]
        public string AnswerData { get; set; } = string.Empty;
    }
}
