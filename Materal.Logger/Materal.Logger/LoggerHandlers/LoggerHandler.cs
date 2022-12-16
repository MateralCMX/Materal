using Materal.ConvertHelper;
using Materal.Logger.Models;
using Materal.StringHelper;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text;

namespace Materal.Logger.LoggerHandlers
{
    public abstract class LoggerHandler
    {
        /// <summary>
        /// 规则
        /// </summary>
        public MateralLoggerRuleConfigModel Rule { get; private set; }
        /// <summary>
        /// 目标
        /// </summary>
        public MateralLoggerTargetConfigModel Target { get; private set; }
        protected LoggerHandler(MateralLoggerRuleConfigModel rule, MateralLoggerTargetConfigModel target)
        {
            Rule = rule;
            Target = target;
        }
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="scope"></param>
        /// <param name="dateTime"></param>
        /// <param name="exception"></param>
        public abstract void Handler(LogLevel logLevel, string message, string? categoryName, MateralLoggerScope? scope, DateTime dateTime, Exception? exception, string threadID);
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logLevel"></param>
        public virtual void SendMessage(string message, LogLevel logLevel) => MateralLogger.LocalServer?.SendMessage(Target, message, logLevel);
        /// <summary>
        /// 关闭
        /// </summary>
        public virtual void Close() { }
        /// <summary>
        /// 能否运行
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public bool CanRun(LogLevel logLevel, string? categoryName)
        {
            if (!Target.Enable || !Rule.Enable || Rule.MinLevel > logLevel || Rule.MaxLevel < logLevel) return false;
            if (string.IsNullOrWhiteSpace(categoryName)) return true;
            foreach (var item in Rule.Ignores)
            {
                if (categoryName.VerifyRegex(item)) return false;
            }
            return true;
        }
        /// <summary>
        /// 获得错误消息
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        protected static string GetErrorMessage(Exception exception)
        {
            StringBuilder builder = new();
            Exception? tempException = exception;
            while (true)
            {
                if (string.IsNullOrWhiteSpace(tempException.StackTrace))
                {
                    builder.Append(tempException.Message);
                }
                else
                {
                    builder.AppendLine(tempException.Message);
                    builder.Append(tempException.StackTrace);
                }
                tempException = tempException.InnerException;
                if(tempException != null)
                {
                    builder.AppendLine("-->");
                }
                else
                {
                    break;
                }
            }
            return builder.ToString();
        }
        /// <summary>
        /// 获得进程ID
        /// </summary>
        /// <returns></returns>
        protected static string GetProgressID()
        {
            Process processes = Process.GetCurrentProcess();
            return processes.Id.ToString();
        }
        /// <summary>
        /// 计算机名称
        /// </summary>
        public static string MachineName => Environment.MachineName;
        /// <summary>
        /// 格式化消息
        /// </summary>
        /// <param name="writeMessage"></param>
        /// <param name="logLevel"></param>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="scope"></param>
        /// <param name="dateTime"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        protected static string FormatMessage(string writeMessage, LogLevel logLevel, string message, string? categoryName, MateralLoggerScope? scope, DateTime dateTime, Exception? exception, string threadID)
        {
            string result = writeMessage;
            foreach (KeyValuePair<string, string> item in MateralLoggerManager.CustomData)
            {
                result = result.Replace($"${{{item.Key}}}", item.Value);
            }
            result = result.Replace("${Date}", dateTime.ToString("yyyy-MM-dd"));
            result = result.Replace("${Time}", dateTime.ToString("HH:mm:ss"));
            result = result.Replace("${DateTime}", dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            result = result.Replace("${Year}", dateTime.Year.ToString());
            result = result.Replace("${Month}", dateTime.Month.ToString());
            result = result.Replace("${Day}", dateTime.Day.ToString());
            result = result.Replace("${Hour}", dateTime.Hour.ToString());
            result = result.Replace("${Minute}", dateTime.Minute.ToString());
            result = result.Replace("${Second}", dateTime.Second.ToString());
            result = result.Replace("${Level}", logLevel.ToString());
            result = result.Replace("${Scope}", scope == null ? "PublicScope" : scope.Scope);
            result = result.Replace("${Message}", message);
            result = result.Replace("${CategoryName}", categoryName);
            result = result.Replace("${Application}", MateralLoggerConfig.Application);
            string errorMesage = exception == null ? string.Empty : GetErrorMessage(exception);
            result = result.Replace("${Exception}", errorMesage);
            string progressID = GetProgressID();
            result = result.Replace("${ProgressID}", progressID);
            result = result.Replace("${ThreadID}", threadID);
            result = result.Replace("${MachineName}", MachineName);
            return result;
        }
        /// <summary>
        /// 格式化路径
        /// </summary>
        /// <param name="path"></param>
        /// <param name="logLevel"></param>
        /// <param name="categoryName"></param>
        /// <param name="scope"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string FormatPath(string path, LogLevel logLevel, string? categoryName, MateralLoggerScope? scope, DateTime dateTime, string threadID)
        {
            string result = path;
            foreach (KeyValuePair<string, string> item in MateralLoggerManager.CustomData)
            {
                result = result.Replace($"${{{item.Key}}}", item.Value);
            }
            result = result.Replace("${Date}", dateTime.ToString("yyyyMMdd"));
            result = result.Replace("${Year}", dateTime.Year.ToString());
            result = result.Replace("${Month}", dateTime.Month.ToString());
            result = result.Replace("${Day}", dateTime.Day.ToString());
            result = result.Replace("${Hour}", dateTime.Hour.ToString());
            result = result.Replace("${Minute}", dateTime.Minute.ToString());
            result = result.Replace("${Second}", dateTime.Second.ToString());
            result = result.Replace("${Level}", logLevel.ToString());
            result = result.Replace("${Scope}", scope == null ? "PublicScope" : scope.Scope);
            result = result.Replace("${CategoryName}", categoryName);
            result = result.Replace("${Application}", MateralLoggerConfig.Application);
            string progressID = GetProgressID();
            result = result.Replace("${ProgressID}", progressID);
            result = result.Replace("${ThreadID}", threadID);
            result = result.Replace("${MachineName}", MachineName);
            return result;
        }
        /// <summary>
        /// 获得Log模型
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="scope"></param>
        /// <param name="dateTime"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        protected static MateralLogModel GetMateralLogModel(LogLevel logLevel, string message, string? categoryName, MateralLoggerScope? scope, DateTime dateTime, Exception? exception, string threadID)
        {
            MateralLogModel result = new()
            {
                Application = MateralLoggerConfig.Application,
                Level = logLevel.ToString(),
                MachineName = MachineName,
                CreateTime = dateTime,
                Message = message,
                ProgressID = GetProgressID(),
                ThreadID = threadID,
                Scope = scope == null ? "PublicScope" : scope.Scope,
                CategoryName = categoryName,
                Error = exception == null ? null : GetErrorMessage(exception),
                CustomInfo = MateralLoggerManager.CustomData.ToJson()
            };
            return result;
        }
    }
}
