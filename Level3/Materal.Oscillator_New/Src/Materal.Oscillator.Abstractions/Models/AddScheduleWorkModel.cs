using Materal.Oscillator.Abstractions.Enums;
using System.ComponentModel.DataAnnotations;

namespace Materal.Oscillator.Abstractions.Models
{
    /// <summary>
    /// 添加调度器任务
    /// </summary>
    public class AddScheduleWorkModel
    {
        /// <summary>
        /// 任务唯一标识
        /// </summary>
        [Required(ErrorMessage = "任务唯一标识为空")]
        public Guid WorkID { get; set; }
        /// <summary>
        /// 成功事件
        /// </summary>
        [Required(ErrorMessage = "成功事件为空"), StringLength(100, ErrorMessage = "成功事件长度大于100")]
        public string SuccessEvent { get; set; } = $"{DefaultWorkEventEnum.Next}";
        /// <summary>
        /// 失败事件
        /// </summary>
        [Required(ErrorMessage = "失败事件为空"), StringLength(100, ErrorMessage = "失败事件长度大于100")]
        public string FailEvent { get; set; } = $"{DefaultWorkEventEnum.Fail}";
    }
}
