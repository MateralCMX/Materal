using System;

namespace Materal.APP.Core.ConfigModels
{
    public class TFMSConfigModel
    {
        /// <summary>
        /// 配置键
        /// </summary>
        private const string ConfigKey = "TFMS";
        /// <summary>
        /// 主机
        /// </summary>
        public string Host => GetConfigValue(nameof(Host));
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port => Convert.ToInt32(GetConfigValue(nameof(Port)));
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName => GetConfigValue(nameof(UserName));
        /// <summary>
        /// 密码
        /// </summary>
        public string Password => GetConfigValue(nameof(Password));
        /// <summary>
        /// 队列名称
        /// </summary>
        public string QueueName => GetConfigValue(nameof(QueueName));
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
