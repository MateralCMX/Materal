using Materal.ConvertHelper;

namespace Materal.ApplicationUpdate.Common
{
    public class EncryptionManager
    {
        public static string GetMd5_32(string inputStr)
        {
            return (inputStr + ApplicationConfig.MD5Salt).ToMd5_32Encode();
        }
    }
}
