//using Materal.Logger.LoggerHandlers.Models;
//using Materal.Logger.Models;
//using Microsoft.Extensions.Logging;

//namespace Materal.Logger.LoggerHandlers
//{
//    /// <summary>
//    /// 日志处理器
//    /// </summary>
//    public abstract class LoggerHandler : ILoggerHandler
//    {
//        /// <summary>
//        /// 处理
//        /// </summary>
//        /// <param name="model"></param>
//        /// <param name="rules"></param>
//        /// <param name="targets"></param>
//        /// <param name="logLevelConfig"></param>
//        public abstract void Handler(LoggerHandlerModel model, List<LoggerRuleConfigModel> rules, List<LoggerTargetConfigModel> targets, Dictionary<string, LogLevel> logLevelConfig);
//        /// <summary>
//        /// 关闭
//        /// </summary>
//        /// <returns></returns>
//        public virtual Task ShutdownAsync() => Task.CompletedTask;
//        /// <summary>
//        /// 是否运行
//        /// </summary>
//        /// <param name="rule"></param>
//        /// <param name="logLevel"></param>
//        /// <param name="categoryName"></param>
//        /// <param name="defaultLogLevels"></param>
//        /// <returns></returns>
//        protected virtual bool CanRun(LoggerRuleConfigModel rule, LogLevel logLevel, string? categoryName, Dictionary<string, LogLevel> defaultLogLevels)
//        {
//            if (logLevel == LogLevel.None) return false;
//            if (rule.MinLevel > logLevel || rule.MaxLevel < logLevel) return false;
//            LogLevel? configLogLevel = null;
//            Dictionary<string, LogLevel> logLevels = rule.LogLevels ?? defaultLogLevels;
//            if (categoryName is not null && !string.IsNullOrWhiteSpace(categoryName))
//            {
//                int index = 0;
//                foreach (string key in logLevels.Keys)
//                {
//                    if (categoryName == key)
//                    {
//                        configLogLevel = logLevels[key];
//                        break;
//                    }
//                    if (!categoryName.StartsWith(key)) continue;
//                    int nowIndex = key.Split('.').Length;
//                    if (index > nowIndex) continue;
//                    index = nowIndex;
//                    configLogLevel = logLevels[key];
//                }
//            }
//            if (configLogLevel is null && logLevels.ContainsKey("Default"))
//            {
//                configLogLevel = logLevels["Default"];
//            }
//            if (configLogLevel is null) return true;
//            if (configLogLevel == LogLevel.None) return false;
//            bool result = configLogLevel <= logLevel;
//            return result;
//        }
//    }
//    /// <summary>
//    /// 日志处理器
//    /// </summary>
//    public abstract class LoggerHandler<T> : LoggerHandler, ILoggerHandler, ILoggerHandler<T>
//        where T : LoggerTargetConfigModel
//    {
//        /// <summary>
//        /// 处理
//        /// </summary>
//        /// <param name="model"></param>
//        /// <param name="rules"></param>
//        /// <param name="targets"></param>
//        /// <param name="defaultLogLevels"></param>
//        /// <returns></returns>
//        public override void Handler(LoggerHandlerModel model, List<LoggerRuleConfigModel> rules, List<LoggerTargetConfigModel> targets, Dictionary<string, LogLevel> defaultLogLevels)
//        {
//            List<T> trueTargets = new();
//            foreach (LoggerTargetConfigModel targe in targets)
//            {
//                if (targe is not T temp) continue;
//                trueTargets.Add(temp);
//            }
//            Handler(model, rules, trueTargets, defaultLogLevels);
//        }
//        /// <summary>
//        /// 处理
//        /// </summary>
//        /// <param name="model"></param>
//        /// <param name="rules"></param>
//        /// <param name="targets"></param>
//        /// <param name="defaultLogLevels"></param>
//        /// <returns></returns>
//        public void Handler(LoggerHandlerModel model, List<LoggerRuleConfigModel> rules, List<T> targets, Dictionary<string, LogLevel> defaultLogLevels)
//        {
//            foreach (LoggerRuleConfigModel rule in rules)
//            {
//                if (!rule.Enable || !CanRun(rule, model.LogLevel, model.CategoryName, defaultLogLevels)) continue;
//                foreach (string targetName in rule.Targets)
//                {
//                    T? target = targets.FirstOrDefault(m => m.Name == targetName && m.Enable);
//                    if (target is null) continue;
//                    try
//                    {
//                        Handler(rule, target, model);
//                    }
//                    catch (Exception ex)
//                    {
//                        LoggerLog.LogError("日志记录出错", ex);
//                    }
//                }
//            }
//        }
//        /// <summary>
//        /// 处理
//        /// </summary>
//        /// <param name="rule"></param>
//        /// <param name="target"></param>
//        /// <param name="model"></param>
//        protected abstract void Handler(LoggerRuleConfigModel rule, T target, LoggerHandlerModel model);
//    }
//}
