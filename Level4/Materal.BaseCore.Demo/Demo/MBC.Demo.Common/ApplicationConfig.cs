using Materal.BaseCore.Common;
using Materal.Extensions;

namespace MBC.Demo.Common
{
    /// <summary>
    /// 应用程序配置
    /// </summary>
    public partial class ApplicationConfig
    {
        /// <summary>
        /// 默认密码
        /// </summary>
        public static string DefaultPassword => MateralCoreConfig.GetConfigItemToString("DefaultPassword");
        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string EncodePassword(string inputString)
        {
            return $"Materal{inputString}Materal".ToMd5_32Encode();
        }
    }
}
