using System.ComponentModel.DataAnnotations;

namespace Materal.BusinessFlow.Abstractions.Services.Models
{
    public interface IEditModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required]
        Guid ID { get; set; }
    }
}
