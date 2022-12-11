using System.ComponentModel.DataAnnotations;

namespace Materal.Oscillator.Abstractions.Models.Answer
{
    public class AddAnswerModel : AnswerModel
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
        /// 启用标识
        /// </summary>
        [Required(ErrorMessage = "启用标识为空")]
        public bool Enable { get; set; } = true;
    }
}
