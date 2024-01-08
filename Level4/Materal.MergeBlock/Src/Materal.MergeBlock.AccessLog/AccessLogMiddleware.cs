using Materal.MergeBlock.AccessLog.Models;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace Materal.MergeBlock.AccessLog
{
    /// <summary>
    /// 访问日志中间件
    /// </summary>
    public class AccessLogMiddleware(IAccessLogService accessLogService) : IMiddleware
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            RequestModel request = new(context.Request);
            ResponseModel? response = null;
            LogLevel logLevel = LogLevel.Information;
            Exception? exception = null;
            Stream originalBody = context.Response.Body;
            using MemoryStream memStream = new();
            context.Response.Body = memStream;
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();
            try
            {
                await next(context);
                stopwatch.Stop();
                memStream.Position = 0;
                response = new ResponseModel(context.Response, memStream);
                memStream.Position = 0;
                await memStream.CopyToAsync(originalBody);
                logLevel = context.Response.StatusCode switch
                {
                    StatusCodes.Status200OK or 
                    StatusCodes.Status401Unauthorized or 
                    StatusCodes.Status404NotFound => LogLevel.Information,
                    >= 500 => LogLevel.Error,
                    _ => LogLevel.Warning,
                };
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                response = new ResponseModel(ex);
                exception = ex;
                throw;
            }
            finally
            {
                if(response is not null)
                {
                    accessLogService.WriteAccessLog(logLevel, request, response, stopwatch.ElapsedMilliseconds, exception);
                }
                context.Response.Body = originalBody;
            }
        }
    }
}
