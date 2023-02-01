using Materal.ConvertHelper;

namespace Materal.Gateway.Common.ConfigModels
{
    public class AuthorizationConfig
    {
        /// <summary>
        /// 配置键
        /// </summary>
        private const string ConfigKey = "Authorization";
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName => GetNLogConfigValue(nameof(UserName));
        /// <summary>
        /// 密码
        /// </summary>
        public string Password => GetNLogConfigValue(nameof(Password));
        /// <summary>
        /// 加密盐
        /// </summary>
        public string Salt => GetNLogConfigValue(nameof(Salt));
        /// <summary>
        /// 获得授权Token
        /// </summary>
        /// <returns></returns>
        public string GetAuthorizationToken()
        {
            return GetAuthorizationToken(UserName, Password);
        }
        /// <summary>
        /// 获得授权Token
        /// </summary>
        /// <returns></returns>
        public string GetAuthorizationToken(string userName, string password)
        {
            return $"{Salt}Materal{userName}{Salt}{password}Materal{Salt}".ToMd5_32Encode();
        }
        /// <summary>
        /// 获得NLog配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetNLogConfigValue(string key)
        {
            return ApplicationConfig.Config.GetSection($"{ConfigKey}:{key}").Value;
        }
    }
}
