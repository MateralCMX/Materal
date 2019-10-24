namespace Materal.Services
{
    /// <summary>
    /// 权限服务
    /// </summary>
    public interface IAuthorityService
    {
        /// <summary>
        /// 密码
        /// </summary>
        /// <param name="password"></param>
        string Login(string password);
        /// <summary>
        /// 已登录
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        bool IsLogin(string token);
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <param name="token"></param>
        void Logout(string token);
    }
}
