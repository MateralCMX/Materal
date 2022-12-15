using Materal.ConvertHelper;
using Materal.Logger.Models;

namespace Materal.Logger.LoggerHandlers
{
    public static class LoggerHandlerFactroy
    {
        public static List<LoggerHandler> GetLoggerHandlers(MateralLoggerRuleConfigModel rule)
        {
            List<LoggerHandler> result = new();
            foreach (string ruleTarget in rule.Targets)
            {
                MateralLoggerTargetConfigModel? target = MateralLoggerConfig.TargetsConfig.FirstOrDefault(m => m.Name == ruleTarget);
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
