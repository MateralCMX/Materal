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
            DateTime startTime = DateTime.Now;
            DateTime? endTime = null;
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
                endTime = DateTime.Now;
                response = new ResponseModel(context.Response, memStream);
                memStream.Position = 0;
                await memStream.CopyToAsync(originalBody);
                logLevel = context.Response.StatusCode switch
                {
                    StatusCodes.Status200OK or
                    StatusCodes.Status201Created or
                    StatusCodes.Status202Accepted or
                    StatusCodes.Status203NonAuthoritative or
                    StatusCodes.Status204NoContent or
                    StatusCodes.Status205ResetContent or
                    StatusCodes.Status206PartialContent or
                    StatusCodes.Status207MultiStatus or
                    StatusCodes.Status208AlreadyReported or
                    StatusCodes.Status401Unauthorized or
                    StatusCodes.Status404NotFound => LogLevel.Information,
                    >= StatusCodes.Status500InternalServerError => LogLevel.Error,
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
                if (response is not null)
                {
                    accessLogService.WriteAccessLog(startTime, endTime, logLevel, request, response, stopwatch.ElapsedMilliseconds, exception);
                }
                context.Response.Body = originalBody;
            }
        }
    }
}
