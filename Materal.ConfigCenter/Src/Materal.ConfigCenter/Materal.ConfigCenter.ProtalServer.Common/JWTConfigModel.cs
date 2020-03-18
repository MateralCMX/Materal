using System;
using System.Collections.Generic;
using System.Text;

namespace Materal.ConfigCenter.ProtalServer.Common
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
    }
}
