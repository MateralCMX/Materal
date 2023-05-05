using System.ComponentModel.DataAnnotations;

namespace Materal.Oscillator.Abstractions.Models
{
    /// <summary>
    /// 修改任务模型
    /// </summary>
    public class EditWorkModel : AddWorkModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识为空")]
        public Guid ID { get; set; }
    }
}
