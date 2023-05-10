using System.ComponentModel.DataAnnotations;

namespace Materal.BusinessFlow.WebAPIControllers.Models.User
{
    public class EditUserModel : AddUserModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required]
        public Guid ID { get; set; }
    }
}
