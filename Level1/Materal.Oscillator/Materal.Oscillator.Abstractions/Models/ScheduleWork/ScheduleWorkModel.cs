using System.ComponentModel.DataAnnotations;

namespace Materal.Oscillator.Abstractions.Models
{
    public class ScheduleWorkModel
    {
        /// <summary>
        /// 任务唯一标识
        /// </summary>
        [Required(ErrorMessage = "任务唯一标识为空")]
        public Guid WorkID { get; set; }
    }
}
