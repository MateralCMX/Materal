using System.ComponentModel.DataAnnotations;

namespace Materal.BusinessFlow.Abstractions.Services.Models.Step
{
    public class EditStepModel : IEditModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required]
        public Guid ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required, StringLength(40)]
        public string Name { get; set; } = string.Empty;
    }
}
