using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;

namespace RC.Deploy.Application
{
    /// <summary>
    /// 静态文件中间件
    /// </summary>
    public abstract class StaticFilesMiddlewareBase(RequestDelegate next)
    {
        /// <summary>
        /// 接受编码
        /// </summary>
        protected abstract string AcceptEncoding { get; }
        /// <summary>
        /// 文件扩展名
        /// </summary>
        protected abstract string FileExtension { get; }
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.AcceptEncoding.Contains(AcceptEncoding))
            {
                await next(context);
                return;
            }
            string? path = context.Request.Path.Value;
            if (path is null)
            {
                await next(context);
                return;
            }
            string filePath = path;
            if (filePath[0] == '/')
            {
                filePath = filePath[1..];
            }
            filePath = Path.Combine(GetType().Assembly.GetDirectoryPath(), "Application", filePath) + $".{FileExtension}";
            if (File.Exists(filePath))
            {
                IContentTypeProvider contentTypeProvider = MateralServices.GetRequiredService<IContentTypeProvider>();
                if (contentTypeProvider.TryGetContentType(path, out string? contentType))
                {
                    context.Response.ContentType = contentType;
                }
                else
                {
                    context.Response.ContentType = "application/octet-stream";
                }
                context.Response.Headers.ContentEncoding = AcceptEncoding;
                using FileStream stream = new(filePath, FileMode.Open);
                await stream.CopyToAsync(context.Response.Body);
                return;
            }
        }
    }
}
