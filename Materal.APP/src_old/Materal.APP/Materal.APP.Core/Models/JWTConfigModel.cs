using System.Text;

namespace Materal.APP.Core.Models
{
    /// <summary>
    /// JWT配置模型
    /// </summary>
    public class JWTConfigModel
    {
        /// <summary>
        /// 密钥
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public uint ExpiredTime { get; set; }
        /// <summary>
        /// 发布者
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 接收者
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 二进制密钥
        /// </summary>
        public byte[] KeyBytes => Encoding.UTF8.GetBytes(Key);
    }
}