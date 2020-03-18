namespace Materal.ConfigCenter.ProtalServer.Domain
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User : BaseDomain
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
