using Materal.Logger.Models;

namespace Materal.Logger.LoggerHandlers
{
    /// <summary>
    /// 日志处理器工厂
    /// </summary>
    public static class LoggerHandlerFactroy
    {
        /// <summary>
        /// 获得日志处理器
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        public static List<LoggerHandler> GetLoggerHandlers(LoggerRuleConfigModel rule)
        {
            List<LoggerHandler> result = new();
            foreach (string ruleTarget in rule.Targets)
            {
                LoggerTargetConfigModel? target = LoggerConfig.TargetsConfig.FirstOrDefault(m => m.Name == ruleTarget);
                if (target == null || !target.Enable) continue;
                LoggerHandler? loggerHandler = $"{target.Type}LoggerHandler".GetObjectByTypeName<LoggerHandler>(rule, target);
                if (loggerHandler != null)
                {
                    result.Add(loggerHandler);
                }
            }
            return result;
        }
    }
}
