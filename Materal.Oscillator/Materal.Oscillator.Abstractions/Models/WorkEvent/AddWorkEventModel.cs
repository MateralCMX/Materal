using System.ComponentModel.DataAnnotations;

namespace Materal.Oscillator.Abstractions.Models.WorkEvent
{
    public class AddWorkEventModel : WorkEventModel
    {
        /// <summary>
        /// 调度唯一标识
        /// </summary>
        [Required(ErrorMessage = "调度器唯一标识不能为空")]
        public Guid ScheduleID { get; set; }
    }
}
