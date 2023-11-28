using Materal.Gateway.Common;
using Materal.Gateway.Common.ConfigModel;
using Materal.Gateway.WebAPI.ConfigModel;

namespace Materal.Gateway.WebAPI
{
    /// <summary>
    /// WebAPIConfig
    /// </summary>
    public static class WebAPIConfig
    {
        /// <summary>
        /// 授权配置
        /// </summary>
        public static List<UserConfigModel> Users => ApplicationConfig.GetValueObject<List<UserConfigModel>>("Users");
        /// <summary>
        /// 授权配置
        /// </summary>
        public static JWTConfigModel JWTConfig => ApplicationConfig.GetValueObject<JWTConfigModel>("JWT");
    }
}
