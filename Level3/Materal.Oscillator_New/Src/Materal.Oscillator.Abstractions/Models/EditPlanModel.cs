using System.ComponentModel.DataAnnotations;

namespace Materal.Oscillator.Abstractions.Models
{
    /// <summary>
    /// 修改计划模型
    /// </summary>
    public class EditPlanModel : AddPlanModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识为空")]
        public Guid ID { get; set; }
    }
}
