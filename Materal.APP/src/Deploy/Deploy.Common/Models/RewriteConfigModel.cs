using Materal.APP.Core;
using System;

namespace Deploy.Common.Models
{
    /// <summary>
    /// 重写配置模型
    /// </summary>
    public class RewriteConfigModel
    {
        /// <summary>
        /// 配置键
        /// </summary>
        private const string ConfigKey = "Rewrite";
        /// <summary>
        /// 启用标识
        /// </summary>
        public bool Enable => Convert.ToBoolean(GetConfigValue(nameof(Enable)));
        /// <summary>
        /// 地址
        /// </summary>
        public string Address => GetConfigValue(nameof(Address));
        /// <summary>
        /// 获得配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetConfigValue(string key)
        {
            return ApplicationConfig.Config.GetSection($"{ConfigKey}:{key}").Value;
        }
    }
}
