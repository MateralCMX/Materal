namespace MMB.Demo.Service.Services
{
    /// <summary>
    /// 密码服务
    /// </summary>
    public class PasswordServiceImpl : IPasswordService
    {
        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string EncodePassword(string password) => $"Materal{password}Materal".ToMd5_32Encode();
    }
}
