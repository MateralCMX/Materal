﻿using System.ComponentModel.DataAnnotations;

namespace Materal.Gateway.OcelotExtension.ConfigModel
{
    /// <summary>
    /// 身份认证配置模型
    /// </summary>
    public class AuthenticationOptionsModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; } = Guid.NewGuid();
        /// <summary>
        /// 验证键
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public string AuthenticationProviderKey { get; set; } = "Bearer";
        /// <summary>
        /// 允许访问的域
        /// </summary>
        public List<string> AllowedScopes { get; set; } = new();
    }
}
