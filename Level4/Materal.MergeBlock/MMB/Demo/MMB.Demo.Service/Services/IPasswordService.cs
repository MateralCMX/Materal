namespace MMB.Demo.Service.Services
{
    /// <summary>
    /// 密码服务
    /// </summary>
    public interface IPasswordService
    {
        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        string EncodePassword(string password);
    }
}
