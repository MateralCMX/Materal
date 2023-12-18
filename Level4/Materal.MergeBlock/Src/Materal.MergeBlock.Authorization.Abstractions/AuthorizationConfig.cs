using System.Text;

namespace Materal.MergeBlock.Authorization.Abstractions
{
    /// <summary>
    /// 鉴权配置模型
    /// </summary>
    public class AuthorizationConfig
    {
        /// <summary>
        /// 配置键
        /// </summary>
        public static string ConfigKey { get; } = "Authorization";
        /// <summary>
        /// 密钥
        /// </summary>
        public string Key { get; set; } = "CMXMateral";
        /// <summary>
        /// 有效期
        /// </summary>
        public uint ExpiredTime { get; set; } = 7200;
        /// <summary>
        /// 发布者
        /// </summary>
        public string Issuer { get; set; } = "MateralCore.Server";
        /// <summary>
        /// 接收者
        /// </summary>
        public string Audience { get; set; } = "MateralCore.WebAPI";
        /// <summary>
        /// 二进制密钥
        /// </summary>
        public byte[] KeyBytes => Encoding.UTF8.GetBytes(Key.ToMd5_32Encode(true));
    }
}
