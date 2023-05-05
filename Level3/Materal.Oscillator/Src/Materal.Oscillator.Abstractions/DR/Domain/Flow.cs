using Materal.Oscillator.Abstractions.Domain;
using System.ComponentModel.DataAnnotations;

namespace Materal.Oscillator.Abstractions.DR.Domain
{
    /// <summary>
    /// 流程
    /// </summary>
    [Serializable]
    public class Flow : BaseDomain, IDomain
    {
        /// <summary>
        /// 作业Key
        /// </summary>
        [Required(ErrorMessage = "作业Key为空")]
        public string JobKey { get; set; } = string.Empty;
        /// <summary>
        /// 调度器数据
        /// </summary>
        [Required(ErrorMessage = "调度器数据为空")]
        public string ScheduleData { get; set; } = string.Empty;
        /// <summary>
        /// 调度器唯一标识
        /// </summary>
        [Required(ErrorMessage = "调度器唯一标识为空")]
        public Guid ScheduleID { get; set; }
        /// <summary>
        /// 任务唯一标识
        /// </summary>
        public Guid? WorkID { get; set; }
        /// <summary>
        /// 任务返回值
        /// </summary>
        public string? WorkResults { get; set; }
        /// <summary>
        /// 认证码
        /// </summary>
        [Required(ErrorMessage = "认证码为空")]
        public Guid AuthenticationCode { get; set; }
    }
}
