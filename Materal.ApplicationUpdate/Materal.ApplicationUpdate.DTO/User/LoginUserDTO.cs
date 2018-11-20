namespace Materal.ApplicationUpdate.DTO.User
{
    /// <inheritdoc />
    /// <summary>
    /// 登录用户数据传输模型
    /// </summary>
    public class LoginUserDTO : UserDTO
    {
        /// <summary>
        /// Token值
        /// </summary>
        public string Token { get; set; }
    }
}
