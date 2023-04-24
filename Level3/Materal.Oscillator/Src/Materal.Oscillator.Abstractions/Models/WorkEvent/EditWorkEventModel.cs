using System.ComponentModel.DataAnnotations;

namespace Materal.Oscillator.Abstractions.Models.WorkEvent
{
    public class EditWorkEventModel : AddWorkEventModel, IEditModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识不能为空")]
        public Guid ID { get; set; }
    }
}
