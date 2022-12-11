using System.ComponentModel.DataAnnotations;

namespace Materal.Oscillator.Abstractions.Models.Answer
{
    public class EditAnswerModel : AddAnswerModel, IEditModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识不能为空")]
        public Guid ID { get; set; } = Guid.Empty;
    }
}
