using System.ComponentModel.DataAnnotations;

namespace Materal.ConDep.Center.PresentationModel.User
{
    public class ChangePasswordRequestModel
    {
        /// <summary>
        /// 旧密码
        /// </summary>
        [Required(ErrorMessage = "旧密码不能为空")]
        public string OldPassword { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        [Required(ErrorMessage = "新密码不能为空")]
        public string NewPassword { get; set; }
    }
}
