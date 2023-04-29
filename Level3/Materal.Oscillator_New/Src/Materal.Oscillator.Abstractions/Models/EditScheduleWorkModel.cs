using System.ComponentModel.DataAnnotations;

namespace Materal.Oscillator.Abstractions.Models
{
    /// <summary>
    /// 添加调度器任务
    /// </summary>
    public class EditScheduleWorkModel : AddScheduleModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识为空")]
        public Guid ID { get; set; }
    }
}
