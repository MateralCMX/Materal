﻿using Materal.Logger.Models;
using Materal.Utils.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Materal.Logger.LoggerHandlers
{
    /// <summary>
    /// 日志处理器
    /// </summary>
    public abstract class LoggerHandler
    {
        /// <summary>
        /// 规则
        /// </summary>
        public LoggerRuleConfigModel Rule { get; private set; }
        /// <summary>
        /// 目标
        /// </summary>
        public LoggerTargetConfigModel Target { get; private set; }
        /// <summary>
        /// 日志处理器
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="target"></param>
        protected LoggerHandler(LoggerRuleConfigModel rule, LoggerTargetConfigModel target)
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
        /// <param name="threadID"></param>
        public abstract void Handler(LogLevel logLevel, string message, string? categoryName, LoggerScope? scope, DateTime dateTime, Exception? exception, string threadID);
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logLevel"></param>
        public virtual void SendMessage(string message, LogLevel logLevel) => Logger.LocalServer?.SendMessage(Target, message, logLevel);
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
            if (categoryName == null || string.IsNullOrWhiteSpace(categoryName)) return true;
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
            if(exception is MateralHttpException httpException)
            {
                return httpException.GetExceptionMessage();
            }
            else
            {
                return exception.GetErrorMessage();
            }
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
        /// 根路径
        /// </summary>
        public static string RootPath
        {
            get
            {
                string result = AppDomain.CurrentDomain.BaseDirectory;
                if (result.EndsWith("\\") || result.EndsWith("/"))
                {
                    result = result[0..^1];
                }
                return result;
            }
        }
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
        /// <param name="threadID"></param>
        /// <returns></returns>
        protected static string FormatMessage(string writeMessage, LogLevel logLevel, string message, string? categoryName, LoggerScope? scope, DateTime dateTime, Exception? exception, string threadID)
        {
            string result = writeMessage;
            foreach (KeyValuePair<string, string> item in LoggerManager.CustomData)
            {
                result = Regex.Replace(result, $@"\$\{{{item.Key}\}}", item.Value);
            }
            result = Regex.Replace(result, @"\$\{RootPath\}", RootPath);
            result = Regex.Replace(result, @"\$\{Date\}", dateTime.ToString("yyyy-MM-dd"));
            result = Regex.Replace(result, @"\$\{Time\}", dateTime.ToString("HH:mm:ss"));
            result = Regex.Replace(result, @"\$\{DateTime\}", dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            result = Regex.Replace(result, @"\$\{Year\}", dateTime.Year.ToString());
            result = Regex.Replace(result, @"\$\{Month\}", dateTime.Month.ToString());
            result = Regex.Replace(result, @"\$\{Day\}", dateTime.Day.ToString());
            result = Regex.Replace(result, @"\$\{Hour\}", dateTime.Hour.ToString());
            result = Regex.Replace(result, @"\$\{Minute\}", dateTime.Minute.ToString());
            result = Regex.Replace(result, @"\$\{Second\}", dateTime.Second.ToString());
            result = Regex.Replace(result, @"\$\{Level\}", logLevel.ToString());
            result = Regex.Replace(result, @"\$\{Scope\}", scope == null ? "PublicScope" : scope.Scope);
            result = Regex.Replace(result, @"\$\{Message\}", message);
            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                result = Regex.Replace(result, @"\$\{CategoryName\}", categoryName);
            }
            result = Regex.Replace(result, @"\$\{Application\}", LoggerConfig.Application);
            string errorMesage = exception == null ? string.Empty : GetErrorMessage(exception);
            result = Regex.Replace(result, @"\$\{Exception\}", errorMesage);
            string progressID = GetProgressID();
            result = Regex.Replace(result, @"\$\{ProgressID\}", progressID);
            result = Regex.Replace(result, @"\$\{ThreadID\}", threadID);
            result = Regex.Replace(result, @"\$\{MachineName\}", MachineName);
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
        /// <param name="threadID"></param>
        /// <returns></returns>
        public static string FormatPath(string path, LogLevel logLevel, string? categoryName, LoggerScope? scope, DateTime dateTime, string threadID)
        {
            string result = path;
            foreach (KeyValuePair<string, string> item in LoggerManager.CustomData)
            {
                result = Regex.Replace(result, $@"\$\{{{item.Key}\}}", item.Value);
            }
            result = Regex.Replace(result, @"\$\{RootPath\}", RootPath);
            result = Regex.Replace(result, @"\$\{Date\}", dateTime.ToString("yyyyMMdd"));
            result = Regex.Replace(result, @"\$\{Year\}", dateTime.Year.ToString());
            result = Regex.Replace(result, @"\$\{Month\}", dateTime.Month.ToString());
            result = Regex.Replace(result, @"\$\{Day\}", dateTime.Day.ToString());
            result = Regex.Replace(result, @"\$\{Hour\}", dateTime.Hour.ToString());
            result = Regex.Replace(result, @"\$\{Minute\}", dateTime.Minute.ToString());
            result = Regex.Replace(result, @"\$\{Second\}", dateTime.Second.ToString());
            result = Regex.Replace(result, @"\$\{Level\}", logLevel.ToString());
            result = Regex.Replace(result, @"\$\{Scope\}", scope == null ? "PublicScope" : scope.Scope);
            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                result = Regex.Replace(result, @"\$\{CategoryName\}", categoryName);
            }
            result = Regex.Replace(result, @"\$\{Application\}", LoggerConfig.Application);
            string progressID = GetProgressID();
            result = Regex.Replace(result, @"\$\{ProgressID\}", progressID);
            result = Regex.Replace(result, @"\$\{ThreadID\}", threadID);
            result = Regex.Replace(result, @"\$\{MachineName\}", MachineName);
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
        /// <param name="threadID"></param>
        /// <returns></returns>
        protected static LogModel GetMateralLogModel(LogLevel logLevel, string message, string? categoryName, LoggerScope? scope, DateTime dateTime, Exception? exception, string threadID)
        {
            LogModel result = new()
            {
                Application = LoggerConfig.Application,
                Level = logLevel.ToString(),
                MachineName = MachineName,
                CreateTime = dateTime,
                Message = message,
                ProgressID = GetProgressID(),
                ThreadID = threadID,
                Scope = scope == null ? "PublicScope" : scope.Scope,
                CategoryName = categoryName,
                Error = exception == null ? null : GetErrorMessage(exception),
                CustomInfo = LoggerManager.CustomData.ToJson()
            };
            return result;
        }
    }
}
