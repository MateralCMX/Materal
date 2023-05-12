using System.ComponentModel.DataAnnotations;

namespace Materal.BusinessFlow.Abstractions.Services.Models.DataModel
{
    public class EditDataModelModel : AddDataModelModel, IEditModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required]
        public Guid ID { get; set; }
    }
}
