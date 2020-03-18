using Materal.ConvertHelper;

namespace Materal.ConfigCenter
{
    public static class StringExtension
    {
        private const string salt = "MateralCMX";
        public static string ToMd5(this string inputString)
        {
            return (salt + inputString + salt).ToMd5_32Encode();
        }
    }
}
