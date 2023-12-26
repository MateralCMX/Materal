using Materal.Utils.Http;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Materal.Logger.LoggerWriter
{
    /// <summary>
    /// 日志写入器帮助类
    /// </summary>
    public static class LoggerWriterHelper
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
        /// <param name="writeMessage"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string FormatMessage(string writeMessage, LoggerWriterModel model)
        {
            string errorMesage = model.Exception is null ? string.Empty : GetErrorMessage(model.Exception);
            string message = model.Message;
            message = FormatText(message, model);
            message = Regex.Replace(message, @"\$\{LogID\}", model.ID.ToString());
            message = Regex.Replace(message, @"\$\{Exception\}", errorMesage);
            string result = writeMessage;
            result = FormatText(result, model);
            result = Regex.Replace(result, @"\$\{Message\}", message);
            result = Regex.Replace(result, @"\$\{LogID\}", model.ID.ToString());
            result = Regex.Replace(result, @"\$\{Exception\}", errorMesage);
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
            result = FormatPath(result, model);
            result = Regex.Replace(result, @"\$\{Time\}", model.CreateTime.ToString("HH:mm:ss"));
            result = Regex.Replace(result, @"\$\{DateTime\}", model.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
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
                if (item.Value is not null && item.Value.IsNullOrWhiteSpaceString())
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
