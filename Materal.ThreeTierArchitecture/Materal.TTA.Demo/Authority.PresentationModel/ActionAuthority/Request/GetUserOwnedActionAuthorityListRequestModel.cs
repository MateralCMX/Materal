using System.ComponentModel.DataAnnotations;

namespace Authority.PresentationModel.ActionAuthority.Request
{
    /// <summary>
    /// 获取用户拥有的功能组列表请求模型
    /// </summary>
    public class GetUserOwnedActionAuthorityListRequestModel
    {
        /// <summary>
        /// 功能组唯一标识
        /// </summary>
        [Required(ErrorMessage = "功能组唯一标识不可以为空")]
        public string ActionGroupCode { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        [Required(ErrorMessage = "Token不可以为空")]
        public string Token { get; set; }
    }
}
