using Materal.ConvertHelper;

namespace Materal.ApplicationUpdate.Common
{
    /// <summary>
    /// 加密管理器
    /// </summary>
    public class EncryptionManager
    {
        /// <summary>
        /// 获得MD5加密32位
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static string GetMd5_32(string inputStr)
        {
            return (inputStr + ApplicationConfig.MD5Salt).ToMd5_32Encode();
        }
    }
}
