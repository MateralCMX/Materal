﻿using Materal.Utils.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Materal.Logger.LoggerHandlers
{
    /// <summary>
    /// 日志处理器帮助类
    /// </summary>
    public static class LoggerHandlerHelper
    {
        /// <summary>
        /// 获得错误消息
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static string GetErrorMessage(Exception exception)
        {
            if (exception is MateralHttpException httpException)
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
        public static string GetProgressID()
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
        /// <param name="loggerConfig"></param>
        /// <param name="writeMessage"></param>
        /// <param name="logLevel"></param>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="scope"></param>
        /// <param name="dateTime"></param>
        /// <param name="exception"></param>
        /// <param name="threadID"></param>
        /// <param name="logID"></param>
        /// <returns></returns>
        public static string FormatMessage(LoggerConfig loggerConfig, string writeMessage, LogLevel logLevel, string message, string? categoryName, LoggerScope? scope, DateTime dateTime, Exception? exception, string threadID, Guid logID)
        {
            string result = writeMessage;
            result = Regex.Replace(result, @"\$\{Message\}", message);
            result = FormatText(loggerConfig, result, logLevel, categoryName, scope, dateTime, threadID);
            string errorMesage = exception == null ? string.Empty : GetErrorMessage(exception);
            result = Regex.Replace(result, @"\$\{LogID\}", logID.ToString());
            result = Regex.Replace(result, @"\$\{Exception\}", errorMesage);
            result = result.Trim();
            return result;
        }
        /// <summary>
        /// 格式化文本
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="text"></param>
        /// <param name="logLevel"></param>
        /// <param name="categoryName"></param>
        /// <param name="scope"></param>
        /// <param name="dateTime"></param>
        /// <param name="threadID"></param>
        /// <returns></returns>
        public static string FormatText(LoggerConfig loggerConfig, string text, LogLevel logLevel, string? categoryName, LoggerScope? scope, DateTime dateTime, string threadID)
        {
            string result = text;
            result = FormatPath(loggerConfig, result, logLevel, categoryName, scope, dateTime, threadID);
            result = Regex.Replace(result, @"\$\{Time\}", dateTime.ToString("HH:mm:ss"));
            result = Regex.Replace(result, @"\$\{DateTime\}", dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            result = result.Trim();
            return result;
        }
        /// <summary>
        /// 格式化路径
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="path"></param>
        /// <param name="logLevel"></param>
        /// <param name="categoryName"></param>
        /// <param name="scope"></param>
        /// <param name="dateTime"></param>
        /// <param name="threadID"></param>
        /// <returns></returns>
        public static string FormatPath(LoggerConfig loggerConfig, string path, LogLevel logLevel, string? categoryName, LoggerScope? scope, DateTime dateTime, string threadID)
        {
            string result = path;
            if (scope is not null)
            {
                result = scope.HandlerText(result);
            }
            foreach (KeyValuePair<string, string> item in loggerConfig.CustomConfig)
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
            result = Regex.Replace(result, @"\$\{Scope\}", scope == null ? "PublicScope" : scope.ScopeName);
            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                result = Regex.Replace(result, @"\$\{CategoryName\}", categoryName);
            }
            result = Regex.Replace(result, @"\$\{Application\}", loggerConfig.Application);
            string progressID = GetProgressID();
            result = Regex.Replace(result, @"\$\{ProgressID\}", progressID);
            result = Regex.Replace(result, @"\$\{ThreadID\}", threadID);
            result = Regex.Replace(result, @"\$\{MachineName\}", MachineName);
            result = result.Trim();
            return result;
        }
    }
}
