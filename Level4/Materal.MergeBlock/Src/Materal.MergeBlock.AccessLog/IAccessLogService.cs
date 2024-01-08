﻿using Materal.MergeBlock.AccessLog.Models;

namespace Materal.MergeBlock.AccessLog
{
    /// <summary>
    /// 访问日志服务
    /// </summary>
    public interface IAccessLogService
    {
        /// <summary>
        /// 写入跟踪日志
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="elapsedMilliseconds"></param>
        /// <param name="exception"></param>
        void WriteTraceLog(RequestModel request, ResponseModel response, long elapsedMilliseconds, Exception? exception = null);
        /// <summary>
        /// 写入调试日志
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="elapsedMilliseconds"></param>
        /// <param name="exception"></param>
        void WriteDebugLog(RequestModel request, ResponseModel response, long elapsedMilliseconds, Exception? exception = null);
        /// <summary>
        /// 写入信息日志
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="elapsedMilliseconds"></param>
        /// <param name="exception"></param>
        void WriteInformationLog(RequestModel request, ResponseModel response, long elapsedMilliseconds, Exception? exception = null);
        /// <summary>
        /// 写入警告日志
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="elapsedMilliseconds"></param>
        /// <param name="exception"></param>
        void WriteWarningLog(RequestModel request, ResponseModel response, long elapsedMilliseconds, Exception? exception = null);
        /// <summary>
        /// 写入错误日志
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="elapsedMilliseconds"></param>
        /// <param name="exception"></param>
        void WriteErrorLog(RequestModel request, ResponseModel response, long elapsedMilliseconds, Exception? exception = null);
        /// <summary>
        /// 写入严重日志
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="elapsedMilliseconds"></param>
        /// <param name="exception"></param>
        void WriteCriticalLog(RequestModel request, ResponseModel response, long elapsedMilliseconds, Exception? exception = null);
        /// <summary>
        /// 写入访问日志
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="elapsedMilliseconds"></param>
        /// <param name="exception"></param>
        void WriteAccessLog(LogLevel logLevel, RequestModel request, ResponseModel response, long elapsedMilliseconds, Exception? exception = null);
    }
}
