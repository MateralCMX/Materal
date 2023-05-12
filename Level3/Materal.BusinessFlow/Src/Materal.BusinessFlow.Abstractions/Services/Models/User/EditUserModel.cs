using System.ComponentModel.DataAnnotations;

namespace Materal.BusinessFlow.Abstractions.Services.Models.User
{
    public class EditUserModel : AddUserModel, IEditModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required]
        public Guid ID { get; set; }
    }
}
