using System.ComponentModel.DataAnnotations;

namespace Materal.Oscillator.Abstractions.Models.Schedule
{
    public class AddScheduleModel : ScheduleModel
    {
        /// <summary>
        /// 启用标识
        /// </summary>
        [Required(ErrorMessage = "启用标识为空")]
        public bool Enable { get; set; } = true;
    }
}
