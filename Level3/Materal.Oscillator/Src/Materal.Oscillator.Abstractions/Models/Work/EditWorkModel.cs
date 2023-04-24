using System.ComponentModel.DataAnnotations;

namespace Materal.Oscillator.Abstractions.Models.Work
{
    public class EditWorkModel : AddWorkModel, IEditModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识不能为空")]
        public Guid ID { get; set; } = Guid.NewGuid();
    }
}
