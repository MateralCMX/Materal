using System.ComponentModel.DataAnnotations;

namespace Materal.WebAPITest.Models
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名为空")]
        public string Name { get; set; } = string.Empty;
    }
}
