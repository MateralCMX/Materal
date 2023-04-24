using Materal.Oscillator.Abstractions.Enums;
using System.ComponentModel.DataAnnotations;

namespace Materal.Oscillator.Abstractions.Models.ScheduleWork
{
    public class AddScheduleWorkModel : ScheduleWorkModel
    {
        /// <summary>
        /// 调度器唯一标识
        /// </summary>
        [Required(ErrorMessage = "调度器唯一标识为空")]
        public Guid ScheduleID { get; set; }
        /// <summary>
        /// 位序
        /// </summary>
        [Required(ErrorMessage = "位序为空")]
        public int OrderIndex { get; set; }
        /// <summary>
        /// 成功事件
        /// </summary>
        [Required(ErrorMessage = "成功事件为空"), StringLength(100, ErrorMessage = "成功事件长度大于100")]
        public string SuccessEvent { get; set; } = DefaultWorkEventEnum.Next.ToString();
        /// <summary>
        /// 失败事件
        /// </summary>
        [Required(ErrorMessage = "失败事件为空"), StringLength(100, ErrorMessage = "失败事件长度大于100")]
        public string FailEvent { get; set; } = DefaultWorkEventEnum.Fail.ToString();
    }
}
