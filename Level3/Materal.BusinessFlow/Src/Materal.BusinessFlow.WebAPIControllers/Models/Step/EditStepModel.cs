using System.ComponentModel.DataAnnotations;

namespace Materal.BusinessFlow.WebAPIControllers.Models.Step
{
    public class EditStepModel : AddStepModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required]
        public Guid ID { get; set; }
    }
}
