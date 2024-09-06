using Microsoft.AspNetCore.Http;

namespace RC.Deploy.Application
{
    /// <summary>
    /// Brotli静态文件中间件
    /// </summary>
    public class BrotliStaticFilesMiddleware(RequestDelegate next) : StaticFilesMiddlewareBase(next)
    {
        /// <summary>
        /// 接受编码
        /// </summary>
        protected override string AcceptEncoding => "br";
        /// <summary>
        /// 文件扩展名
        /// </summary>
        protected override string FileExtension => "br";
    }
}
