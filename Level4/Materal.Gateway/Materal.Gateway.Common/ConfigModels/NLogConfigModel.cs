using Materal.EnumHelper;
using Microsoft.Extensions.Logging;

namespace Materal.Gateway.Common.ConfigModels
{
    public class NLogConfigModel
    {
        /// <summary>
        /// 配置键
        /// </summary>
        private const string ConfigKey = "NLog";
        /// <summary>
        /// 最大文件保存天数
        /// </summary>
        public string MaxLogFileSaveDays => GetNLogConfigValue(nameof(MaxLogFileSaveDays));
        /// <summary>
        /// 最小日志等级
        /// </summary>
        public LogLevel MinLogLevel
        {
            get
            {
                string value = GetNLogConfigValue(nameof(MinLogLevel));
                LogLevel result = EnumManager.Parse<LogLevel>(value);
                return result;
            }
        }
        /// <summary>
        /// 获得NLog配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetNLogConfigValue(string key)
        {
            return ApplicationConfig.Config.GetSection($"{ConfigKey}:{key}").Value;
        }
    }
}
