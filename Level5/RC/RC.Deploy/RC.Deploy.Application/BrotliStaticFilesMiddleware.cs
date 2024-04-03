using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using System.Drawing.Drawing2D;

namespace RC.Deploy.Application
{
    /// <summary>
    /// Brotli静态文件中间件
    /// </summary>
    public class BrotliStaticFilesMiddleware(RequestDelegate next)
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            string? path = context.Request.Path.Value;
            if (path is not null)
            {
                string filePath = path;
                if (filePath[0] == '/')
                {
                    filePath = filePath[1..];
                }
                filePath = Path.Combine(GetType().Assembly.GetDirectoryPath(), "Application", filePath) + ".br";
                if(File.Exists(filePath))
                {
                    IContentTypeProvider contentTypeProvider = MateralServices.GetRequiredService<IContentTypeProvider>();
                    if(contentTypeProvider.TryGetContentType(path, out string? contentType))
                    {
                        context.Response.ContentType = contentType;
                    }
                    else
                    {
                        context.Response.ContentType = "application/octet-stream";
                    }
                    context.Response.Headers.ContentEncoding = "br";
                    using FileStream stream = new(filePath, FileMode.Open);
                    await stream.CopyToAsync(context.Response.Body);
                    return;
                }
            }
            await next(context);
        }
    }
}
