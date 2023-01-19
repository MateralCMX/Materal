using System.ComponentModel.DataAnnotations;

namespace Authority.PresentationModel.WebMenuAuthority.Request
{
    /// <summary>
    /// 获取用户拥有的网页菜单权限树
    /// </summary>
    public class GetUserHasWebMenuAuthorityTreeRequestModel
    {
        /// <summary>
        /// Token
        /// </summary>
        [Required(ErrorMessage = "Token不可以为空")]
        public string Token { get; set; }
    }
}
