using System.ComponentModel.DataAnnotations;

namespace Materal.BusinessFlow.Abstractions.Services.Models.User
{
    public class AddUserModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required, StringLength(40)]
        public string Name { get; set; } = string.Empty;
    }
}
