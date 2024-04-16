﻿using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Materal.Logger.LoggerWriter
{
    /// <summary>
    /// 日志写入器帮助类
    /// </summary>
    public static class LoggerWriterHelper
    {
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
        private static string? _rootPath;
        /// <summary>
        /// 根路径
        /// </summary>
        public static string RootPath
        {
            get
            {
                if (_rootPath is null || string.IsNullOrWhiteSpace(_rootPath))
                {
                    _rootPath = typeof(Logger).Assembly.GetDirectoryPath();
                    if (_rootPath.EndsWith('\\') || _rootPath.EndsWith('/'))
                    {
                        _rootPath = _rootPath[..^1];
                    }
                }
                return _rootPath;
            }
        }
        /// <summary>
        /// 格式化消息
        /// </summary>
        /// <param name="writeMessage"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string FormatMessage(string writeMessage, LoggerWriterModel model)
        {
            string errorMesage = model.Exception is null ? string.Empty : model.Exception.GetErrorMessage();
            string message = model.Message;
            message = FormatText(message, model);
            message = Regex.Replace(message, @"\$\{Exception\}", errorMesage);
            string result = writeMessage;
            result = Regex.Replace(result, @"\$\{Message\}", message);
            result = Regex.Replace(result, @"\$\{Exception\}", errorMesage);
            result = FormatText(result, model);
            result = result.Trim();
            return result;
        }
        /// <summary>
        /// 格式化文本
        /// </summary>
        /// <param name="text"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string FormatText(string text, LoggerWriterModel model)
        {
            string result = text;
            result = Regex.Replace(result, @"\$\{LogID\}", model.ID.ToString());
            result = Regex.Replace(result, @"\$\{Time\}", model.CreateTime.ToString("HH:mm:ss"));
            result = Regex.Replace(result, @"\$\{DateTime\}", model.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            result = FormatPath(result, model);
            result = result.Trim();
            return result;
        }
        /// <summary>
        /// 格式化文本
        /// </summary>
        /// <param name="path"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string FormatPath(string path, LoggerWriterModel model)
        {
            string result = path;
            result = Regex.Replace(result, @"\$\{RootPath\}", RootPath);
            result = Regex.Replace(result, @"\$\{Date\}", model.CreateTime.ToString("yyyyMMdd"));
            result = Regex.Replace(result, @"\$\{Year\}", model.CreateTime.Year.ToString());
            result = Regex.Replace(result, @"\$\{Month\}", model.CreateTime.Month.ToString());
            result = Regex.Replace(result, @"\$\{Day\}", model.CreateTime.Day.ToString());
            result = Regex.Replace(result, @"\$\{Hour\}", model.CreateTime.Hour.ToString());
            result = Regex.Replace(result, @"\$\{Minute\}", model.CreateTime.Minute.ToString());
            result = Regex.Replace(result, @"\$\{Second\}", model.CreateTime.Second.ToString());
            result = Regex.Replace(result, @"\$\{Level\}", model.LogLevel.ToString());
            result = Regex.Replace(result, @"\$\{Scope\}", model.Scope.ScopeName);
            if (!string.IsNullOrWhiteSpace(model.CategoryName))
            {
                result = Regex.Replace(result, @"\$\{CategoryName\}", model.CategoryName);
            }
            result = Regex.Replace(result, @"\$\{Application\}", model.Config.Application);
            string progressID = GetProgressID();
            result = Regex.Replace(result, @"\$\{ProgressID\}", progressID);
            result = Regex.Replace(result, @"\$\{ThreadID\}", model.ThreadID);
            result = Regex.Replace(result, @"\$\{MachineName\}", MachineName);
            foreach (KeyValuePair<string, object?> item in LoggerConfig.CustomConfig)
            {
                string value = string.Empty;
                if (item.Value is not null && !item.Value.IsNullOrWhiteSpaceString())
                {
                    value = item.Value is string stringValue ? stringValue : item.Value.ToString() ?? string.Empty;
                }
                result = Regex.Replace(result, $@"\$\{{{item.Key}\}}", value);
            }
            result = model.Scope.HandlerText(result);
            result = result.Trim();
            return result;
        }
    }
}