using Materal.MergeBlock.AccessLog.Models;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace Materal.MergeBlock.AccessLog
{
    /// <summary>
    /// 访问日志中间件
    /// </summary>
    public class AccessLogMiddleware(ILogger<AccessLogMiddleware> logger) : IMiddleware
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            AdvancedScope advancedScope = new(ConstData.AccessLogScopeName, new Dictionary<string, object?>
            {
                [nameof(context.Request)] = new RequestModel(context.Request)
            });
            using IDisposable? scope = logger.BeginScope(advancedScope);
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
                advancedScope.ScopeData.TryAdd("ElapsedMilliseconds", stopwatch.ElapsedMilliseconds);
                advancedScope.ScopeData.TryAdd(nameof(context.Response), new ResponseModel(context.Response, memStream));
                memStream.Position = 0;
                await memStream.CopyToAsync(originalBody);
                if (context.Response.StatusCode == StatusCodes.Status200OK)
                {
                    logger.LogInformation($"[{context.Request.Method}] {context.Request.Scheme}://{context.Request.Host.Host}:{context.Request.Host.Port}{context.Request.Path} [{context.Response.StatusCode}]");
                }
                else if (context.Response.StatusCode >= 500)
                {
                    logger.LogError($"[{context.Request.Method}] {context.Request.Scheme}://{context.Request.Host.Host}:{context.Request.Host.Port}{context.Request.Path} [{context.Response.StatusCode}]");
                }
                else
                {
                    logger.LogWarning($"[{context.Request.Method}] {context.Request.Scheme}://{context.Request.Host.Host}:{context.Request.Host.Port}{context.Request.Path} [{context.Response.StatusCode}]");
                }
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                advancedScope.ScopeData.TryAdd("ElapsedMilliseconds", stopwatch.ElapsedMilliseconds);
                logger.LogError(ex, "请求有错误");
                throw;
            }
            finally
            {
                context.Response.Body = originalBody;
            }
        }
    }
}
